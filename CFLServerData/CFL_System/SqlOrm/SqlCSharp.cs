using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using MSTD;
using MSTD.ShBase;

namespace SqlOrm
{
    public enum SqlType
    {
        NOTMAPPED,        // retourné pour une propriété qui ne sera pas 
                          // représentée dans la db (non publique, ...)
                          
        BOOL,             // bool
        INTEGER,          // short
        BIGINT,           // long
        NUMERIC,          // decimal, double
        TEXT,             // string
        UUID,             // Guid
        DATE,             // DateTime
        TIME,             // TimeSpan

        // non sql, utile pour identifier le type d'une propriété.
        CLASS,
        ENUM,
        LIST
    }

    public static class SqlCSharp
    {
        #region types 

        public static SqlType GetSqlType(Type _t)
        {
            if(_t == typeof(bool))
                return SqlType.BOOL;
            if(_t == typeof(short))
                return SqlType.INTEGER;
            if(_t == typeof(int))
                return SqlType.INTEGER;
            if(_t == typeof(long))
                return SqlType.BIGINT;
            if(_t == typeof(decimal))
                return SqlType.NUMERIC;
            if(_t == typeof(double))
                return SqlType.NUMERIC;
            if(_t == typeof(string))
                return SqlType.TEXT;
            if(_t == typeof(Guid))
                return SqlType.UUID;
            if(_t == typeof(DateTime) || _t == typeof(DateTime?))
                return SqlType.DATE;
            if(_t == typeof(TimeSpan) || _t == typeof(TimeSpan?))
                return SqlType.TIME;
            if(_t.IsEnum)
                return SqlType.ENUM;
            if(BaseHelper.IsGenericList(_t))
                return SqlType.LIST;
            if(_t.IsSubclassOf(typeof(Base)))
                return SqlType.CLASS;

            return SqlType.NOTMAPPED;
        }

        /// <summary>
        /// Retourne une chaine représentant le type sql, ex BOOL, INTEGER, ... 
        /// Ne traite que les types primitif, Date et Time.
        /// </summary>
        public static string GetSqlTypeStr(SqlType _sqlType)
        {
            switch (_sqlType)
            {
                case SqlType.NOTMAPPED:
                    return "";
                case SqlType.BOOL:
                    return "BOOL";
                case SqlType.INTEGER:
                    return "INTEGER";
                case SqlType.BIGINT:
                    return "BIGINT";
                case SqlType.NUMERIC:
                    return "NUMERIC";
                case SqlType.TEXT:
                    return "TEXT";
                case SqlType.UUID:
                    return "UUID";//"VARCHAR(36)";
                case SqlType.DATE:
                    return "DATE";
                case SqlType.TIME:
                    return "TIME";
                default:
                    return "";
            }
        }

        public static bool IsNumeric(SqlType _t)
        {
            return _t >= SqlType.INTEGER && _t <= SqlType.NUMERIC;
        }

        #endregion types

        #region Sql values

        /// <summary>
        /// Retourne un nom de colone pour cette propriété si cette propriété peut être mappée
        /// dans la db, sinon retourne une chaine vide.
        /// Pour une classe : class_type_classmembername
        /// Pour une List{T} : list_itemstype_listmembername
        /// Pour une Enum : enum_enumtype_enummembername
        /// </summary>
        public static string ColumnName(PropertyInfo _prInfo)
        {
            if(!BaseHelper.IsMappableProperty(_prInfo))
                return "";

            switch (GetSqlType(_prInfo.PropertyType))
            {
                case SqlType.NOTMAPPED:
                    return "";

                case SqlType.CLASS:
                {
                    return ("class_"+ _prInfo.PropertyType.Name + "_" + _prInfo.Name).ToLower();
                }
                    
                case SqlType.ENUM:
                    return ("enum_" + _prInfo.PropertyType.Name + "_" + _prInfo.Name).ToLower();
                
                case SqlType.LIST:
                    return ("list_" + BaseHelper.ListItemsType(_prInfo.PropertyType).Name + "_" + _prInfo.Name).ToLower();
                    
                default:
                    return _prInfo.Name.ToLower();
            }
        }

        /// <summary>
        /// Retourne une chaine qui représentera la valeur de la propriété dans la db.
        /// Si la propriété est un objet de classe, retourne l'id de l'objet.
        /// </summary>
        public static string SqlValue(object _class, PropertyInfo _prInfo)
        {
            object _value = _prInfo.GetValue(_class);
            SqlType _sqlType = GetSqlType(_prInfo.PropertyType);
            
            return SqlValue(_value, _sqlType, _sqlType == SqlType.LIST);
        }

        /// <summary>
        /// Retourne une chaine qui représentera la valeur de la propriété dans la db.
        /// Si la propriété est un objet de classe, retourne l'id de l'objet.
        /// </summary>
        public static string SqlValue(object _value, SqlType _sqlType, bool _inList)
        {
            if(_value == null)
                return "NULL";
            
            string _format = (_inList) ? "" : "'";
            
            switch (_sqlType)
            {
                case SqlType.LIST:
                    return ListValueStr(_value);

                case SqlType.BOOL:
                    if((bool)_value == true)
                        return _format + "TRUE" + _format;
                    else
                        return _format + "FALSE" + _format;

                case SqlType.INTEGER:
                    return ((int)_value).ToString();

                case SqlType.BIGINT:
                    return ((long)_value).ToString();

                case SqlType.NUMERIC:
                {
                    NumberFormatInfo _nf = new NumberFormatInfo(){ NumberDecimalSeparator = "."};
                    if(_value is decimal)
                        return ((decimal)_value).ToString(_nf);
                    else
                        return ((double)_value).ToString(_nf);
                }
                    

                case SqlType.TEXT:
                    return _format + _value.ToString().Replace("'", "''") + _format;

                case SqlType.UUID:
                    return _format + ((Guid)_value).ToString() + _format;

                case SqlType.DATE:
                    return _format + ((DateTime)_value).ToString("yyyy-MM-dd") +  _format;

                case SqlType.TIME:
                    return _format + ((TimeSpan)_value).ToString(@"hh\:mm") + _format;

                case SqlType.CLASS:
                {
                    Type _valueType = _value.GetType();
                    return _format + _valueType.Name.ToLower() + "_" + ((Base)_value).ID.ToString() + _format;
                }

                case SqlType.ENUM:
                    return ((int)(_value)).ToString();

                default:
                    return "";
            }
        }

        /// <summary>
        /// Retourne une chaine qui représentera les valeurs de la propriété List dans la db,
        /// sous la forme val1,val2,...
        /// </summary>
        public static string ListValueStr(object _list)
        {
            IEnumerable _enumerable = _list as IEnumerable;
            string _listValues = "";
            
            foreach(object _item in _enumerable)
            {
                if(_listValues != "")
                    _listValues += ",";
                SqlType _sqlType = GetSqlType(_item.GetType());
                string _sqlValue = SqlValue(_item, _sqlType, true);
                
                _listValues += _sqlValue;
            }
            return "'" + _listValues + "'";
        }

        #endregion Sql values

        #region value from sql

        public static object ValueFromSql(string _sqlValue, SqlType _sqlType)
        {
            throw new NotImplementedException();
        }

        #endregion value from sql
    }
}
