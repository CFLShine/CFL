using System;
using System.Reflection;
using CFL_1.CFL_System.MSTD;
using Newtonsoft.Json;

namespace MSTD.ShBase
{
    public static class ProxyHelper
    {
        public static ClassProxy ClassProxyFactory(ref ClassProxy proxy, Type t)
        {
            if(proxy == null)
                proxy = new ClassProxy();

            proxy.Type = t.Name;

            foreach(PropertyInfo _pr in t.GetProperties())
            {
                if(PropertyHelper.IsMappableProperty(_pr))
                {
                    proxy.Properties[_pr.Name] = PropertyProxyFactory(_pr, proxy);
                }
            }

            return proxy;
        }

        public static PropertyProxy PropertyProxyFactory(PropertyInfo prInfo, ClassProxy parent)
        {
            if(TypeHelper.IsPrimitiveOrAlike(prInfo.PropertyType))
                return new PropertyPrimitiveProxy(prInfo, parent);
            if(prInfo.PropertyType.IsSubclassOf(typeof(Base)))
                return new PropertyObjectProxy(prInfo, parent);
            if(TypeHelper.IsGenericList(prInfo.PropertyType))
                return new PropertyListProxy(prInfo, parent);
            if(TypeHelper.IsDictionary(prInfo.PropertyType))
                return new PropertyDictionaryProxy(prInfo, parent);
            if(prInfo.PropertyType.IsEnum)
                return new PropertyEnumProxy(prInfo, parent);
            
            throw new Exception("Le type " + prInfo.PropertyType.Name + " n'est pas pris en compte.");
        }

    }
}
