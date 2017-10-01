using System;
using System.Collections.Generic;
using MSTD;

namespace SqlOrm
{
    public abstract class DBSet
    {
        public DBSet(Type _entitiesType)
        {
            EntitiesType = _entitiesType;
        }

        public DBContext Context
        {
            get;
            set;
        } = null;

        public Type EntitiesType
        {
            get;
            private set;
        }

        public ClassProxy Factory()
        {
            object _entity = (Activator.CreateInstance(EntitiesType));
            return new ClassProxy(_entity, Context);
        }

        /// <summary>
        /// Trouve ou ajoute au DBSet le ClassProxy correspondant à _entity,
        /// puis le retourne.
        /// N'est pas responsable de la mise à jour des entités contenues dans le DBContext 
        /// ni dans ce DBSet.
        /// </summary>
        public ClassProxy Attach(object _entity)
        {
            Guid _id = ObjectHelper.ID(_entity);
            ClassProxy _proxy = null;
            if(__classProxies.TryGetValue(_id, out _proxy) == false)
            {
                _proxy = new ClassProxy(_entity, Context);
                __classProxies[_id] = _proxy;
            }
            return _proxy;
        }

        /// <summary>
        /// Ajoute ou remplace _classProxy dans le DBSet (les ClassProxy sont rangés dans un Dictionary{Guid, ClassProxy}.
        /// N'est pas responsable de la mise à jour des entités contenues dans le DBContext 
        /// ni dans ce DBSet.
        /// </summary>
        public void Attach(Guid _id, ClassProxy _classAndProxy)
        {
            __classProxies[_id] = _classAndProxy;
        }

        public bool IsAttached(object _entity)
        {
            foreach(KeyValuePair<Guid, ClassProxy> _kvp in __classProxies)
            {
                if(_kvp.Value.Entity == _entity)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Met à jour les membres de classe et les listes de classes de l'entité
        /// de chaque ClassProxy en bouclant sur ses proxis de membres et de listes, 
        /// lesquels récupèrent les entités dans le DBContext si elles existent.
        /// </summary>
        public void UpdateClassProxiesEntities()
        {
            foreach(ClassProxy _classProxy in ClassProxies)
                _classProxy.UpdateEntity();
        }

        /// <summary>
        /// Retourne le nom de type de Entity.
        /// Concerve la casse.
        /// </summary>
        public string EntitiesTypeName
        {
            get
            {
                return EntitiesType.Name;
            }
        }

        public IEnumerable<object> Entities
        {
            get
            {
                foreach(KeyValuePair<Guid, ClassProxy> _kvp in __classProxies)
                    yield return _kvp.Value.Entity;
            }
        }

        public IEnumerable<ClassProxy> ClassProxies
        {
            get
            {
                foreach(KeyValuePair<Guid, ClassProxy> _kvp in __classProxies)
                    yield return _kvp.Value;
            }
        }

        public ClassProxy GetClassProxy(Guid _id)
        {
            ClassProxy _classProxy = null;
            __classProxies.TryGetValue(_id, out _classProxy);
            return _classProxy;
        }

        protected Dictionary<Guid, ClassProxy> __classProxies = new Dictionary<Guid, ClassProxy>();
    }

    public class DBSet<T> : DBSet where T : class
    {
        public DBSet()
            :base(typeof(T))
        { }
    }
}
