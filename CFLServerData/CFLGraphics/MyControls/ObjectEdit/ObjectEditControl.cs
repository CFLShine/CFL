using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using BoxLayouts;
using CFL_1.CFL_System.MSTD;
using MSTD;
using MSTD.ShBase;
using RuntimeExec;

namespace ObjectEdit
{
    public class ObjectEditControl : VBoxLayout
    {
        public ObjectEditControl(Base @object)
        {
            Object = @object;
        }

        public BoxLayout LayoutLabels{ get => __layoutLabels; }
        public BoxLayout LayoutControls{ get => __layoutControls; }

        public ObjectEditControl(ObjectEditControlLayout parent)
        {
            Init();
            ConfigsForPropertyClassControls = parent.ConfigsForPropertyClassControls;
            ControlsHeight = parent.ControlsHeight;
            __excludeds = parent.Excludeds;
            LabelsMinimumWidth = parent.LabelsMinimumWidth;
            LabelsMaximumWidth = parent.LabelsMaximumWidth;
            ControlsMinimumWidth = parent.ControlsMinimumWidth;
            ControlsMaximumWidth = parent.ControlsMaximumWidth;

            Object = parent.Object;
        }

        /// <summary>
        /// get : 
        /// Retourne la valeur qui a été donnée par un set,
        /// ou par défaut le nom du type de <see cref="Object"/>.
        /// </summary>
        public string Header
        {
            get =>(!string.IsNullOrWhiteSpace(__nameLabel))? __nameLabel : Object.GetType().Name;

            private set => __nameLabel = value;
        }

        public bool ShowHeader
        {
            get => __showHeader;
            set
            {
                __showHeader = value;
                if(__showHeader)
                    ShowHeaderIfNotDone();
                else
                    RemoveHeader();
            }
        }

        /// <summary>
        /// objet édité.
        /// set : provoque une exception si la valeur est null.
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

        public double LabelsMinimumWidth{ get; set; } = 0;
        public double LabelsMaximumWidth{ get; set; } = 0;
        public double ControlsMinimumWidth{ get; set; } = 0;
        public double ControlsMaximumWidth{ get; set; } = 0;

        /// <summary>
        /// Retourne une énumération des <see cref="ObjectEditControl"/> généré pour
        /// les propriété de type <see cref="Base"/> de <see cref="Object"/>.
        /// Ces <see cref="ObjectEditControl"/> sont fournis sans que leur fonction <see cref="Build"/> 
        /// n'est été appellée.
        /// Provoque une exeption si Build n'a pas été appelé ou si Object est null.
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

        public bool IsBuilt { get; private set; }

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
                        if(ConfigsForPropertyClassControls != null 
                        && ConfigsForPropertyClassControls.TryGetValue(_prInfo.PropertyType, out _config))
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
                                     LabelsMaximumWidth = LabelsMaximumWidth,
                                     ControlsMinimumWidth = ControlsMinimumWidth,
                                     ControlsMaximumWidth = ControlsMaximumWidth,
                                     ShowHeader = ShowHeader
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
            IsBuilt = true;
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
                MinWidth = LabelsMinimumWidth,
                MaxWidth = LabelsMaximumWidth,
                Background = Brushes.White,
                BorderThickness = new System.Windows.Thickness(1)
            };

            _editControl.Height = ControlsHeight;
            _editControl.MinWidth = ControlsMinimumWidth;
            _editControl.MaxWidth = ControlsMaximumWidth;

            __layoutLabels.Add(_label);
            __layoutControls.Add(_editControl);
        }

        private string GetLabel(PropertyInfo prInfo)
        {
            return PropertyHelper.GetNameToDisplay(prInfo);
        }

        private bool IsElligible(PropertyInfo prInfo)
        {
            Type _t = prInfo.PropertyType;

            DisplayAttribute _att = prInfo.GetCustomAttribute<DisplayAttribute>();
            if(_att != null && _att.GetAutoGenerateField() == false)
            {
                return false;
            }

            if(_t.IsPublic == false
            || prInfo.CanRead == false
            || prInfo.CanWrite == false)
                return false;

            foreach(string _name in __excludeds)
            {
                if(_name == prInfo.Name)
                    return false;
            }

            return true;
        }

        private void Init()
        {
            Clear();
            
            __labelHeader = null;
            if(ShowHeader)
                ShowHeaderIfNotDone();

            Background = Brushes.IndianRed;
            HBoxLayout _hLayout = new HBoxLayout(){ IsPerpendicularMinimized = true, Background = Brushes.Green };

            __layoutLabels = new VBoxLayout(){ IsPerpendicularMinimized = true, Background = Brushes.Chartreuse };
            __layoutControls = new VBoxLayout(){ IsPerpendicularMinimized = true, Background = Brushes.Chartreuse };

            _hLayout.Add(__layoutLabels);
            _hLayout.Add(__layoutControls);

            Add(_hLayout);
        }

        private void ShowHeaderIfNotDone()
        {
            if(__labelHeader == null)// sinon, déja ajouté
            {
                __labelHeader = new Label()
                { 
                    Content = Header, 
                    Height = 27,// ControlsHeight, 
                    Background = Brushes.Salmon 
                };

                Insert(0, __labelHeader);
            }
            if(__internalObjectEditControls != null)
            {
                foreach(ObjectEditControl _objectEditCtrl in InternalObjectEditControls())
                    _objectEditCtrl.ShowHeader = true;
            }
        }

        private void RemoveHeader()
        {
            //Remove(null) est permis
            Remove(__labelHeader);
            __labelHeader = null;

            if(__internalObjectEditControls != null)
            {
                foreach(ObjectEditControl _objectEditCtrl in InternalObjectEditControls())
                    _objectEditCtrl.ShowHeader = false;
            }
        }

        private VBoxLayout __layoutLabels = null;
        private VBoxLayout __layoutControls = null;
        private Label __labelHeader = null;

        private Base __object = null;

        private List<string> __excludeds = new List<string>();

        private double __controlsHeight = 27;
        private Dictionary<Type, PropertyEditControlConfig> ConfigsForPropertyClassControls{ get; set; } 
            = new Dictionary<Type, PropertyEditControlConfig>();
        private List<ObjectEditControl> __internalObjectEditControls { get; set; }

        private string __nameLabel = "";
        private bool __showHeader = false;
    }
}
