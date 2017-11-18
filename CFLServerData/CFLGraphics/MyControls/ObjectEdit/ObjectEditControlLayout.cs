using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BoxLayouts;
using MSTD;
using MSTD.ShBase;
using RuntimeExec;

namespace ObjectEdit
{
    public enum ObjectsDisposition
    {
        HORIZONTAL,
        VERTICAL,
        TABS
    }

    public class ObjectEditControlLayout : ScrollViewer
    {
        public ObjectEditControlLayout(Base _object)
        {
            Object = _object;
            if(Object == null)
                throw new ArgumentNullException("_object");
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        public Base Object
        {
            get => __object;
            set
            {
                __object = value??throw new ArgumentNullException("value");
            }
        }
        private Base __object = null;

        public void Exclude(REMemberExpression _expr)
        {
            Exclude(_expr.LastMemberName());
        }
        public void Exclude(string _propertyName)
        {
            Excludeds.Add(_propertyName);
        }

        public ObjectsDisposition Disposition { get; set; }

        public double ControlsHeight { get; set; } = 27;
        
        public double LabelsMinimumWidth{ get; set; } = 0;
        public double LabelsMaximumWidth{ get; set; } = 0;
        public double ControlsMinimumWidth{ get; set; } = 0;
        public double ControlsMaximumWidth{ get; set; } = 0;

        public void SetConfigFor(Type _propertyType, List<Base> _objectsToDisplay, DataDisplay _dataDisplay)
        {
            PropertyClassEditControlConfig _config = new PropertyClassEditControlConfig()
                                                     { 
                                                        DataDisplay = _dataDisplay,
                                                        ObjectsToDisplay = _objectsToDisplay
                                                     };

            ConfigsForPropertyClassControls[_propertyType] = _config;
        }

        public void Build()
        {
            if(Object == null)
                throw new Exception("Object ne peut pas être null");

            Init();
            
            ObjectEditControl _objectEditControl = new ObjectEditControl(this);
            _objectEditControl.Build();
            
            if(_objectEditControl.HasRows)
            {
                __layoutObjects.Add(_objectEditControl);
            }

            foreach(ObjectEditControl _ctrl in _objectEditControl.InternalObjectEditControls())
            {
                ObjectEditTreeVisualiser _tree = new ObjectEditTreeVisualiser(_ctrl){ Indentation = 20 };
                _tree.Build();
                
                __layoutObjects.Add(_tree);
            }
        }

        /// <summary>
        /// Initialise le Content de this avec les layouts en fonction de Orientation.
        /// </summary>
        private void Init()
        {
            __layout = new VBoxLayout();

            FrameworkElement _objectsContainer = null;

            switch (Disposition)
            {
                case ObjectsDisposition.HORIZONTAL:
                    __layoutObjects = new HBoxLayout();
                    _objectsContainer = __layoutObjects;
                    break;
                case ObjectsDisposition.VERTICAL:
                    __layoutObjects = new VBoxLayout();
                    _objectsContainer = __layoutObjects;
                    break;
                case ObjectsDisposition.TABS:
                    __tabControl = new TabControl();
                    _objectsContainer = __tabControl;
                    break;
            }

            __labelHeader = new Label()
            {
                Background = Brushes.LightGray,
                Height = ControlsHeight,
                Content = GetHeader(Object)
            };
            __layout.Add(__labelHeader);
            __layout.Add(_objectsContainer);
            Content = __layout;
        }

        private string GetHeader(Base _object)
        {
            Type _t = _object.GetType();

            DisplayAttribute _att = Attribute.GetCustomAttribute(_t,
                                    typeof(DisplayAttribute)) as DisplayAttribute;
            if(_att != null )
            {
                if(!string.IsNullOrWhiteSpace(_att.Name))
                    return _att.Name;
            }
            return _t.Name;
        }

        public Dictionary<Type, PropertyEditControlConfig> ConfigsForPropertyClassControls{ get; private set; } = new Dictionary<Type, PropertyEditControlConfig>();
        public List<string> Excludeds { get; private set; } = new List<string>();

        private VBoxLayout __layout = null;
        private BoxLayout __layoutObjects = null;
        private TabControl __tabControl = null;
        private Label __labelHeader = null;
    }
}
