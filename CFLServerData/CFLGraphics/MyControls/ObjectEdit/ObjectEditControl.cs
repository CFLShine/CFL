using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using BoxLayouts;
using MSTD;
using RuntimeExec;

namespace ObjectEdit
{
    public class ObjectEditControl : HBoxLayout
    {
        public ObjectEditControl(Base _object)
        {
            Init();
            Object = _object;
        }

        public ObjectEditControl(ObjectEditControlLayout _parent)
        {
            Init();
            ConfigsForPropertyClassControls = _parent.ConfigsForPropertyClassControls;
            ControlsHeight = _parent.ControlsHeight;
            __excludeds = _parent.Excludeds;
            LabelsMinimumWidth = _parent.LabelsMinimumWidth;
            ControlsMinimumWidth = _parent.ControlsMinimumWidth;
            Object = _parent.Object;
        }

        public string Header
        {
            get =>(!string.IsNullOrWhiteSpace(__nameLabel))? __nameLabel : Object.GetType().Name;

            private set => __nameLabel = value;
        }

        public bool ShowHeader { get; set; }

        /// <summary>
        /// objet édité.
        /// </summary>
        public Base Object
        {
            get => __object;
            set
            {
                __object = value??throw new ArgumentNullException("value");
            }
        }

        public void Exclude(REMemberExpression _expr)
        {
            Exclude(_expr.LastMemberName());
        }
        public void Exclude(string _propertyName)
        {
            __excludeds.Add(_propertyName);
        }

        public void SetConfigFor(Type _propertyType, List<Base> _objectsToDisplay, DataDisplay _dataDisplay)
        {
            PropertyClassEditControlConfig _config = new PropertyClassEditControlConfig()
                                                     { 
                                                        DataDisplay = _dataDisplay,
                                                        ObjectsToDisplay = _objectsToDisplay
                                                     };

            ConfigsForPropertyClassControls[_propertyType] = _config;
        }
        public void SetConfigFor(Type _propertyType, PropertyEditControlConfig _config)
        {
            ConfigsForPropertyClassControls[_propertyType] = _config;
        }

        public double ControlsHeight 
        { 
            get => __controlsHeight;
            set
            {
                __controlsHeight = value;
            }
        }

        public double LabelsMinimumWidth
        {
            get => __labelsMinimumWidth;
            set
            {
                __labelsMinimumWidth = value;
                if(!double.IsNaN(__labelsMinimumWidth))
                {
                    __layoutLabels.MinWidth = __labelsMinimumWidth;
                }
            }
        }
        public double ControlsMinimumWidth
        {
            get => __controlsMinimumWidth;
            set
            {
                __controlsMinimumWidth = value;
                if(!double.IsNaN(__controlsMinimumWidth))
                {
                    __layoutControls.MinWidth = __controlsMinimumWidth;
                }
            }
        }

        private double __labelsMinimumWidth = double.NaN;
        private double __controlsMinimumWidth = double.NaN;

