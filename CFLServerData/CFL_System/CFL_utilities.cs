using System;

public enum CFL_attributes
{
    REGISTERFORDB,
    REGISTERFORCODE
}

namespace CFL_1.CFL_System
{

    [AttributeUsage(AttributeTargets.Class)]
    public class NoCFLAttribute : Attribute
    {
        public NoCFLAttribute() { }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterForDB : Attribute
    {
        public RegisterForDB() { }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterForCode : Attribute
    {
        public RegisterForCode() { }
    }

}
