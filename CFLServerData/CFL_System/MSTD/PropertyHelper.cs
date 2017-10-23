
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MSTD
{
    public static class PropertyHelper
    {

        /// <summary>
        /// Verifie que cette propriété est publique et qu'elle pocède
        /// les accèsseurs get et set.
        /// </summary>
        public static bool IsExposedProperty(PropertyInfo prInfo)
        {
            Type _t = prInfo.PropertyType;

            return _t.IsPublic
                && prInfo.CanRead
                && prInfo.CanWrite;
        }

        /// <summary>
        /// Vérifie que cette propriété est publique,
        /// qu'elle n'a pas l'attribut [NotMapped] et
        /// qu'elle pocède les les accèsseurs get et set.
        /// </summary>
        public static bool IsMappableProperty(PropertyInfo prInfo)
        {
            return IsExposedProperty(prInfo)
                && prInfo.GetCustomAttribute<NotMappedAttribute>() == null;
        }

        /// <summary>
        /// Si le propriété a l'atribut DisplayAttribute, retourne DisplayAttribute.Name,
        /// sinon, retourne le nom de la propriété (PropertyInfo.Name).
        /// </summary>
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

        public static T GetAttribute<T>(PropertyInfo prInfo) where T : Attribute
        {
            return prInfo.GetCustomAttribute<T>();
        }
    }
}
