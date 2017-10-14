using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MSTD.ShBase
{
    public abstract class PropertyProxy
    {
        public PropertyProxy()
        { }

        public PropertyProxy(PropertyInfo prInfo, ClassProxy parent)
        {
            PropertyInfo = prInfo;
            Parent = parent;
        }

        /// <summary>
        /// Chaque classe fille surcharge PropertyInfo pour renseigner
        /// <see cref="Name"/> et 
        /// <see cref="TypeName"/> et événtuèlement d'autre propriétés de type.
        /// <see cref="PropertyPrimitiveProxy"/> et <see cref="PropertyObjectProxy"/>: <see cref="TypeName"/> = le type c#,
        /// <see cref="PropertyListProxy"/> et <see cref="PropertyDictionaryProxy"/> renseignent aussi
        /// le type des items, ou des clés et valeurs.
        /// </summary>
        public abstract PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// La classe proxy dont fait partie ce membre
        /// </summary>
        public ClassProxy Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Dans le cas d'une propriété object, il s'agit de <see cref="Base.ID"/>,
        /// autrement, un nouveau Guid est attribué à cette propriété, ce qui peut 
        /// permettre, par ex., de la retrouver lors d'échanges avec un système non c#.
        /// </summary>
        public Guid PropertyGuid{ get; set; } = Guid.NewGuid();

        /// <summary>
        /// Le nom qui devra être affiché à l'utilisateur final
        /// pour représenter cette propriété.
        /// ex. string Name : Label = "Nom" si un DisplayAttribute est attaché à
        /// la propriété avec Name = "Nom", sinon, "Name", le nom de la propriété.
        /// </summary>
        public string Label 
        { 
            get => PropertyHelper.GetNameToDisplay(__prInfo);
        }

        /// <summary>
        /// Le type CSharp du membre représenté par ce <see cref="PropertyProxy"/>.
        /// Il est automatiquement renseigné lorsque PropertyInfo est donné dans une classe fille.
        /// Dans le cas d'une liste, <see cref="TypeName"/> == "List", un dictionnaire <see cref="TypeName"/> == "Dictionary",
        /// un DateTime ou DateTime?, un TimeSpan ou TimeSpan?, "Date" et "Time",
        /// pour un type primitif ou object (<see cref="Base"/>), le type c#.
        /// </summary>
        public string TypeName { get; protected set; }

        /// <summary>
        /// Le nom du membre, dans la classe CSharp.
        /// </summary>
        public string Name
        {
            get => __name;
            set
            {
                __name = value;
                if(Parent != null 
                && Parent.Entity != null)
                {
                    PropertyInfo _prInfo = PropertyHelper.Property(Parent.Entity.GetType(), value);
                    PropertyInfo = _prInfo??throw new Exception("La propriété " + value + " n'existe pas dans le type " + Parent.Entity.GetType().Name);
                }
            }
        }

        /// <summary>
        /// La valeur est une copie de la valeur du membre de l'objet
        /// représenté par la classe Proxy.
        /// <see cref="PropertyObjectProxy"/> surcharge <see cref="Value"/>
        /// qui est un objet de classe proxy de ce type. Ce proxy sera cherché
        /// dans le Context ou instancié si non trouvé.
        /// </summary>
        public virtual object Value
        {
            get => __value;
            set => __value = value;
        }

        /// <summary>
        /// Liste de valeurs possibles pour <see cref="Value"/>.
        /// Ceci peut être utile, par exemple, si cette <see cref="PropertyProxy"/>
        /// est utilisé par un control visuel, comme un combo, lequel se peuplera de ces valeurs.
        /// </summary>
        public Dictionary<Guid, object> Data { get; set; } = new Dictionary<Guid, object>();

        /// <summary>
        /// Copie la valeur de la propriété représentée par ce
        /// <see cref="PropertyProxy"/> et l'affecte à <see cref="Value"/>.
        /// Provoque une exeption si <see cref="Parent"/> est null 
        /// ou si (<see cref="Parent"/>.)<see cref="ClassProxy.Entity"/> est null.
        /// </summary>
        public abstract void CopyValueFromEntity();

        /// <summary>
        /// Affecte <see cref="Value"/> à la propriété 
        /// </summary>
        public abstract void GiveValueToEntity();
        
        protected PropertyInfo __prInfo = null;
        private string __name = "";

        protected object __value = null;
    }

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
                Name = __prInfo.Name;

                if(__prInfo.PropertyType == typeof(DateTime) || __prInfo.PropertyType == typeof(DateTime?))
                    TypeName = "DateTime";
                else
                if(__prInfo.PropertyType == typeof(TimeSpan) || __prInfo.PropertyType == typeof(TimeSpan?))
                    TypeName = "TimeSpan";
                else
                    TypeName = __prInfo.PropertyType.Name;
            }
        }

        public override void GiveValueToEntity()
        {
            if(Parent == null)
                throw new Exception("Parent ne peut pas être null");
            if(Parent.Entity == null)
                throw new Exception("Parent.Object ne peut pas être null");
            if(PropertyInfo == null)
                throw new Exception("PropertyInfo ne peut pas être null.");
            PropertyInfo.SetValue(Parent.Entity, Value);
        }

        public override void CopyValueFromEntity()
        {
            throw new NotImplementedException();
        }
    }

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
                Name = __prInfo.Name;
                TypeName = __prInfo.PropertyType.Name;
            }
        }

        public override object Value
        {
            get
            {
                if(__value == null)
                {
                    if(PropertyInfo == null)
                        throw new Exception("Cette propriété doit retourner un ClassProxy.Impossible de retourner une valeur car PropertyInfo est null.");
                    __value = new ClassProxy(PropertyInfo.PropertyType);
                }
                
                return __value;
            }
        }

        public override void GiveValueToEntity()
        {
            throw new NotImplementedException();
        }

        public override void CopyValueFromEntity()
        {
            if(PropertyInfo == null)
            {

            }
            else
            {

            }
        }

    }

    public class PropertyListProxy : PropertyProxy
    {
        public PropertyListProxy()
        { }

        public PropertyListProxy(PropertyInfo prInfo, ClassProxy parent)
            :base(prInfo, parent)
        { }

        public Type ItemsType { get; private set; }

        public string ItemsTypeName { get; set; }

        public override PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                Name = __prInfo.Name;
                TypeName = "list";
                ItemsType = TypeHelper.ListItemsType(__prInfo.PropertyType);
                ItemsTypeName = ItemsType.Name;
            }
        }

        public override void GiveValueToEntity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Si les items de la liste sont de type primitif, 
        /// ils sont copiés dans la liste proxy,
        /// si ils sont de type dérivé de <see cref="Base"/>,
        /// leurs ID est copié.
        /// </summary>
        public override void CopyValueFromEntity()
        {
            if(Parent == null)
                throw new Exception("Parent ne peut pas être null");
            if(Parent.Entity == null)
                throw new Exception("Parent.Object ne peut pas être null");
            if(PropertyInfo == null)
                throw new Exception("PropertyInfo ne peut pas être null.");
            
            List<object> _copy = new List<object>();

            IEnumerable enumerable = PropertyInfo.GetValue(Parent.Entity) as IEnumerable;
            if(enumerable != null)
            {
                if(TypeHelper.IsPrimitiveOrAlike(ItemsType))
                {
                    foreach (object item in enumerable)
                    {
                        _copy.Add(item);
                    }
                }
                else
                if(ItemsType.IsSubclassOf(typeof(Base)))
                {
                    foreach(object item in enumerable)
                    {
                        if(item != null)
                            _copy.Add(((Base)item).ID);
                    }
                }
            }
            Value = _copy;
        }
    }

    public class PropertyDictionaryProxy : PropertyProxy
    {
        public PropertyDictionaryProxy()
        { }

        public PropertyDictionaryProxy(PropertyInfo prInfo, ClassProxy parent)
            :base(prInfo, parent)
        { }

        public Type KeysType { get; private set; }
        public Type ValuesType { get; private set; }

        public string KeysTypeName { get; set; }

        public string ValuesTypeName { get; set; }

        public override PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                Name = __prInfo.Name;
                Tuple<Type, Type> _keyValueTypes = TypeHelper.DictionaryKeysValuesTypes(__prInfo.PropertyType);
                KeysType = _keyValueTypes.Item1;
                ValuesType = _keyValueTypes.Item2;
                KeysTypeName = KeysType.Name;
                ValuesTypeName = ValuesType.Name;
            }
        }

        public override void GiveValueToEntity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Si les clés ou les valeurs sont de type primitif, 
        /// elles sont copiées dans de dictionaire proxy,
        /// si elles sont de type dérivé de <see cref="Base"/>,
        /// c'est leur ID qui est copié.
        /// </summary>
        public override void CopyValueFromEntity()
        {
            throw new NotImplementedException();
        }
    }

    public class PropertyEnumProxy : PropertyProxy
    {
        public PropertyEnumProxy()
        { }

        public  PropertyEnumProxy(PropertyInfo prInfo, ClassProxy parent)
            :base(prInfo, parent)
        { }

        public override PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                Name = __prInfo.Name;
                TypeName = __prInfo.PropertyType.Name;
                
                foreach(var v in Enum.GetNames(__prInfo.PropertyType))
                {
                    Data.Add(Guid.NewGuid(), v);
                }
            }
        }

        public override void CopyValueFromEntity()
        {
            throw new NotImplementedException();
        }

        public override void GiveValueToEntity()
        {
            throw new NotImplementedException();
        }
    }
}
