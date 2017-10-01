using System;
using System.Collections.Generic;
using BoxLayouts;

namespace ObjectEdit
{
    public class ObjectEditTreeVisualiser : VBoxLayout
    {
        public ObjectEditTreeVisualiser(ObjectEditControl _objectEditControl)
        {
            ObjectEditControl = _objectEditControl;
        }

        public double Indentation
        {
            get
            {
                if(double.IsNaN(__identation))
                    __identation = 0;
                return __identation;
            }
            set => __identation = value;
        }

        public ObjectEditControl ObjectEditControl
        {
            get; 
            set;
        }

        public void Build()
        {
            if(ObjectEditControl == null)
                throw new Exception("ObjectEditControl ne peut pas être null");

            ObjectEditControl.Build();
            Add(ObjectEditControl);

            foreach(ObjectEditControl _ctrl in ObjectEditControl.InternalObjectEditControls())
            {
                ObjectEditTreeVisualiser _tree = new ObjectEditTreeVisualiser(_ctrl);
                _tree.Indentation = Indentation;
                _tree.Build();
                
                HBoxLayout _hlayout = new HBoxLayout();

                _hlayout.Add(new FixedSpacer(Indentation));
                _hlayout.Add(_tree);

                Add(_hlayout);
            }
            Add(new Spacer());
        }

        private double __identation = 20;
    }
}
