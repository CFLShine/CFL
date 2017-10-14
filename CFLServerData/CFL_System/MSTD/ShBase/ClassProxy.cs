using System;
using System.Collections.Generic;

namespace MSTD.ShBase
{
    public class ClassProxy
    {
        #region Constructors

        public ClassProxy(){ }

        public ClassProxy(Type classType)
        {
            ClassProxy _proxy = this;
            ProxyHelper.ClassProxyFactory(ref _proxy, classType);
        }

        public ClassProxy(Base _object)
        {
            ClassProxy _proxy = this;
            ProxyHelper.ClassProxyFactory(ref _proxy, _object.GetType());
            Entity = _object ??throw new ArgumentNullException("_object");
        }

        #endregion Constructors

        public Base Entity { get; private set; }

        public ShContext Context{ get; set; }

        public Guid ID{ get; set; } = Guid.Empty;

        /// <summary>
        /// Le nom du type de la classe sur laquelle ce <see cref="ClassProxy"/> est construit.
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        public Dictionary<string, PropertyProxy> Properties
        {
            get;
            set;
        } = new Dictionary<string, PropertyProxy>();

        public void UpdateProxyValues()
        {

        }
        
        public void UpdateEntityValues()
        {

        }

        /// <summary>
        /// Réinitialise ce <see cref="ClassProxy"/> en vidant sa liste
        /// de membres et en affectant Guid.Empty à <see cref="ID"/>;
        /// </summary>
        public void Clear()
        {
            ID = Guid.Empty;
            if(Properties != null)
                Properties.Clear();
        }

    }
}
