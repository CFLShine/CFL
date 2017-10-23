using System;
using System.Collections.Generic;
using MSTD.ShBase;

namespace MSTD
{
    /// <summary>
    /// Représente un type qu'il soit nullable ou pas.
    /// Prend en compte le type <see cref="Base"/>.
    /// LIST pour tout type de List, DICTIONARY pour tout type de Dictionary,
    /// ARRAY pour tout type de Array.
    /// </summary>
    public enum TypeEnum
    {
        UNKNOWN,
        STRING,
        BASE,
        ENUM,
        BOOL,
        INT,
        CHAR,
        LONG,
        DOUBLE,
        DECIMAL,
        DATETIME,
        TIMESPAN
    }

    public static class TypeHelper
    {
        #region Instance

        public static object NewInstance(object o)
        {
            return NewInstance(o.GetType());
        }

        public static object NewInstance(Type T)
        {
            return (Activator.CreateInstance(T));
        }

        public static T NewInstance<T>()
        {
            return (T) NewInstance(typeof(T));
        }

        public static object GetDefaultValue(Type t)
        {
            if(t != null)
            {
                if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
                    return Activator.CreateInstance(t);
            }
            return null;
        }

        #endregion Instance


        /// <summary>
        /// Retourne true si le type est primitif : 
        /// Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single,
        /// ou string, Guid, DateTime, TimeSpan, Decimal (nullables ou non).
        /// Inclus les types nullable.
        /// </summary>
        public static bool IsPrimitiveOrAlike(Type type)
        {
            return type.IsPrimitive
                || type == typeof(string)
                || type == typeof(Guid)
                || type == typeof(Guid?)
                || type == typeof(DateTime)
                || type == typeof(TimeSpan)
                || type == typeof(Decimal?)
                || type == typeof(DateTime?)
                || type == typeof(TimeSpan?)
                || type == typeof(Decimal);
        }

        public static bool IsGenericList(Type _type)
        {
            return(_type.IsGenericType && _type.GetGenericTypeDefinition() == typeof(List<>));
        }

        /// <summary>
        /// Retourne true si objectType est une liste dont les items
        /// sont de type ou sous-type de itemsType.
        /// </summary>
        public static bool IsListOf(Type objectType, Type itemsType)
        {
            return IsGenericList(objectType) && 
            (ListItemsType(objectType) == itemsType || ListItemsType(objectType).IsSubclassOf(itemsType));
        }

        /// <summary>
        /// Retourne le type des items d'une liste.
        /// </summary>
        public static Type ListItemsType(Type _ListType)
        {
            return _ListType.GetGenericArguments()[0];
        }

        public static bool IsDictionary(Type type)
        {
            return(type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(Dictionary<,>));
        }

        /// <summary>
        /// Retourne un tuple du type des clés et du type des valeurs du dictionnaire si 
        /// type est un type de dictionnaire, sinon retourne null.
        /// </summary>
        public static Tuple<Type, Type> DictionaryKeysValuesTypes(Type type)
        {
            if(!IsDictionary(type))
                return null;
            return new Tuple<Type, Type>(type.GetGenericArguments()[0], type.GetGenericArguments()[1]);
        }

    }
}
