using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace MSTD.ShBase
{
    [DataContract]
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
            Object = _object ??throw new ArgumentNullException("_object");

            ClassProxy _proxy = this;
            ProxyHelper.ClassProxyFactory(ref _proxy, _object.GetType());
        }

        #endregion Constructors

        public Base Object { get; private set; }

        public ShContext Context{ get; set; }

        [DataMember]
        public Guid ID{ get; set; } = Guid.Empty;

        [DataMember]
        /// <summary>
        /// Le nom du type de la classe sur laquelle ce <see cref="ClassProxy"/> est construit.
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        [DataMember]
        public List<PropertyProxy> Members
        {
            get;
            set;
        } = new List<PropertyProxy>();

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
            if(Members != null)
                Members.Clear();
        }

    }
}
