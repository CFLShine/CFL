using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MSTD;
using Npgsql;

namespace SqlOrm
{
    public enum ProxyType
    {
        ClassMember,
        ListMember
    }

    public abstract class MemberProxy
    {
        public static MemberProxy Factory(ClassProxy _classProxy, string _columnName, DBRow _row)
        {
            string[] _elements = _columnName.Split('_');
            if(_elements.Length != 3)
                return null;
            if(_elements[0] == "class")
                return new ClassMemberProxy(_classProxy, _columnName, _row);
            else
            if(_elements[0] == "list")
                return new ListProxy(_classProxy, _columnName, _row);
            return null;
        }

        public ClassProxy ClassProxy
        {
            get;
            protected set;
        } = null;

        public abstract ProxyType ProxyType { get; }

        /// <summary>
        /// Le DBSet correspondant au type du membre ou des objects contenus dans la liste.
        /// </summary>
        public DBSet DBSet
        {
            get;
            protected set;
        } = null;

        public PropertyInfo Property
        {
            get;
            protected set;
        } = null;

        public DBContext Context
        {
            get
            {
                return ClassProxy.Context;
            }
        }

        /// <summary>
        /// Retourne true si Property != null
        /// </summary>
        public bool IsValid
        {
            get
            {
                return Property != null;
            }
        }
    
        public abstract void UpdateEntity();
    }

    public class ClassMemberProxy : MemberProxy
    {
        public ClassMemberProxy(ClassProxy _classProxy, string _columnName, DBRow _row)
        {
            ClassProxy = _classProxy;
            init(_columnName, _row);
        }

        public override ProxyType ProxyType
        {
            get
            {
                return ProxyType.ClassMember;
            }
        }

        public Guid MemberId
        {
            get;
            private set;
        } = Guid.Empty;

        public override void UpdateEntity()
        {
            ClassProxy _classProxy = null;
            if(DBSet != null)
                _classProxy = DBSet.GetClassProxy(MemberId);
            if(_classProxy == null)
                _classProxy = Context.GetClassProxy(MemberId);
            if(_classProxy != null)
                Property.SetValue(ClassProxy.Entity, _classProxy.Entity);
            else
                Property.SetValue(ClassProxy.Entity, null);
        }

        /// _columnName : class_type_classmembername 
        private void init(string _columnName, DBRow _row)
        {
            Property = null;
            MemberId = Guid.Empty;

            string[] _elements = _columnName.Split('_');
            Property = ObjectHelper.Property(ClassProxy.Entity, _elements[2]);

            DBSet = Context.GetDBSet(Property.PropertyType);

            // les objets de classe sont représentés ainsi : typename_guid
            object _value = _row.GetValue(_columnName);
            string _valuestr = "";
            if(_value is DBNull == true)
                return;

            _valuestr = (string)_row.GetValue(_columnName);

            _elements = _valuestr.Split('_');
            if( _elements.Length != 2
            || string.IsNullOrWhiteSpace(_elements[0])
            || string.IsNullOrWhiteSpace(_elements[1]))
            {
                throw new Exception("Valeur de champs non valide " + _valuestr);
            }

            Guid _id = Guid.Empty;
            if(!Guid.TryParse(_elements[1], out _id))
                throw new Exception("Guid non valide dans la valeur de champs " + _valuestr);
            MemberId = _id;
        }
    }

    public class ListProxy : MemberProxy
    {
        public ListProxy(ClassProxy _classProxy, string _columnName, DBRow _row)
        {
            ClassProxy = _classProxy;
            Init(_columnName, _row);
        }

        public override ProxyType ProxyType
        {
            get
            {
                return ProxyType.ListMember;
            }
        }

