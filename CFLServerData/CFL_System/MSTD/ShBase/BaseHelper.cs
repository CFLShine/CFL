using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MSTD.ShBase
{
    public static class BaseHelper
    {
        /// <summary>
        /// Retourne une nouvelle instance du même type que _base.
        /// </summary>
        public static Base NewInstance(Base _base)
        {
            return (Base)(Activator.CreateInstance(_base.GetType()));
        }

        #region Components

        /// <summary>
        /// Retourne le premier composant trouvé de type T, qu'il soit contenu par une propriété
        /// ou une liste.
        /// </summary>
        public static Base ComponentOfType(Base _parent, Type T)
        {
            return ComponentOfType(_parent, T.Name);
        }

        /// <summary>
        /// Retourne le premier composant trouvé de type _typeName, qu'il soit contenu par une propriété
        /// ou une liste.
        /// Non sensible à la casse.
        /// </summary>
        public static Base ComponentOfType(Base _parent, string _typeName)
        {
            _typeName = _typeName.ToLower();
            Type _propertyType = null;

            foreach(PropertyInfo _pr in _parent.GetType().GetProperties())
            {
                _propertyType = _pr.PropertyType;
                if(_propertyType.IsPublic)
                {
                    if(_propertyType.Name.ToLower() == _typeName)
                    { 
                        return (Base)(_pr.GetValue(_parent));
                    }

                    if(IsGenericList(_propertyType))
                    {
                        Type _itemsType = ListItemsType(_propertyType);
                        if(_itemsType == typeof(Base) || _itemsType.IsSubclassOf(typeof(Base)))
                        {
                            IList _list = (IList)_pr.GetValue(_parent);
                            foreach(object _o in _list)
                            {
                                if(_o != null)
                                {
                                    Type _ot = _o.GetType();
                                    if(_o.GetType().Name.ToLower() == _typeName)
                                    return (Base)_o;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retourne les composants de type T, qu'ils soient contenus par des propriétés
        /// ou des listes.
        /// </summary>
        public static IEnumerable<Base> ComponentsOfType(Base _parent, Type T)
        {
            return ComponentsOfType(_parent, T.Name.ToLower());
        }

        /// <summary>
        /// Retourne les composants de type _typeName, qu'ils soient contenus par des propriétés
        /// ou des listes.
        /// Non sensible à la casse.
        /// </summary>
        public static IEnumerable<Base> ComponentsOfType(Base _parent, string _typeName)
        {
            _typeName = _typeName.ToLower();
            Type _propertyType = null;

            foreach(PropertyInfo _pr in _parent.GetType().GetProperties())
            {
                _propertyType = _pr.PropertyType;
                if(_propertyType.IsPublic)
                {
                    if(_propertyType.Name.ToLower() == _typeName)
                    { 
                        yield return (Base)(_pr.GetValue(_parent));
                    }

                    if(IsGenericList(_propertyType) && ListItemsType(_propertyType).IsSubclassOf(typeof(Base)))
                    {
                        IList _list = (IList)_pr.GetValue(_parent);
                        foreach(object _o in _list)
                        {
                            if(_o != null)
                            {
                                Type _t = _o.GetType();
                                if(_o.GetType().Name.ToLower() == _typeName)
                                yield return (Base)_o;
                            }
                        }
                    }
                }
            }
        }

        public static Base Component(Base _parent, Guid _id)
        {
            foreach(PropertyInfo _pr in _parent.GetType().GetProperties())
            {
                Type _propertyType = _pr.PropertyType;
                if(_propertyType.IsPublic)
                {
                    if(_propertyType.IsPublic && _propertyType.IsSubclassOf(typeof(Base)))
                    { 
                        Base _value = (Base)(_pr.GetValue(_parent));
                        if(_value.ID == _id)
                            return _value;
                    }

                    if(IsGenericList(_propertyType) && ListItemsType(_propertyType).IsSubclassOf(typeof(Base)))
                    {
                        IList _list = (IList)_pr.GetValue(_parent);
                        foreach(object _o in _list)
                        {
                            if(_o is Base _component && _component.ID == _id)
                                return _component;
                        }
                    }
                }
            }
            return null;
        }

        #endregion Components

        #region DB

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

        #endregion DB

        public static string GetNameToDisplay(PropertyInfo _prInfo)
        {
            DisplayAttribute _attribute = _prInfo.GetCustomAttribute<DisplayAttribute>();
            return (_attribute != null && !string.IsNullOrWhiteSpace(_attribute.Name))?
                    _attribute.Name : _prInfo.Name;
        }

        /// <summary>
        /// Retourne la propriété membre de la classe de type _classType dont le nom est _propertyName.
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

        /// <summary>
        /// Retourne la première propriété publique de type propertyType trouvée dans la class de type classType,
        /// ou null si non trouvée.
        /// </summary>
        public static PropertyInfo Property(Type classType, Type propertyType)
        {
            foreach(PropertyInfo _prInfo in classType.GetProperties())
            {
                if(_prInfo.PropertyType.IsPublic && _prInfo.PropertyType == propertyType)
                    return _prInfo;
            }
            return null;
        }

        /// <summary>
        /// Retourne true si le type est primitif : 
        /// Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single,
        /// ou Decimal, string, DateTime, TimeSpan.
        /// </summary>
        public static bool IsPrimitiveOrAlike(Type type)
        {
            return type.IsPrimitive
                || type == typeof(Decimal)
                || type == typeof(string)
                || type == typeof(DateTime)
                || type == typeof(TimeSpan);
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