        /// <summary>
        /// Retourne une énumération des <see cref="ObjectEditControl"/> généré pour
        /// les propriété de type <see cref="Base"/> de <see cref="Object"/>.
        /// Ces <see cref="ObjectEditControl"/> sont fournis sans que leur fonction <see cref="Build"/> 
        /// n'est été appellée.
        /// </summary>
        public IEnumerable<ObjectEditControl> InternalObjectEditControls()
        {
            if(__internalObjectEditControls == null)
                throw new Exception(@"__internalObjectEditControls est null. 
                                    Soit Build() n'a pas été appelé avant d'appeler cette fonction,
                                    soit Object est null.");

            foreach(ObjectEditControl _ctrl in __internalObjectEditControls)
                yield return _ctrl;
        }

        public bool HasRows
        {
            get=> RowCount > 0;
        }

        public int RowCount
        {
            get => __layoutLabels.Count;
        }

        /// <summary>
        /// Peuple le <see cref="EditControl"/>
        /// </summary>
        public void Build()
        {
            if(Object == null)
                throw new Exception("Object ne peut pas être null");

            Init();

            __internalObjectEditControls = new List<ObjectEditControl>();

            foreach(PropertyInfo _prInfo in Object.GetType().GetProperties())
            {
                if(IsElligible(_prInfo))
                {
                    PropertyEditControl _editControl = null;
                    PropertyEditControlConfig _config = null;

                    if(_prInfo.PropertyType.IsSubclassOf(typeof(Base)))
                    {
                        if(ConfigsForPropertyClassControls != null && ConfigsForPropertyClassControls.TryGetValue(_prInfo.PropertyType, out _config))
                        {
                            _editControl = new PropertyClassControl();
                        }
                        else
                        {
                            Base _object = (Base)_prInfo.GetValue(Object);
                            if(_object != null)
                            {
                                ObjectEditControl _objectEditControl = new ObjectEditControl(_object)
                                { 
                                     ConfigsForPropertyClassControls = ConfigsForPropertyClassControls,
                                     ControlsHeight = ControlsHeight,
                                     LabelsMinimumWidth = LabelsMinimumWidth,
                                     ControlsMinimumWidth = ControlsMinimumWidth
                                };
                                DisplayAttribute _att = _prInfo.GetCustomAttribute<DisplayAttribute>();
                                
                                if(_att != null && !string.IsNullOrWhiteSpace(_att.GetName()))
                                    _objectEditControl.Header = _att.GetName();

                                __internalObjectEditControls.Add(_objectEditControl);
                            }
                        }
                    }
                    else
                    {
                        _editControl = PropertyEditControl.Factory(_prInfo);
                        _config = new PropertyEditControlConfig();
                    }

                    if(_editControl != null)
                    {
                        _config.Object = Object;
                        _config.PropertyInfo = _prInfo;
                        _editControl.Config = _config;
                        AddControl(_editControl);
                    }
                }
            }
        }

        /// <summary>
        /// Provoque une exception si _editControl.Config == null, ou _editControl.Config.PropertyInfo == null
        /// </summary>
        public void AddControl(PropertyEditControl _editControl)
        {
            PropertyInfo _prInfo = _editControl.Config.PropertyInfo;

            if(_prInfo == null)
                throw new NullReferenceException("_prInfo");

            Label _label = new Label()
            { 
                Content = GetLabel(_prInfo),
                Height = ControlsHeight,
                Width = LabelsMinimumWidth,
                Background = Brushes.White,
                BorderThickness = new System.Windows.Thickness(1)
            };

            _editControl.Height = ControlsHeight;
            _editControl.Width = ControlsMinimumWidth;

            __layoutLabels.Add(_label);
            __layoutControls.Add(_editControl);
        }

        private string GetLabel(PropertyInfo _prInfo)
        {
            DisplayAttribute _att = _prInfo.GetCustomAttribute<DisplayAttribute>();
            if(_att != null )
            {
                if(!string.IsNullOrWhiteSpace(_att.Name))
                    return _att.Name;
            }
            
            return _prInfo.Name;
        }

        private bool IsElligible(PropertyInfo _prInfo)
        {
            Type _t = _prInfo.PropertyType;

            DisplayAttribute _att = _prInfo.GetCustomAttribute<DisplayAttribute>();
            if(_att != null && _att.GetAutoGenerateField() == false)
            {
                return false;
            }

            if(_t.IsPublic == false
            || _prInfo.CanRead == false
            || _prInfo.CanWrite == false)
                return false;

            foreach(string _name in __excludeds)
            {
                if(_name == _prInfo.Name)
                    return false;
            }

            return true;
        }

        private void Init()
        {
            __layoutLabels = new VBoxLayout(){ };
            __layoutControls = new VBoxLayout(){ };
            Add(new Glue());
            Add(__layoutLabels);
            Add(new Glue());
            Add(__layoutControls);
        }

        private VBoxLayout __layoutLabels;
        private VBoxLayout __layoutControls;

        private Base __object = null;

        private List<string> __excludeds = new List<string>();

        private double __controlsHeight = 27;
        private Dictionary<Type, PropertyEditControlConfig> ConfigsForPropertyClassControls{ get; set; } 
            = new Dictionary<Type, PropertyEditControlConfig>();
        private List<ObjectEditControl> __internalObjectEditControls { get; set; }

        private string __nameLabel = "";
    }
}
