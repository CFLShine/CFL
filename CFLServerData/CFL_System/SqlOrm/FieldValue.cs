using System;
using System.Reflection;

namespace SqlOrm
{
    public class FieldValue
    {
        public FieldValue(object _class, PropertyInfo _prInfo)
        {
            Class = _class;
            PrInfo = _prInfo;
            SqlValueType = SqlCSharp.GetSqlType(_prInfo.PropertyType);
        }

        public object Class 
        {
            get;
            private set;
        } = null;

        public PropertyInfo PrInfo
        {
            get ;
            private set ;
        }

        public string ColumnName
        {
            get { return SqlCSharp.ColumnName(PrInfo) ; }
        } 

        public SqlType SqlValueType
        {
            get;
            private set;
        }

        public object Value
        {
            get
            {
                 return PrInfo.GetValue(Class);
            }
        } 

        /// <summary>
        /// Le get :
        /// Utiliser lors
        /// Le set :
        /// utiliser lors d'un chargement depuis la db.
        /// Remet à jour la valeur dans Class.
        /// </summary>
        public string SqlValue
        {
            get
            {
                string _sqlValue = SqlCSharp.SqlValue(Value, SqlValueType, SqlValueType == SqlType.LIST);
                if(string.IsNullOrWhiteSpace(_sqlValue))
                    throw new Exception("Aucune valeur Sql n'a été retournée.");
                return _sqlValue;
            }

            set
            {
                PrInfo.SetValue(Class, SqlCSharp.ValueFromSql(value, SqlValueType));
            }
        }

    }
}
