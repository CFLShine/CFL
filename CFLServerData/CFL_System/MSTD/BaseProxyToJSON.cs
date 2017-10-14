using System;
using System.Collections.Generic;
using System.Reflection;
using MSTD;
using MSTD.ShBase;
using Newtonsoft.Json;

namespace CFL_1.CFL_System.MSTD
{
    /*
{
 "Défunt": 
 {
  "Etat civil":
  {
   "Identite": 
   {
    "properties": 
    {
     "GUID": 
     {
      "label": "Nom",
      "type": "input/text",
     }
    }
   },
   "properties": 
   {
    "GUID2": 
    {
     "type":"input/numeric",
     "label":"Taille du sexe"
    },
    "GUID3": 
    {
     "type": "input/checkbox",
     "label": "Est mort"
    },
    "GUID4": 
    {
     "type": "list",
     "label": "Commune du décès",
     "data": 
     {
      "GUID5": "Aix-les-Bains",
      "GUID6": "Pugny-Chatenod"
     }
    }
   }
  }
 }
}
    retour de l'interface :

{
 "GUID": "valeur",
 "GUID2": "valeur"
}

https://www.w3schools.com/html/html_form_input_types.asp

    */

    public static class BaseProxyToJson
    {
        public static string ProduceJSon(ClassProxy _proxy)
        {
            if(_proxy == null)
                throw new ArgumentNullException("_proxy");

            Dictionary<string, object> _properties = ClassProxyRepresentation(_proxy, 0);
            Dictionary<string, object> _object = new Dictionary<string, object>()
            {
                { _proxy.Type, _properties }
            };

            string _json = JsonConvert.SerializeObject(_object);
            return _json;
        }

        private static Dictionary<string, object> ClassProxyRepresentation(ClassProxy _proxy, int _level)
        {
            Dictionary<string, object> _dic = new Dictionary<string, object>();

            _dic.Add("Level", _level);

            foreach(KeyValuePair<string, PropertyProxy> kvp in _proxy.Properties)
            {
                if(kvp.Value is PropertyObjectProxy _prObjectProxy)
                {
                    _dic.Add(_prObjectProxy.Name, ClassProxyRepresentation((ClassProxy)_prObjectProxy.Value, _level + 1));
                }
                else
                {
                    Dictionary<string, object> _prRepesentation = PropertyRepresentation(kvp.Value);
                    _dic.Add(kvp.Value.Name, _prRepesentation);
                }
            }
            return _dic;
        }

        private static Dictionary<string, object> PropertyRepresentation(PropertyProxy _prProxy)
        {
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("Guid", _prProxy.PropertyGuid.ToString());
            _dic.Add("Label", _prProxy.Label);
            _dic.Add("Type", _prProxy.TypeName);
            _dic.Add("Value", _prProxy.Value);
            return _dic;
        }
        
    }
}
