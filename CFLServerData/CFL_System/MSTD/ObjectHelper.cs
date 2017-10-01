

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MSTD
{
    public class ObjectHelper
    {
        /// <summary>
        /// Vérifie que cette propriété est publique,
        /// qu'elle n'a pas l'attribut [NotMapped] et
        /// qu'elle pocède les methodes get et set.
        /// </summary>
        public static bool IsMappableProperty(PropertyInfo _prInfo)
        {
            Type _t = _prInfo.PropertyType;

            if(_prInfo.GetCustomAttribute<NotMappedAttribute>() != null
            || _t.IsPublic == false
            || _prInfo.CanRead == false
            || _prInfo.CanWrite == false)
                return false;
            return true;
        }

        /// <summary>
        /// Vérifie que ce type est une classe
        /// et que cette classe contient une propriété Guid id (ou Id ou ID) mappable.
        /// </summary>
        public static bool IsMappableClassType(Type _t)
        {
            if(!IsCustomClass(_t))
                return false;
            return IdProperty(_t) != null;
        }

        public static bool IsMappableListOfClass(PropertyInfo _prInfo)
        {
            return IsMappableProperty(_prInfo) && IsListOfClass(_prInfo);
        }

        public static bool IsListOfClass(PropertyInfo _prInfo)
        {
            if(!IsGenericList(_prInfo.PropertyType))
                return false;
            Type _itemsType = GetListItemsType(_prInfo.PropertyType);
            return IsMappableClassType(_itemsType);
        }

        public static PropertyInfo IdProperty(Type _classType)
        {
            foreach(PropertyInfo _pr in _classType.GetProperties())
            {
                if(_pr.Name.ToLower() == "id" && _pr.PropertyType == typeof(Guid)
                && IsMappableProperty(_pr))
                    return _pr;
            }
            return null;
        }

        public static Guid ID(object _entity)
        {
            PropertyInfo _prInfo = IdProperty(_entity.GetType());
            return (Guid)(_prInfo.GetValue(_entity));
        }

        public static void SetId(object _entity, Guid _id)
        {
            PropertyInfo _prInfo = IdProperty(_entity.GetType());
            _prInfo.SetValue(_entity, _id);
        }

        /// <summary>
        /// Retourne la propriété de la classe _object dont le nom est _propertyName.
        /// N'est pas sensible à la casse.
        /// </summary>
        public static PropertyInfo Property(object _object, string _propertyName)
        {
            return Property(_object.GetType(), _propertyName);
        }

        /// <summary>
        /// Retourne la première propriété mappable de type _ofType
        /// trouvée dans le type _inEntity, ou null si non trouvée.
        /// </summary>
        public static PropertyInfo Property(Type _ofType, Type _inEntity)
        {
            foreach(PropertyInfo _prInfo in _inEntity.GetProperties())
            {
                if(_prInfo.PropertyType == _ofType && IsMappableProperty(_prInfo))
                    return _prInfo;
            }
            return null;
        }

        /// <summary>
        /// Retourne la propriété de la classe de type _classType dont le nom est _propertyName.
        /// N'est pas sensible à la casse.
        /// </summary>
        public static PropertyInfo Property(Type _classType, string _propertyName)
        {
            foreach(PropertyInfo _prInfo in _classType.GetProperties())
            {
                if(_prInfo.Name.ToLower() == _propertyName.ToLower())
                    return _prInfo;
            }
            return null;
        }

        public static bool IsGenericList(Type _type)
        {
            return _type.IsGenericType && _type.GetGenericTypeDefinition() == typeof(List<>);
        }

        public static Type GetListItemsType(Type _ListType)
        {
            if(!IsGenericList(_ListType))
                return null;
            return _ListType.GetGenericArguments()[0];
        }

        /// <summary>
        /// Retourne vrai si _t.IsClass
        /// et _t n'est pas typeof(string), typeof(Decimal)
        /// </summary>
        public static bool IsCustomClass(Type _t)
        {
            return _t.IsClass 
                && _t != typeof(string)
                && _t != typeof(Decimal)
                && !IsGenericList(_t);
        }
    }
}
