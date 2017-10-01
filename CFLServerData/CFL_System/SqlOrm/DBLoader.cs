using System;
using System.Collections.Generic;
using System.Reflection;
using MSTD;
using Npgsql;

namespace SqlOrm
{
    public class DBLoader<T> where T : class, new()
    {
        public DBLoader(DBSelect<T> _select)
        {
            __select = _select;
        }

        public List<T> ToList() 
        {
            DbContext.StartProcess();
            List<T> _list = new List<T>();
            string _initialQuery = InitialQuery();
            List<ClassProxy> _initials = Load(_initialQuery);

            foreach(ClassProxy _proxy in _initials)
                _list.Add((T)_proxy.Entity);
            
            Include(_initials);

            DbContext.UpdateClassProxiesEntities();
            DbContext.CancelProcessedsChanges();
            DbContext.EndProcess();

            return _list;
        }

        public T First() 
        {
            List<T> _list = ToList();
            if(_list.Count > 0)
                return _list[0];
            return null;
        }

        public X LoadMother<X>() where X : class, new()
        {
            T _child = First();
            string _memberName = ObjectHelper.Property(typeof(T), typeof(X)).Name.ToLower();
            string _fieldName = 
                "class_" + typeof(T).Name.ToLower() + "_" + _memberName;
            DBSelect<X> _select = new DBSelect<X>(DbContext);
            return _select.Where(_fieldName + " = '" + ObjectHelper.ID(_child).ToString() + "'").First();
        }

        public DBConnection Connection
        {
            get
            {
                return __select.Connection;
            }
        }
        
        private List<ClassProxy> Load(string _query)
        {
            List<ClassProxy> _proxies = new List<ClassProxy>();

            DBReader _reader = new DBReader(Connection, _query);
            while(_reader.Read())
            {
                ClassProxy _classProxy = RetrieveOrCreate(_reader.CurrentRow.GetTableName(), _reader.CurrentRow.GetId());
                _classProxy.ToBeInserted = false;
                GiveValuesToClass(_classProxy, _reader.CurrentRow);
                _classProxy.UpdateProxies(_reader.CurrentRow);
                DbContext.AttachProxy(_classProxy);
                _proxies.Add(_classProxy);
                DbContext.Process(_classProxy);
            }
            return _proxies;
        }

        #region initial query

        private string InitialQuery()
        {
            string _members = SelectedMembers();
            string _tableName = typeof(T).Name.ToLower();
            
            string _sqlSelect =  "SELECT " + _members +
                       " FROM " + _tableName + " ";
            if(!string.IsNullOrWhiteSpace(__select.WherePredicats))
                    _sqlSelect += " WHERE " + __select.WherePredicats;
            
            return _sqlSelect + ";";
        }

        private string SelectedMembers()
        {
            string _members = "id";
            foreach(string _member in __select.SelectedMembers)
            {
                if(_member.ToLower() == "all" || _member == "*")
                    return "*";
                if(_member != "id")
                {
                    _members += ",";
                    _members += _member;
                }
            }
            return _members;
        }

        #endregion initial query

        #region Include

        private void Include(List<ClassProxy> _initials)
        {
            string _includeQuery = "";

            foreach(ClassProxy _initial in _initials)
            {
                foreach(string _include in __select.Includeds)
                {
                    if(_include.ToLower() == "all")
                    {
                        _includeQuery = IncludeAllQuery(_initial);
                        break;
                    }
                    else
                        _includeQuery += IncludeQuery(_initial, _include);
                }
            }
            
            if(_includeQuery == "")
                return;
            List<ClassProxy> _includeds = Load(_includeQuery);
            IncludeCascade(_includeds);
        }

        private void IncludeCascade(List<ClassProxy> _proxies)
        {
            string _includeQuery = "";
            foreach(ClassProxy _proxy in _proxies)
            {
                _includeQuery += IncludeAllQuery(_proxy);
            }

            if(_includeQuery != "")
            {
                _proxies = Load(_includeQuery);
                IncludeCascade(_proxies);
            }
        }

        private string IncludeQuery(ClassProxy _classProxy, string _memberName)
        {
            MemberProxy _memberProxy = _classProxy.GetMemberProxy(_memberName);
            if(_memberProxy.ProxyType == ProxyType.ClassMember)
                return IncludeClassQuery((ClassMemberProxy)_memberProxy);
            else
                return IncludeListQuery((ListProxy)_memberProxy);
        }

        private string IncludeAllQuery(ClassProxy _classProxy)
        {
            string _query = "";
            foreach(MemberProxy _memberProxy in _classProxy.MembersProxies)
            {
                if(_memberProxy.ProxyType == ProxyType.ClassMember)
                    _query += IncludeClassQuery((ClassMemberProxy)_memberProxy);
                else
                    _query += IncludeListQuery((ListProxy)_memberProxy);
            }
            return _query;
        }

