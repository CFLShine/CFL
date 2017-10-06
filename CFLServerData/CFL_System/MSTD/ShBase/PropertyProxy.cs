using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace MSTD.ShBase
{
    [DataContract]
    public abstract class PropertyProxy
    {
        public PropertyProxy()
        { }

        public PropertyProxy(PropertyInfo prInfo, ClassProxy parent)
        {
            PropertyInfo = prInfo;
            Parent = parent;
        }

        public abstract PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// La classe proxy dont fait partie ce membre
        /// </summary>
        public ClassProxy Parent
        {
            get;
            set;
        }

        [DataMember]
        /// <summary>
        /// Le type CSharp du membre représenté par ce <see cref="PropertyProxy"/>
        /// </summary>
        public string Type { get; set; }

        [DataMember]
        /// <summary>
        /// Le nom du membre, dans la classe CSharp 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        /// <summary>
        /// Le nom qui devra être affiché à l'utilisateur final
        /// pour représenter cette propriété.
        /// ex. string Name : Label = "Nom".
        /// </summary>
        public string Label { get; set; }

        [DataMember]
        /// <summary>
        /// La valeur est une copie de la valeur du membre de l'objet
        /// représenté par la classe Proxy.
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// Liste de valeurs possibles pour <see cref="Value"/>.
        /// Ceci peut être utile, par exemple, si cette <see cref="PropertyProxy"/>
        /// est utilisé par un control visuel, comme un combo, lequel se peuplera de ces valeurs.
        /// </summary>
        public List<object> Data { get; set; }

        /// <summary>
        /// Copie la valeur de la propriété représentée par ce
        /// <see cref="PropertyProxy"/> et l'affecte à <see cref="Value"/>.
        /// </summary>
        public abstract void UpdateValueFromProperty();

        /// <summary>
        /// Affecte <see cref="Value"/> à la propriété 
        /// </summary>
        public abstract void GiveValueToProperty(object value);
        
        protected PropertyInfo __prInfo = null;
    }

    [DataContract]
    public class PropertyPrimitiveProxy : PropertyProxy
    {
        public PropertyPrimitiveProxy()
        { }

        public  PropertyPrimitiveProxy(PropertyInfo prInfo, ClassProxy parent)
            :base(prInfo, parent)
        { }

        public override PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                Type = __prInfo.PropertyType.Name;
            }
        }

        public override void GiveValueToProperty(object value)
        {
            if(Parent == null)
                throw new Exception("Parent ne peut pas être null");
            if(Parent.Object == null)
                throw new Exception("Parent.Object ne peut pas être null");
        }

        public override void UpdateValueFromProperty()
        {
            throw new NotImplementedException();
        }
    }

    [DataContract]
    public class PropertyObjectProxy : PropertyProxy
    {
        public PropertyObjectProxy()
        { }

        public PropertyObjectProxy(PropertyInfo prInfo, ClassProxy parent)
            :base(prInfo, parent)
        { }

        public override PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                Type = __prInfo.PropertyType.Name;
            }
        }

        public override void GiveValueToProperty(object value)
        {
            throw new NotImplementedException();
        }

        public override void UpdateValueFromProperty()
        {
            throw new NotImplementedException();
        }
    }

    [DataContract]
    public class PropertyListProxy : PropertyProxy
    {
        public PropertyListProxy()
        { }

        public PropertyListProxy(PropertyInfo prInfo, ClassProxy parent)
            :base(prInfo, parent)
        { }

        [DataMember]
        public string ItemsType { get; set; }

        public override PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                Type = "list";
                ItemsType = BaseHelper.ListItemsType(__prInfo.PropertyType).Name;
            }
        }

        public override void GiveValueToProperty(object value)
        {
            throw new NotImplementedException();
        }

        public override void UpdateValueFromProperty()
        {
            throw new NotImplementedException();
        }
    }

    [DataContract]
    public class PropertyDictionaryProxy : PropertyProxy
    {
        public PropertyDictionaryProxy()
        { }

        public PropertyDictionaryProxy(PropertyInfo prInfo, ClassProxy parent)
            :base(prInfo, parent)
        { }

        [DataMember]
        public string KeysType { get; set; }

        [DataMember]
        public string ValuesType { get; set; }

        public override PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                Tuple<Type, Type> _keyValueTypes = BaseHelper.DictionaryKeysValuesTypes(__prInfo.PropertyType);
                KeysType = _keyValueTypes.Item1.Name;
                ValuesType = _keyValueTypes.Item2.Name;
            }
        }

        public override void GiveValueToProperty(object value)
        {
            throw new NotImplementedException();
        }

        public override void UpdateValueFromProperty()
        {
            throw new NotImplementedException();
        }
    }
}
