using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using CFL_1.CFL_System.MSTD;
using MSTD;
using MSTD.ShBase;

namespace SqlOrm
{
    public enum SqlType
    {
        /// <summary>
        /// retourné pour une propriété qui ne sera pas 
        /// représentée dans la db (non publique, ...)
        /// </summary>
        NOTMAPPED,        
        
        /// <summary>
        /// équivalent c# : bool
        /// </summary>
        BOOL,  
        
        /// <summary>
        /// équivalent c# : short, int
        /// </summary>
        INTEGER, 
        
        /// <summary>
        /// équivalent c# : long
        /// </summary>
        BIGINT,    
        
        /// <summary>
        /// équivalent c# : decimal, double
        /// </summary>
        NUMERIC,          

        /// <summary>
        /// équivalent c# : string
        /// </summary>
        TEXT, 
        
        /// <summary>
        /// équivalent c# : Guid
        /// </summary>
        UUID,             

        /// <summary>
        /// équivalent c# : DateTime, DateTime?
        /// </summary>
        DATE,             

        /// <summary>
        /// équivalent c# : TimeSpan, TimeSpan?
        /// </summary>
        TIME,             

        /// <summary>
        /// équivalent c# : propriété de type <see cref="Base"/>
        /// ou dérivée.
        /// </summary>
        CLASS,

        ENUM,

        LIST
    }

    public static class SqlCSharp
    {
        #region types 

        /// <summary>
        /// Retourne un <see cref="SqlType"/> pour un type C# :
        /// NOTMAPPED,        retourné pour une propriété qui ne sera pas 
        ///                   représentée dans la db (non publique, ...)
        ///                 
        /// <see cref="SqlType.BOOL"/>     : bool.
        /// <see cref="SqlType.INTEGER"/>  : short, int.
        /// <see cref="SqlType.BIGINT"/>   : long.
        /// <see cref="SqlType.NUMERIC"/>  : decimal, double.
        /// <see cref="SqlType.TEXT"/>     : string.
        /// <see cref="SqlType.UUID"/>     : Guid.
        /// <see cref="SqlType.DATE"/>     : DateTime ou DateTime?.
        /// <see cref="SqlType.TIME"/>     : TimeSpan ou TimeSpan?.
        /// 
        /// Non sql, utiles pour identifier le type d'une propriété :
        /// <see cref="SqlType.CLASS"/> propriété de type <see cref="Base"/> ou dérivé.
        /// <see cref="SqlType.ENUM"/>
        /// <see cref="SqlType.LIST"/>
        /// 
        /// </summary>
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
            if(TypeHelper.IsGenericList(_t))
                return SqlType.LIST;
            if(_t.IsSubclassOf(typeof(Base)))
                return SqlType.CLASS;

            return SqlType.NOTMAPPED;
        }

        /// <summary>
        /// Retourne une chaine représentant le type sql, ex "BOOL", "INTEGER", ... 
        /// Ne traite que les types primitif, Guid, Date et Time.
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
                    return "UUID"; //"VARCHAR(36)";
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

        #region Column name

        /// <summary>
        /// Retourne un nom de colone pour cette propriété si cette propriété peut être mappée
        /// dans la db, sinon retourne une chaine vide.
        /// Pour une classe : class_typename_membername
        /// Pour une List{T} : list_itemstype_membername
        /// Pour une Enum : enum_enumtype_membername
        /// Provoque une exception si membreName null ou vide.
        /// </summary>
        public static string ColumnName(PropertyInfo _prInfo)
        {
            if(!PropertyHelper.IsMappableProperty(_prInfo))
                return "";

            return ColumnName(_prInfo.PropertyType, _prInfo.Name);
        }

        /// <summary>
        /// Retourne un nom de colone pour un membre de type type et nomé memberName.
        /// Pour une classe : class_typename_membername
        /// Pour une List{T} : list_itemstype_membername
        /// Pour une Enum : enum_enumtype_membername
        /// Provoque une exception si membreName null ou vide.
        /// </summary>
        public static string ColumnName(Type type, string memberName)
        {
            switch (GetSqlType(type))
            {
                case SqlType.NOTMAPPED:
                    return "";

                case SqlType.CLASS:
                    return ClassObjectMemberColumnName(type, memberName);
                    
                case SqlType.ENUM:
                    return EnumMemberColumnName(type, memberName);
                
                case SqlType.LIST:
                    return ListMemberColumnName(type, memberName);
                    
                default:
                    if(string.IsNullOrWhiteSpace(memberName))
                        throw new Exception("memberName non valide");
                    return memberName.ToLower();
            }
        }

        /// <summary>
        /// Retourne un nom de colone pour un membre objet de classe et nomé memberName,
        /// sous la forme class_typename_membername.
        /// Provoque une exception si membreName null ou vide.
        /// </summary>
        public static string ClassObjectMemberColumnName(Type type, string memberName)
        {
            if(string.IsNullOrWhiteSpace(memberName))
                throw new Exception("memberName non valide");
            return ("class_"+ type.Name + "_" + memberName).ToLower();
        }

        /// <summary>
        /// Retourne un nom de colone pour un membre list et nomé memberName,
        /// sous la forme list_itemstype_membername.
        /// Provoque une exception si membreName null ou vide. 
        /// </summary>
        public static string ListMemberColumnName(Type type, string memberName)
        {
            if(string.IsNullOrWhiteSpace(memberName))
                throw new Exception("memberName non valide");
            return ("list_" + TypeHelper.ListItemsType(type).Name + "_" + memberName).ToLower();
        }

        /// <summary>
        /// Retourne un nom de colone pour un membre Enum et nomé memberName,
        /// sous la forme enum_enumtype_membername.
        /// Provoque une exception si membreName null ou vide. 
        /// </summary>
        public static string EnumMemberColumnName(Type type, string memberName)
        {
            if(string.IsNullOrWhiteSpace(memberName))
                throw new Exception("memberName non valide");
            return ("enum_" + type.Name + "_" + memberName).ToLower();
        }

        #endregion Column name

        #region Sql values

        /// <summary>
        /// Retourne une chaine qui représentera la valeur de la propriété dans la db.
        /// Si la propriété est un objet de classe, la forme est typename_id.
        /// </summary>
        public static string SqlValue(object _class, PropertyInfo _prInfo)
        {
            object _value = _prInfo.GetValue(_class);
            SqlType _sqlType = GetSqlType(_prInfo.PropertyType);
            
            return SqlValue(_value, _sqlType, _sqlType == SqlType.LIST);
        }

        /// <summary>
        /// Retourne une chaine qui représentera la valeur de la propriété dans la db.
        /// Si la propriété est un objet de classe, la valeur est l'id.
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
                    if(_inList)
                    {
                        Type _valueType = _value.GetType();
                        return _format + _valueType.Name.ToLower() + "_" + ((Base)_value).ID.ToString() + _format;
                    }
                    else
                    {
                        return ((Base)_value).ID.ToString();
                    }
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
        /// Si les valeurs sont des objets de classe, elles sont représentée sous la forme
        /// typename_id.
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

        public static void UpdateClassProxyMembers(ClassProxy proxy, DBRow row)
        {
            foreach(DBField _field in row)
            {
                PropertyProxy _prProxy = proxy.Property(_field.Name);
                if(_prProxy != null)// des colones dans la tables servent
                                    // au fonctionnement de l'orm et correspondent
                                    // pas à des propriétés d'une entité.
                    _prProxy.Value = _field.Value;
            }
        }

        public static object ValueFromSql(string _sqlValue, SqlType _sqlType)
        {
            throw new NotImplementedException();
        }

        #endregion value from sql
    }
}