        private string IncludeClassQuery(ClassMemberProxy _memberProxy)
        {
            // si le membre est null
            if(_memberProxy.MemberId == Guid.Empty)
                return "";

            string _memberGuid = _memberProxy.MemberId.ToString();
            if(DbContext.IsProssed(_memberGuid))
                return "";
            return "SELECT * FROM " + _memberProxy.DBSet.EntitiesTypeName.ToLower() + 
                   " WHERE id = " + "'" + _memberProxy.MemberId.ToString() + "'" + ";";
        }

        private string IncludeListQuery(ListProxy _memberProxy)
        {
            string _query = "";

            foreach(KeyValuePair<Guid, DBSet> _kvp in _memberProxy.Objects)
            {
                string _tableName = _kvp.Value.EntitiesTypeName.ToLower();
                string _guidstr = _kvp.Key.ToString();
                if(!DbContext.IsProssed(_guidstr))
                {
                    _query +=
                    "SELECT * FROM " + _tableName + 
                    " WHERE id = " + "'" + _guidstr + "'" + ";";
                }
            }
            return _query;
        }

        #endregion Include

        #region give values to object

        /// <summary>
        /// Les données sont extraites de la ligne en cours du reader.
        /// </summary>
        public void GiveValuesToClass(ClassProxy _proxy, DBRow _row)
        {
            for (int _i = 0; _i < _row.Count; _i++)
            {
                string _fieldName = _row.GetFieldName(_i);
                PropertyInfo _propertyInfo = ObjectHelper.Property(_proxy.Entity, _fieldName);
                if(_propertyInfo != null)
                {
                    object _value = _row.GetValue(_i);
                    SetValueToPrimaryMember(_proxy.Entity, _propertyInfo, _value);
                }
            }
        }

        private void SetValueToPrimaryMember(object _entity, PropertyInfo _pr, object _value)
        {
            Type _valueType = _value.GetType();
            Type _propertyType = _pr.PropertyType;

            if(_value is DBNull)
            {
                if (Nullable.GetUnderlyingType(_propertyType) != null)
                    _pr.SetValue(_entity, null);
                
                else
                if(_propertyType == typeof(bool))
                    _pr.SetValue(_entity, false);
                
                else
                if(_propertyType == typeof(int)
                || _propertyType == typeof(long)
                || _propertyType == typeof(double)
                || _propertyType == typeof(decimal))
                    _pr.SetValue(_entity, 0);
                
                else
                if(_propertyType == typeof(string))
                    _pr.SetValue(_entity, "");

                return;
            }

            if(_valueType != _propertyType)
            {
                if(_valueType == typeof(decimal) && _propertyType == typeof(double))
                    _pr.SetValue(_entity, Convert.ToDouble(_value));
                else 

                if (Nullable.GetUnderlyingType(_propertyType) != null)
                {
                    if(_propertyType == typeof(bool?))
                        _pr.SetValue(_entity, (bool?)_value);
                    else
                    if(_propertyType == typeof(int?))
                        _pr.SetValue(_entity, (int?)_value);
                    else
                    if(_propertyType == typeof(long?))
                        _pr.SetValue(_entity, (long?)_value);
                    else
                    if(_propertyType == typeof(double?))
                        _pr.SetValue(_entity, (double?)_value);
                    else
                        if(_propertyType == typeof(decimal?))
                        _pr.SetValue(_entity, (decimal?)_value);
                    else
                        if(_valueType == typeof(DateTime?))
                        _pr.SetValue(_entity, (DateTime?)_value);
                    else
                    if(_propertyType == typeof(TimeSpan?))
                        _pr.SetValue(_entity, (TimeSpan?)_value);
                }

                else throw new Exception("_propertyType = " + _propertyType.Name + Environment.NewLine + 
                                         "_valueType = " + _valueType.Name + Environment.NewLine +
                                         "Cast de ces types non prévu dans cette fonction.");
            }
            else
                _pr.SetValue(_entity, _value);
        }

        #endregion
        
        DBContext DbContext
        {
            get
            {
                return __select.DbContext;
            }
        }

        private ClassProxy RetrieveOrCreate(string _typeName, Guid _id)
        {
            DBSet _dbset = __select.DbContext.GetDBSet(_typeName);

            foreach(ClassProxy _classAndProxy in _dbset.ClassProxies)
            {
                object _entity = _classAndProxy.Entity;
                if(ObjectHelper.ID(_entity) == _id)
                    return _classAndProxy;
            }
            
            // non trouvé.

            ClassProxy _newClassAndProxy = _dbset.Factory();
            ObjectHelper.SetId(_newClassAndProxy.Entity, _id);
            return _newClassAndProxy;
        }

        private DBSelect<T> __select = null;
    }
}
