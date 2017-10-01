
using System;
using MSTD;

namespace RuntimeExec
{
    public abstract class REBase : Base
    {
        /// <summary>
        /// Pour les <see cref="REExpression"/>, <see cref="TypeName"/> est le nom du type
        /// de <see cref="REExpression.CValue"/>, la valeur CSharp.
        /// Par exemple, un <see cref="REClassObject"/> qui encapsule un objet CSharp aura
        /// pour <see cref="TypeName"/> le nom du type de l'objet encapsulé.
        /// </summary>
        public string TypeName { get; set; }

        public virtual REBase Parent 
        { 
            get => __parent; 
            set
            {
                __parent = value;
                if(__parent != null)
                    ParentTypeName = __parent.TypeName;
            }
        }
        private REBase __parent = null;

        public virtual string ParentTypeName { get ; set; }

        public abstract REBase Copy();

        public abstract REBase[] Children { get; }

        public REBase FindAncestorOfType(Type _ofType)
        {
            REBase _parent = Parent;
            while(_parent != null)
            {
                if(_parent.GetType() == _ofType)
                    return _parent;
                _parent = _parent.Parent;
            }
            return null;
        }

        #region 
        public static REValue NULL = new REValue(null);
        #endregion

        #region Helper methods

        /// <summary>
        /// Retourne true si l'objet est de type <see cref="Base"/> mais pas de type <see cref="REBase"/>.
        /// </summary>
        public static bool IsCustomClass(Type _t)
        {
            return _t != null && _t.IsSubclassOf(typeof(Base)) && (!_t.IsSubclassOf(typeof(REBase)));
        }

        #endregion
    }
}
