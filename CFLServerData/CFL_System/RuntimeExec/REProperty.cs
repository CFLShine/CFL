
using System;

namespace RuntimeExec
{
    /// <summary>
    /// TODO faire la différence entre une REProperty et un REField
    /// </summary>
    public class REProperty : REMember
    {
        public REProperty(){}

        public REProperty(REBase _value, string _memberName)
        {
            ReValue = _value;
            MemberName = _memberName;
        }

        public REProperty(string _parentTypeName, string _memberName)
        {
            ParentTypeName = _parentTypeName; 
            MemberName = _memberName; 
        }

        public override REBase Copy()
        {
            return new REProperty(ParentTypeName, MemberName);
        }

        public override REBase ReValue 
        { 
            get
            {
                if(__value is REBase _base)
                    return _base;
                return this;
            }
            set
            {
                if(__value is REExpression _expr)
                {
                    _expr.ReValue = value;
                }
                else
                {
                    __value = value;
                }
                if(__value is REClassObject _classObject)
                    Parent = _classObject;
            }
        }

        public override object CValue
        {
            get
            {
                if(__value is REExpression _expr)
                    return _expr.CValue;
                else
                return null;
            }

            set
            {
                if(__value is REExpression _expr)
                    _expr.CValue = value;
                else
                    throw new Exception("Cette propriété désigne un objet de type " + __value.GetType().Name +
                                    " auquel une valeur CSharpe ne peut être assignée.");
            }
        }

        public override REExpression Invoke()
        {
            // do nothing
            return this;
        }

        private REBase __value;
    }
}
