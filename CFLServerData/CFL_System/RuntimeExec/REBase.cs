
using System;
using MSTD.ShBase;

namespace RuntimeExec
{
    public abstract class REBase : Base
    {
        public static REValue NULL = new REValue(null);

        /// <summary>
        /// Pour les <see cref="REExpression"/>, <see cref="TypeName"/> est le nom du type
        /// de <see cref="REExpression.CValue"/>, la valeur CSharp.
        /// Par exemple, un <see cref="REClassObject"/> qui encapsule un objet CSharp aura
        /// pour <see cref="TypeName"/> le nom du type de l'objet encapsulé.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// <see cref="Parent"/> est le <see cref="REBase"/> dont celui-ci fait partie.
        /// Permet de remonter l'arbre jusqu'à l'ancêtre racine.
        /// </summary>
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
        
        public virtual string ParentTypeName { get ; set; }

        public abstract REBase[] Children { get; }

        public abstract REBase Copy();

        /// <summary>
        /// Retourne le premier ancêtre trouvé de type t
        /// </summary>
        public REBase FindAncestorOfType(Type t)
        {
            REBase _parent = Parent;
            while(_parent != null)
            {
                if(_parent.GetType() == t)
                    return _parent;
                _parent = _parent.Parent;
            }
            return null;
        }

        #region Helper methods

        /// <summary>
        /// Retourne true si l'objet est de type <see cref="ShBase"/> 
        /// mais pas de type <see cref="REBase"/>.
        /// </summary>
        public static bool IsCustomClass(Type t)
        {
            return t != null 
                && t.IsSubclassOf(typeof(Base)) 
                && (!t.IsSubclassOf(typeof(REBase)));
        }

        #endregion

        private REBase __parent = null;
    }
}
