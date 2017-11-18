using MyControls;
using System;
using System.Windows.Controls;

namespace CFL_1.CFLGraphics
{
    /// <summary>
    /// Hérite de <seealso cref="ObjectTreeEditor"/>.
    /// Permet de visualiser dans un arbre une CFL_form, ses propriétés et champs de type SC_object et Control.
    /// </summary>
    public class ctrl_CFL_form_treeEditor : ObjectTreeEditor
    {
        public ctrl_CFL_form_treeEditor(bool _editable, bool _includePrimitivesInRender)
            :base(_editable, _includePrimitivesInRender)
        {
            init();
        }

        private void init()
        {
        }

        private void method_sc_primitive (ref object _object, string _value)
        {
        }
    }
}
