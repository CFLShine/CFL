using System;
using System.Collections.Generic;
using System.Reflection;
using CFL_1.CFL_System.SqlServerOrm;
using MSTD.ShBase;
using Npgsql;

namespace SqlOrm
{
    public class DBSaveChanges
    {
        public DBSaveChanges(DBContext _context)
        {
            __context = _context?? throw new ArgumentNullException("_context");
        }
        
        public bool Exe()
        {
            string _saveChangesQuery = SaveChangesQuery();

            if(__context.Connection.ExecuteNonQuery(new NpgsqlCommand(_saveChangesQuery)))
            {
                foreach(KeyValuePair<Guid, ClassProxy> _kvp in __inserteds)
                    _kvp.Value.ToBeInserted = false;
                foreach(KeyValuePair<Guid, ClassProxy> _kvp in __updateds)
                    _kvp.Value.CancelChanges();

                if(__context.Connection.NotifyChanges &&
                  (__inserteds.Count != 0 || __updateds.Count != 0))
                {
                    DBNotification _notification = new DBNotification();
                    foreach(KeyValuePair<Guid, ClassProxy> _kvp in __inserteds)
                        _notification.AddEntity(_kvp.Value.TableName, _kvp.Key);
                    foreach(KeyValuePair<Guid, ClassProxy> _kvp in __updateds)
                        _notification.AddEntity(_kvp.Value.TableName, _kvp.Key);
                    __context.Connection.Notify(_notification);
                }

                return true;
            }
            return false;
        }

        private string SaveChangesQuery()
        {
            string _saveChangesQuery = "";

            foreach(DBSet _dbset in __context.GetDbSets())
            {
                foreach(ClassProxy _proxy in _dbset.ClassProxies)
                {
                    if(_proxy.ToBeInserted)
                    {
                        _saveChangesQuery += InsertQuery(_proxy);
                        __inserteds[_proxy.EntityId] = _proxy;
                    }
                    else
                    {
                        List<PropertyInfo> _changes = _proxy.ChangedProperties();
                        if(_changes.Count != 0)
                        {
                            _saveChangesQuery += UpdateQuery(_changes, _proxy);
                            __updateds[_proxy.EntityId] = _proxy;
                        }
                    }
                }
            }
            return _saveChangesQuery;
        }

        private string InsertQuery(ClassProxy _classAndProxy)
        {
            List<FieldValue> _fieldsValues = FieldsValues(_classAndProxy.Properties, _classAndProxy);
            string _query = "INSERT INTO " + _classAndProxy.TableName +
                              "(tablename," + Fields(_fieldsValues) + ") " + 
                              "VALUES (" + "'" + _classAndProxy.TableName + "'" + "," + Values(_fieldsValues) + ");";
            return _query;
        }

        private string UpdateQuery(IEnumerable<PropertyInfo> _changes, ClassProxy _classAndProxy)
        {
            List<FieldValue> _fieldsValues = FieldsValues(_changes, _classAndProxy);
            return "UPDATE " + _classAndProxy.TableName + 
                            " SET " + FieldsEqualValues(_fieldsValues, _classAndProxy) +
                             " WHERE " + _classAndProxy.TableName + ".id = " + 
                             SqlCSharp.SqlValue(_classAndProxy.Entity, BaseHelper.Property(_classAndProxy.EntityType, "ID")) +
                             ";";
        }

        private List<FieldValue> FieldsValues(IEnumerable<PropertyInfo> _changes, ClassProxy _classAndProxy)
        {
            List<FieldValue> _fieldsValues = new List<FieldValue>();
            
            foreach(PropertyInfo _prInfo in _changes)
            {
                _fieldsValues.Add(new FieldValue(_classAndProxy.Entity, _prInfo));
            }
            return _fieldsValues;
        }

        /// <summary>
        /// Retourne une chaine comme : field1,field2,...
        /// </summary>
        private string Fields(List<FieldValue> _changes)
        {
            string _fields = "";
            foreach(FieldValue _fieldValue in _changes)
            {
                if(_fields != "")
                    _fields += ",";
                _fields += _fieldValue.ColumnName;
            }
            return _fields;
        }

        /// <summary>
        /// Retourne une chaine comme : val1,val2,...
        /// </summary>
        private string Values(List<FieldValue> _changes)
        {
            string _values = "";
            foreach(FieldValue _fieldValue in _changes)
            {
                if(_values != "")
                    _values += ",";
                _values += _fieldValue.SqlValue;
            }
            return _values;
        }

        /// <summary>
        /// Retourne une chaine comme : field1=val1,field2=val2,...
        /// </summary>
        private string FieldsEqualValues(List<FieldValue> _changes, ClassProxy _classAndProxy)
        {
            string _fieldsEqualValues = "";
            foreach(FieldValue _fieldValue in _changes)
            {
                if(_fieldsEqualValues != "")
                    _fieldsEqualValues += ",";
                _fieldsEqualValues += _fieldValue.ColumnName +
                                      "=" +
                                      _fieldValue.SqlValue;
            }
            return _fieldsEqualValues;
        }

        private DBContext __context = null;
        private Dictionary<Guid, ClassProxy> __inserteds = new Dictionary<Guid, ClassProxy>();
        private Dictionary<Guid, ClassProxy> __updateds = new Dictionary<Guid, ClassProxy>();

    }
}
