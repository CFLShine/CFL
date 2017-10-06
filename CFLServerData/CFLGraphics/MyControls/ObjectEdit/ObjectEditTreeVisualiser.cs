using System;
using BoxLayouts;

namespace ObjectEdit
{
    public class ObjectEditTreeVisualiser : VBoxLayout
    {
        public ObjectEditTreeVisualiser(ObjectEditControl _objectEditControl)
        {
            RootObjectEditControl = _objectEditControl??throw new ArgumentNullException("_objectEditControl");
            IsPerpendicularMinimized = true;
        }

        public bool ShowHeaders 
        { 
            get => RootObjectEditControl.ShowHeader; 
            set
            {
                RootObjectEditControl.ShowHeader = value;
            }
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

        public ObjectEditControl RootObjectEditControl
        {
            get; 
            set;
        }

        public bool IsBuild { get; private set; }

        public void Build()
        {
            if(RootObjectEditControl == null)
                throw new Exception("ObjectEditControl ne peut pas être null");

            Clear();
            
            if(!RootObjectEditControl.IsBuilt)
                RootObjectEditControl.Build();

            Add(RootObjectEditControl);

            foreach(ObjectEditControl _ctrl in RootObjectEditControl.InternalObjectEditControls())
            {
                ObjectEditTreeVisualiser _tree = new ObjectEditTreeVisualiser(_ctrl);
                _tree.Indentation = Indentation;
                _tree.Build();
                
                HBoxLayout _hlayout = new HBoxLayout(){ IsPerpendicularMinimized = true };

                _hlayout.Add(new FixedSpacer(Indentation));
                _hlayout.Add(_tree);

                Add(_hlayout);
            }
        }

        private double __identation = 20;
        private bool __showHeaders = false;
    }
}
