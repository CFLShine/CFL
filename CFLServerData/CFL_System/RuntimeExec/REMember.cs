
using System;

namespace RuntimeExec
{
    /// <summary>
    /// <see cref="REMember"/> est la classe abstraite dont héritent 
    /// les classes qui expriment un membre Property ou Field.
    /// Elle contientla propriété <see cref="Parent"/>, le <see cref="REClassObject"/>
    /// qui contient ce membre.
    /// </summary>
    public abstract class REMember : REExpression
    {
        public REMember(){}

        public REMember(string _memberName) => MemberName = _memberName;

        /// <summary>
        /// Le <see cref="REClassObject"/> dont fait partie ce <see cref="REMember"/>.
        /// Cette propriété est identique à <see cref="REBase.Parent"/> 
        /// sauf le set qui provoque une exception si value n'est pas de type <see cref="REClassObject"/>.
        /// </summary>
        public override REBase Parent 
        { 
            get => __parent;
            
            set
            {
                __parent = value;
                
                if(__parent != null)
                {
                    if((value is REClassObject _classObject) == false)
                        throw new Exception
                        ("Seul un " + typeof(REClassObject).Name + 
                        " peut être donné comme parent à un " + 
                        typeof(REMember).Name);
                    else
                    {
                        ParentTypeName = __parent.TypeName;
                    }
                }
            }
        }

        public string MemberName { get; set; }

        public override REBase[] Children
        {
            get
            {
                return new REBase[0];
            }
        }

        /// <summary>
        /// Donne _object à <see cref="Parent"/> si <see cref="REBase.ParentTypeName"/> != ""
        /// et _object.TypeName == <see cref="REBase.ParentTypeName"/>.
        /// </summary>
        public override REExpression Update(REClassObject _object)
        {
            if(_object != null)
            {
                if(!string.IsNullOrWhiteSpace(ParentTypeName) && _object.TypeName == ParentTypeName)
                    Parent = _object;
            }
            return this;
        }

        private REBase __parent = null;
    }
}