        /// <summary>
        /// Remplit la liste des objets quelle doit contenir.
        /// Pour chaque objet, cherche d'abord dans <see cref="PreferedDBSet"/>,
        /// si non trouvé, cherche dans tout le <see cref="DBContext"/>.
        /// </summary>
        public override void UpdateEntity()
        {
            IList _list = Property.GetValue(ClassProxy.Entity) as IList;

            if(_list == null)
                throw new Exception("L'entité de type " + ClassProxy.EntityType.Name + " contient une liste mappable nulle.");

            _list.Clear();
            ClassProxy _classProxy = null;

            foreach(KeyValuePair<Guid, DBSet> _kvp in Objects)
            {
                _classProxy = _kvp.Value.GetClassProxy(_kvp.Key);
                
                if(_classProxy != null && _classProxy.Entity != null)
                    _list.Add(_classProxy.Entity);
            }
        }

        /// <summary>
        /// Analyse _columnName pour déterminer la propriété de type List concernée,
        /// puis apelle <see cref="InitList(string)"/>.
        /// _columnName : list_itemstype_listmembername.
        /// itemstype n'est actuelement pas utile, chaque item indique son type(items sous la forme typename_guid).
        /// </summary>
        private void Init(string _columnName, DBRow _row)
        {
            Property = null;

            string[] _elements = _columnName.Split('_');
            Property = ObjectHelper.Property(ClassProxy.Entity, _elements[2]);
            InitList((string)_row.GetValue(_columnName));
        }

        public Dictionary<Guid, DBSet> Objects { get; } = new Dictionary<Guid, DBSet>();

        // 
        /// <summary>
        /// Initialise la liste de Guid __objectsIds.
        /// _fieldValue : guid1,guid2,...
        /// </summary>
        private void InitList(string _fieldValue)
        {
            Objects.Clear();

            if(string.IsNullOrWhiteSpace(_fieldValue))
                return;

            string[] _valuesstr = _fieldValue.Split(',');

            foreach(string _objectstr in _valuesstr)
            {
                string[] _elements = _objectstr.Split('_');
                
                if(_elements.Length != 2 
                || string.IsNullOrEmpty(_elements[0])
                || string.IsNullOrEmpty(_elements[1]))
                    throw new Exception("Chaine" + _objectstr + "non valide dans la liste " + _fieldValue);
                
                Guid _id = Guid.Empty;

                DBSet _dbset = ClassProxy.Context.GetDBSet(_elements[0]);
                if(_dbset == null)
                    throw new Exception("Type " + _elements[0] + " dans la liste " + _fieldValue +
                                                                 " item " + _objectstr +
                                                                 " non trouvé dans le DBContext");

                if(Guid.TryParse(_elements[1], out _id))
                {
                    Objects[_id] =  _dbset;
                }
                else
                    throw new Exception("Guid " + _elements[1] + " non valide dans la liste " + _fieldValue +
                                                                 " item " + _objectstr);
            }
        }
    }

    public class ClassProxy 
    {
        public ClassProxy(object _entity, DBContext _context)
        {
            __entity = _entity;
            EntityType = _entity.GetType();
            PropertyId = ObjectHelper.IdProperty(EntityType);
            Context = _context;
            BuildProxy();
        }

        public Type EntityType
        {
            get;
            private set;
        }

        public string TableName
        {
            get
            {
                return EntityType.Name.ToLower();
            }
        }

        public DBContext Context
        {
            get;
            private set;
        } = null;

        public PropertyInfo Property(string _propertyName)
        {
            foreach(PropertyInfo _pr in Properties)
            {
                if(_pr.Name.ToLower() == "_propertyName")
                    return _pr;
            }
            return null;
        }

        public PropertyInfo PropertyId
        {
            get;
            private set;
        }

        public IEnumerable<PropertyInfo> Properties
        {
            get
            {
                return __propertiesValues.Properties;
            }
        }

        /// <summary>
        /// Est à true par défaut.
        /// Lors de l'instanciation d'un <see cref="ClassProxy"/> suite à un chargement depuis la db,
        /// la classe qui effectue le chargement aura la responsabilité d'assigner cette propriété à false.
        /// </summary>
        public bool ToBeInserted { get; set; } = true;

