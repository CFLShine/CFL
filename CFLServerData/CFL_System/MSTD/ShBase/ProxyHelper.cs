using System;
using System.Reflection;

namespace MSTD.ShBase
{
    public static class ProxyHelper
    {
        public static ClassProxy ClassProxyFactory(ref ClassProxy proxy, Type t)
        {
            if(proxy == null)
                proxy = new ClassProxy();

            foreach(PropertyInfo _pr in t.GetProperties())
            {
                if(BaseHelper.IsMappableProperty(_pr))
                {
                    
                }
            }

            return proxy;
        }

        public static PropertyProxy PropertyProxyFactory(PropertyInfo prInfo)
        {
            if(prInfo.PropertyType.IsSubclassOf(typeof(Base)))
                return new PropertyObjectProxy(prInfo, null);
                

            return null;
        }
    }
}
