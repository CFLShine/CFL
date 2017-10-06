using System;
using System.Collections.Generic;

namespace MSTD.ShBase
{
    public class Set
    {
        public Set(Type type)
        {
            Type = type?? throw new ArgumentNullException("type");
        }

        /// <summary>
        /// Type des entités
        /// </summary>
        public Type Type
        {
            get;
            private set;
        }

        public ShContext Context { get; set; } = null;

        public ClassProxy Factory()
        {
            Base _entity = (Activator.CreateInstance(Type)) as Base;
            ClassProxy _proxy = new ClassProxy(_entity)
            {
                Context = Context
            };
            return _proxy;
        }

        /// <summary>
        /// Trouve ou ajoute au <see cref="Set"/> le <see cref="ClassProxy"/> construit
        /// sur entity.
        /// </summary>
        public ClassProxy Attach(Base entity)
        {
            ClassProxy _proxy = Proxy(entity);
            if(_proxy == null)
            {
                _proxy = new ClassProxy(entity)
                {
                    Context = Context
                };
            }
            return _proxy;
        }

        private ClassProxy Proxy(Base _entity)
        {
            ClassProxy _proxy = null;

            foreach(WeakReference<ClassProxy> _ref in __proxies)
            {
                if(_ref.TryGetTarget(out _proxy) && _proxy != null && _proxy.ID == _entity.ID)
                    return _proxy;
            }
            return null;
        }

        private List<WeakReference<ClassProxy>> __proxies = new List<WeakReference<ClassProxy>>();
    }
}
