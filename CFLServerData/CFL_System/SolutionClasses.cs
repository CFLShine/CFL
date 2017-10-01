using System;
using System.Collections.Generic;
using System.Reflection;
using MSTD;

namespace CFL_1.CFL_System
{
    public class SolutionClasses
    {
        public static Type ClassType(string _typename)
        {
            return (Instance.__classes.TryGetValue(_typename, out Type _type)) ? _type : null;
        }

        public static Base ClassInstance(string _typename)
        {
            Type _type = ClassType(_typename);
            return (_type != null) ? (Base)(Activator.CreateInstance(_type)) : null;
        }

        public static Dictionary<string, Type>.ValueCollection ClassesTypes()
        {
            return Instance.__classes.Values;
        }

        private Dictionary<string, Type> __classes = new Dictionary<string, Type>();
        
        private void Init()
        {
            Type[] _types = Assembly.GetExecutingAssembly().GetTypes();
            foreach(Type _type in _types)
            {
                if(_type.IsSubclassOf(typeof(Base)))
                    __classes.Add(_type.Name, _type);
            }
        }

        private static SolutionClasses Instance
        {
            get
            {
                if(__instance == null)
                    __instance = new SolutionClasses();
                return __instance;
            }
        }

        private static SolutionClasses __instance;
        private SolutionClasses() 
        {
            Init();    
        }
    }
}