        public object Entity
        {
            get
            {
                return __entity;
            }

            set
            {
                __entity = value;
                BuildProxy();
            }
        }

        public Guid EntityId
        {
            get
            {
                return ObjectHelper.ID(Entity);
            }
        }

        /// <summary>
        /// Construit ou reconstruit ses proxis de membres et de listes
        /// depuis le NpgsqlDataReader.
        /// </summary>
        public void UpdateProxies(DBRow _row)
        {
            for (int _i = 0; _i < _row.Count; _i++)
            {
                string _fieldName = _row.GetFieldName(_i);
                MemberProxy _memberProxy = MemberProxy.Factory(this, _fieldName, _row);
                if(_memberProxy != null)
                    __membersProxies[_memberProxy.Property] = _memberProxy;
            }
        }

        /// <summary>
        /// Met à jour les membres de classe et les listes de classes de l'entité
        /// en bouclant sur les proxis de membres et de listes, lesquels récupèrent
        /// les entités dans le DBContext si elles existent.
        /// </summary>
        public void UpdateEntity()
        {
            foreach(KeyValuePair<PropertyInfo, MemberProxy> _kvp in __membersProxies)
                _kvp.Value.UpdateEntity();
        }

        private void BuildProxy()
        {
            __propertiesValues.Clear();

            foreach(PropertyInfo _pr in EntityType.GetProperties())
            {
                if(ObjectHelper.IsMappableProperty(_pr))
                {
                    if(ObjectHelper.IsListOfClass(_pr))
                    {
                        List<object> _listProxy = new List<object>();
                        IList _list = _pr.GetValue(Entity) as IList;
                        foreach(object _item in _list)
                            _listProxy.Add(_item);
                        __propertiesValues.Add(_pr, _listProxy);
                    }
                    else
                        __propertiesValues.Add(_pr, _pr.GetValue(__entity));
                }
            }
        }

        /// <summary>
        /// Remet les valeurs du proxy conformes aux valeurs de l'entité.
        /// </summary>
        public void CancelChanges()
        {
            BuildProxy();
        }

        public List<PropertyInfo> ChangedProperties()
        {
            List<PropertyInfo> _changeds = new List<PropertyInfo>();
            foreach(PropertyInfo _prInfo in __propertiesValues.Properties)
            {
                if(HasChanged(_prInfo))
                    _changeds.Add(_prInfo);
            }
            return _changeds;
        }

        public Dictionary<PropertyInfo, MemberProxy>.ValueCollection MembersProxies
        {
            get
            {
                return __membersProxies.Values;
            }
        }

        public MemberProxy GetMemberProxy(string _memberName)
        {
            _memberName = _memberName.ToLower();

            foreach(MemberProxy _memberProxy in MembersProxies)
            {
                if(_memberProxy.Property.Name.ToLower() == _memberName)
                    return _memberProxy;
            }
            return null;
        }

