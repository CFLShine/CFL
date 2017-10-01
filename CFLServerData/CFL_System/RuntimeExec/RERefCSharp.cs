using System;
using System.Reflection;
using MSTD;

namespace RuntimeExec
{
    public class RERefCSharp : REProperty
    {
        #region constructors

        public RERefCSharp(){ }

        public RERefCSharp(string _parentTypeName, string _memberName)
        {
            TypeName = _parentTypeName;
            MemberName = _memberName;
        }

        public RERefCSharp(REClassObject _parent, string _memberName)
        {
            Parent = _parent;
            MemberName = _memberName;
        }

        #endregion constructors

        public override REBase Copy()
        {
            return new RERefCSharp(TypeName, MemberName);
        }

        /// <summary>
        /// si <see cref="MemberInfo"/> ou <see cref="Parent"/> sont nuls,
        /// get retourne null,
        /// set provoque une <see cref="ArgumentNullException"/>
        /// </summary>
        public override REBase ReValue 
        { 
            get
            {
                return this;
            }

            set
            {
                if(value is REExpression _expr)
                    CValue = _expr.CValue;
                else
                    if(value == null)
                    throw new NullReferenceException("value");
                else
                    throw new Exception("Il n'est pas possible d'assigner un objet de type " + value.GetType().Name +
                                        " à un " + this.GetType().Name);
            }
        }

        /// <summary>
        /// Le set provoque une erreur si <see cref="Parent"/> == null.
        /// </summary>
        public override object CValue 
        { 
            get
            {
                REClassObject _object = Parent as REClassObject;
                if(_object == null || _object.CValue== null || string.IsNullOrWhiteSpace(MemberName))
                    return null;
                Type _cSharpType = _object.CValue.GetType();
                PropertyInfo _prInfo = _cSharpType.GetProperty(MemberName);
                if(_prInfo != null)
                    return _prInfo.GetValue(_object.CValue);
               
                FieldInfo _fldInfo = _cSharpType.GetField(MemberName);
                if(_fldInfo != null)
                    return (_fldInfo.GetValue(_object.CValue));
                throw new Exception("Le membre " + MemberName + " n'a pas été trouvé dans les champs ni propriétés du type " + _cSharpType.Name);
            }
            
            set
            {
                REClassObject _object = Parent as REClassObject;
                if(_object == null)
                    throw new NullReferenceException("Parent");
                if(_object.CValue == null)
                    throw new NullReferenceException("Parent.CValue");
                
                Type _cSharpType = _object.CValue.GetType();

                PropertyInfo _prInfo = _cSharpType.GetProperty(MemberName);
                if(_prInfo != null)
                    _prInfo.SetValue(_object.CValue, value);
                else
                {
                    FieldInfo _fldInfo = _cSharpType.GetField(MemberName);
                    if(_fldInfo != null)
                        _fldInfo.SetValue(_object.CValue, value);
                    else
                        throw new Exception("Le membre " + MemberName + " n'a pas été trouvé dans les membres du type " + _cSharpType.Name);
                }
                    
            }
        }
        
    }
}