        /// <summary>
        /// Si la propriété est
        /// - une List : vérifie le nombre d'items et les id des entité dans la liste.
        /// - une entité : vérifie la valeur, c.a.d. la référence.
        /// - une primitive ou une structure (DateTime, ...) : vérifie la valeur.
        /// </summary>
        private bool HasChanged(PropertyInfo _prInfo)
        {
            Type _t = _prInfo.PropertyType;

            if(ObjectHelper.IsGenericList(_t))
            {
                IEnumerable _entityEnumerable = (IEnumerable)_prInfo.GetValue(__entity);
                IEnumerable _proxyEnumerable = (IEnumerable)__propertiesValues.GetValue(_prInfo);
                
                IEnumerator _entityListEnumerator = _entityEnumerable.GetEnumerator();
                IEnumerator _proxyListEnumerator = _proxyEnumerable.GetEnumerator();

                while(_entityListEnumerator.MoveNext())
                {
                    // la list dans l'entité a été augmentée
                    if(! _proxyListEnumerator.MoveNext())
                        return true;
                    
                    if(_entityListEnumerator.Current != _proxyListEnumerator.Current)
                        return true;
                }

                // la liste dans l'entité a été réduite
                if(_proxyListEnumerator.MoveNext())
                    return true;
            }
            else 
            // un objet de classe
            if(ObjectHelper.IsMappableClassType(_t))
            {
                object _entityValue = _prInfo.GetValue(__entity);
                object _proxyValue = __propertiesValues.GetValue(_prInfo);

                if(_entityValue == null && _proxyValue == null)
                    return false;

                if((_entityValue == null && _proxyValue != null)
                || (_entityValue != null && _proxyValue == null))
                    return true;

                if(ObjectHelper.ID(_entityValue) != ObjectHelper.ID(_proxyValue))
                    return true;
                return false;
            }

            // value property
            // il n'est pas utile de faire un test spécial pour les nullables,
            // int? _i is int retourne true et le cast (int)object nullable int fonctionne.
            {
                object _entityValue = _prInfo.GetValue(__entity);
                object _proxyValue = __propertiesValues.GetValue(_prInfo);

                if(_entityValue == null && _proxyValue == null)
                    return false;
                
                if((_entityValue == null && _proxyValue != null)
                || (_entityValue != null && _proxyValue == null))
                    return true;

                if(_entityValue is Guid _guidV)
                    return _guidV != (Guid)_proxyValue;

                if(_entityValue is int _intV)
                    return _intV != (int)_proxyValue;

                if(_entityValue is long _lngV)
                    return _lngV != (long)_proxyValue;

                if(_entityValue is double _dblV)
                    return _dblV != (double)_proxyValue;

                if(_entityValue is decimal _decV)
                    return _decV != (decimal)_proxyValue;

                if(_entityValue is string _strV)
                    return _strV != (string)_proxyValue;

                if(_entityValue is DateTime _dtV)
                    return _dtV != (DateTime)_proxyValue;

                if(_entityValue is TimeSpan _tspV)
                    return _tspV != (TimeSpan)_proxyValue;
            }
            return false;
        }

        private object __entity = null;

        private Dictionary<PropertyInfo, MemberProxy> __membersProxies = new Dictionary<PropertyInfo, MemberProxy>();
    
        private PropertiesValue __propertiesValues = new PropertiesValue();

        private class PropertiesValue
        {
            public void Clear()
            {
                __properties.Clear();
                __values.Clear();
            }

            public void Add(PropertyInfo _prInfo, object _value)
            {
                __properties.Add(_prInfo);
                __values.Add(_value);
            }

            public void SetValue(PropertyInfo _prInfo, object _value)
            {
                int _i = IndexOf(_prInfo);
                if(_i == -1)
                    throw new Exception("La propriété " + _prInfo.Name + " n'existe pas dans la collection.");
                __values[_i] = _value;
            }

            public object GetValue(PropertyInfo _prInfo)
            {
                int _i = IndexOf(_prInfo);
                if(_i == -1)
                    throw new Exception("La propriété " + _prInfo.Name + " n'existe pas dans la collection.");
                return __values[_i];
            }

            public IEnumerable<PropertyInfo> Properties
            {
                get
                {
                    foreach(PropertyInfo _pr in __properties)
                        yield return _pr;
                }
            }

            public int IndexOf(PropertyInfo _prInfo)
            {
                int _i = 0;
                foreach(PropertyInfo _pr in __properties)
                {
                    if(_pr == _prInfo)
                        return _i;
                    _i++;
                }
                return -1;
            }

            private List<PropertyInfo> __properties = new List<PropertyInfo>();
            private List<object> __values = new List<object>();
        }
    }

}
