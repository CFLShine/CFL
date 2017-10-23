
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BoxLayouts;
using MSTD;
using MSTD.ShBase;
using RuntimeExec;

namespace ObjectEdit
{
    public abstract class PropertyEditControl : HBoxLayout
    {
        protected PropertyEditControl()
        {
            Init();
        }

        /// <summary>
        /// Procure un <see cref="PropertyEditControl"/> adapté au type de _prInfo,
        /// avec <see cref="PropertyEditControl.Config"/> == null.
        /// </summary>
        public static PropertyEditControl Factory(PropertyInfo _prInfo)
        {
            Type _t = _prInfo.PropertyType;

            if(!__propertiesTypes_controlsTypes.ContainsKey(_t))
                return null;

            PropertyEditControl _ctrl =  (PropertyEditControl)(Activator.CreateInstance(__propertiesTypes_controlsTypes[_t]));
            
            return _ctrl;
        }

        public object Value
        {
            get
            {
                if(Config != null)
                    return Config.Value;
                return null;
            }

            set
            {
                if(Config != null)
                    Config.Value = value;
            }
        }

        /// <summary>
        /// Produit le <see cref="BoxLayout"/>.
        /// </summary>
        protected abstract void Init();

        public abstract void Update();

        /// <summary>
        /// get : retourne Config
        /// set : garde Config,
        /// appelle <see cref="Update"/>
        /// </summary>
        public PropertyEditControlConfig Config
        {
            get => __config;
            set
            {
                __config = value;
                if(__config != null)
                {
                    __config.Control = this;
                }
            }
        }

        private static Dictionary<Type, Type> __propertiesTypes_controlsTypes = new Dictionary<Type, Type>()
        {
            { typeof(string)    , typeof(PropertyTextControl)     },
            { typeof(bool)      , typeof(PropertyBoolControl)     },
            { typeof(int)       , typeof(PropertyIntControl)      },
            { typeof(long)      , typeof(PropertyLongControl)     },
            { typeof(double)    , typeof(PropertyDoubleControl)   },
            { typeof(DateTime?) , typeof(PropertyDateControl)     },
            { typeof(TimeSpan?) , typeof(PropertyTimeSpanControl) }
        };

        private PropertyEditControlConfig __config = null;
    }

    public abstract class PropertyStringsControl : PropertyEditControl
    {
        protected void ReformatCurrentTextBox(char _default)
        {
            int _wantedLenght = __textBoxes[CurrentTextBox];
            //un caractère à été suprimé 
            if(CurrentTextBox.Text.Length < _wantedLenght)
            {
                int _index = CurrentTextBox.CaretIndex;
                CurrentTextBox.Text = CurrentTextBox.Text.Insert(_index, new string(_default, _wantedLenght - CurrentTextBox.Text.Length));
                CurrentTextBox.CaretIndex = _index;
            }
            else if(CurrentTextBox.Text.Length > _wantedLenght)
            {
                if(PreviewCaretIndex >= _wantedLenght) // caractères ajoutés à la fin
                {
                    CurrentTextBox.Text = CurrentTextBox.Text.Substring(0, _wantedLenght);
                }
                else  // caractères ajoutés avant la fin
                {
                    CurrentTextBox.Text = CurrentTextBox.Text.Remove(CurrentTextBox.CaretIndex, CurrentTextBox.Text.Length - _wantedLenght);
                }
                CurrentTextBox.CaretIndex = PreviewCaretIndex + 1;
            }
            CurrentTextBox.Text = CurrentTextBox.Text.PadRight(_wantedLenght, _default);
        }

        protected virtual void OnPreviewKeyDown(object sender, RoutedEventArgs e)
        {
            CurrentTextBox = sender as TextBox;
            PreviewCaretIndex = CurrentTextBox.CaretIndex;
        }

        protected void AddTextBox(TextBox _textBox, int _wantedLenght)
        {
            __textBoxes[_textBox] = _wantedLenght;
        }

        protected int WantedLenght(TextBox _textBox)
        {
            return __textBoxes[_textBox];
        }

        protected TextBox CurrentTextBox = null;
        protected int PreviewCaretIndex = 0;

        // dictionaire permetant de garder le nombre de caractères voulus par textbox.
        private Dictionary<TextBox, int> __textBoxes = new Dictionary<TextBox, int>();
    }

    public class PropertyTextControl : PropertyEditControl
    {
        protected override void Init()
        {
            Add(__textBox); 
            __textBox.KeyUp += OnKeyUp;
            __textBox.PreviewKeyDown += OnPreviewKeyDown;
        }

        public override void Update()
        {
            if(Value != null)
                __textBox.Text = Value.ToString();
            else
                __textBox.Text = "";
        }

        protected virtual void OnKeyUp(object sender, RoutedEventArgs e)
        {
            Value = __textBox.Text;
        }

        protected virtual void OnPreviewKeyDown(object sender, RoutedEventArgs e)
        {
            __currentTextBox = sender as TextBox;
            __previousCaretPosition = __textBox.CaretIndex;
        }

        protected TextBox __currentTextBox = null;
        protected TextBox __textBox = new TextBox(){ Background = Brushes.White };
        protected int __previousCaretPosition = 0;
    }

    public class PropertyBoolControl : PropertyEditControl
    {
        protected override void Init()
        {
            Add(__checkBox); 
            __checkBox.Click += Click;
        }

        protected virtual void Click(object sender, RoutedEventArgs e)
        {
            Value = __checkBox.IsChecked;
        }

        public override void Update()
        {
            object _value = Value;
            if(_value != null)
                __checkBox.IsChecked = (bool)_value;
            else
                __checkBox.IsChecked = false;
        }

        private CheckBox __checkBox = new CheckBox();
    }

    public class PropertyIntControl : PropertyTextControl
    {
        protected override void OnKeyUp(object sender, RoutedEventArgs e)
        {
            string _new = __textBox.Text;
            if(!int.TryParse(_new, out int _int))
            {
                Value = 0;
                Update();
                __textBox.CaretIndex = __previousCaretPosition;
            }
            else Value = _int;
        }
    }

    public class PropertyLongControl : PropertyTextControl
    {
        protected override void OnKeyUp(object sender, RoutedEventArgs e)
        {
            string _new = __textBox.Text;
            if(!long.TryParse(_new, NumberStyles.Any, CultureInfo.InvariantCulture, out long _long))
            {
                Value = 0;
                Update();
                __textBox.CaretIndex = __previousCaretPosition;
            }
            else Value = _long;
        }
    }

    public class PropertyDoubleControl : PropertyTextControl
    {
        protected override void OnKeyUp(object sender, RoutedEventArgs e)
        {
            string _new = __textBox.Text;
            if(!double.TryParse(_new, NumberStyles.Any, CultureInfo.InvariantCulture, out double _double))
            {
                Value = 0;
                Update();
                __textBox.CaretIndex = __previousCaretPosition;
            }
            else Value = _double;
        }

        public override void Update()
        {
            if(Value != null)
                __textBox.Text = Value.ToString().Replace(',', '.');
            else
                __textBox.Text = "";
        }
    }

    public class PropertyDateControl : PropertyStringsControl
    {
        protected override void Init()
        {
            __textBoxDay = new TextBox(){ Background = Brushes.White, Width = 25 };
            __textBoxMonth = new TextBox(){ Background = Brushes.White, Width = 25};
            __textBoxYear = new TextBox(){ Background = Brushes.White, Width = 50};

            AddTextBox(__textBoxDay, 2);
            AddTextBox(__textBoxMonth, 2);
            AddTextBox(__textBoxYear, 4);

            __labelSeparator1 = new Label()
            {
                Background = Brushes.White,
                Content = ":",
                Width = 13,
                HorizontalContentAlignment = HorizontalAlignment.Center, 
                VerticalContentAlignment = VerticalAlignment.Center
            };

            __labelSeparator2 = new Label()
            {
                Background = Brushes.White, 
                Content = ":",
                Width = 13,
                HorizontalContentAlignment = HorizontalAlignment.Center, 
                VerticalContentAlignment = VerticalAlignment.Center
            };

            Add(__textBoxDay);
            Add(__labelSeparator1);
            Add(__textBoxMonth);
            Add(__labelSeparator2);
            Add(__textBoxYear);

            Width = __textBoxDay.Width + 
                    __labelSeparator1.Width + 
                    __textBoxMonth.Width +
                    __labelSeparator2.Width +
                    __textBoxYear.Width; 

            __textBoxDay.PreviewKeyDown += OnPreviewKeyDown;
            __textBoxMonth.PreviewKeyDown += OnPreviewKeyDown;
            __textBoxYear.PreviewKeyDown += OnPreviewKeyDown;

            __textBoxDay.PreviewKeyUp += OnPreviewKeyUp;
            __textBoxMonth.PreviewKeyUp += OnPreviewKeyUp;
            __textBoxYear.PreviewKeyUp += OnPreviewKeyUp;
        }

        public override void Update()
        {
            if(Value is DateTime _date)
            {
                if(_date.Day < 10)
                    __textBoxDay.Text = "0" + _date.Day.ToString();
                else
                    __textBoxDay.Text = _date.Day.ToString();

                if(_date.Month < 10)
                    __textBoxMonth.Text = "0" + _date.Month.ToString();
                else
                    __textBoxMonth.Text = "0" + _date.Month.ToString();

                __textBoxYear.Text = _date.Year.ToString();
                __textBoxYear.Text = new string('0', __textBoxYear.Text.Length - 4) + __textBoxYear.Text;
            }
            else
            {
               Clear();
            }
        }

        protected new void Clear()
        {
            __textBoxDay.Text = "00";
            __textBoxMonth.Text = "00";
            __textBoxYear.Text = "0000";
            Value = null;
        }

        protected void OnPreviewKeyUp(object sender, RoutedEventArgs e)
        {
            ReformatCurrentTextBox('0');

            if(DateTime.TryParse(__textBoxDay.Text + "/" + __textBoxMonth.Text + "/" + __textBoxYear.Text, out DateTime _date))
            {
                Value = _date;
                return;
            }
            else Value = null;

            // caractère non numéricque tapé
            if( ! int.TryParse(__textBoxDay.Text, out int _day)
             || ! int.TryParse(__textBoxMonth.Text, out int _month)
             || ! int.TryParse(__textBoxYear.Text, out int _year)
             || (_day < 0 || _month < 0 || _year < 0)
               //date tapée entière mais non valide
             || (_day * _month * _year != 0)
             || (_day > 31 || _month > 12))
            {
                Clear();
                return;
            }

            if(((KeyEventArgs)e).Key == Key.Enter)
            {
                if(CurrentTextBox == __textBoxDay)
                {
                    if(_day != 0)
                    {
                        DateTime _now = DateTime.Now;
                        string _nowMonth = _now.Month.ToString();
                        string _nowYear = _now.Year.ToString();
                        if(DateTime.TryParse(__textBoxDay.Text + "/" + _nowMonth + "/" + _nowYear, out _date))
                        {
                            Value = _date;
                            Update();
                        }
                    }
                }
                else
                if(CurrentTextBox == __textBoxMonth)
                {
                    if(_day != 0 && _month != 0)
                    {
                        string _nowYear = DateTime.Now.Year.ToString();
                        if(DateTime.TryParse(__textBoxDay.Text + "/" + __textBoxMonth.Text + "/" + _nowYear, out _date))
                        {
                            Value = _date;
                            Update();
                        }
                    }
                }
            }

            if(((KeyEventArgs)e).Key == Key.Tab)
            {
                if(CurrentTextBox == __textBoxDay)
                    __textBoxMonth.Focus();
                else
                    if(CurrentTextBox == __textBoxMonth)
                    __textBoxYear.Focus();
            }
        }

        protected TextBox __textBoxDay = null;
        protected TextBox __textBoxMonth = null;
        protected TextBox __textBoxYear = null;
        protected Label __labelSeparator1 = null;
        protected Label __labelSeparator2 = null;
    }

    public class PropertyTimeSpanControl : PropertyStringsControl
    {
        protected override void Init()
        {
            __textBoxHours = new TextBox() { Background = Brushes.White, Width = 30 };
            __textBoxMinutes = new TextBox() { Background = Brushes.White, Width = 30 };
            
            __labelSeparator = new Label()
            { 
                Background = Brushes.White, 
                Content = ":",
                HorizontalContentAlignment = HorizontalAlignment.Center, 
                VerticalContentAlignment = VerticalAlignment.Center,
                Width = 13
            };

            AddTextBox(__textBoxHours, 2);
            AddTextBox(__textBoxMinutes, 2);

            Add(__textBoxHours);
            Add(__labelSeparator);
            Add(__textBoxMinutes);

            Width = __textBoxHours.Width + __labelSeparator.Width + __textBoxMinutes.Width;

            __textBoxHours.PreviewKeyDown += OnPreviewKeyDown;
            __textBoxMinutes.PreviewKeyDown += OnPreviewKeyDown;

            __textBoxHours.PreviewKeyUp += OnPreviewKeyUp;
            __textBoxMinutes.PreviewKeyUp += OnPreviewKeyUp;
        }

        public override void Update()
        {
            if(Value is TimeSpan _timeSpan)
            {
                __textBoxHours.Text = _timeSpan.Hours.ToString();
                __textBoxMinutes.Text = _timeSpan.Minutes.ToString();
            }
            else
            {
                Clear();
            }
        }

        protected new void Clear()
        {
            __textBoxHours.Text = "00";
            __textBoxMinutes.Text = "00";
            Value = null;
        }

        protected void OnPreviewKeyUp(object sender, RoutedEventArgs e)
        {
            if(((KeyEventArgs)e).Key == Key.Tab || ((KeyEventArgs)e).Key == Key.Enter)
            {
                if(CurrentTextBox == __textBoxHours)
                    __textBoxMinutes.Focus();
                e.Handled = true;
                return;
            }

            ReformatCurrentTextBox('0');

            if(!int.TryParse(CurrentTextBox.Text, out int _int)
            || (CurrentTextBox == __textBoxHours && _int > 23)
            || (CurrentTextBox == __textBoxMinutes && _int > 59))
            {
                Clear();
                CurrentTextBox.CaretIndex = PreviewCaretIndex;
            }
            else
            {
                int _hours = 0;
                int _minutes = 0;

                if(int.TryParse(__textBoxHours.Text, out int _h))
                        _hours = _h;
                else throw new Exception("Un cas n'a pas été prévu");

                if(int.TryParse(__textBoxMinutes.Text, out int _m))
                        _minutes = _m;
                else throw new Exception("Un cas n'a pas été prévu");

                Value = new TimeSpan(_hours, _minutes, 00);
            }
        }

        private TextBox __textBoxHours = null;
        private TextBox __textBoxMinutes = null;
        private Label __labelSeparator = null;
    }

    public class PropertyClassControl : PropertyEditControl
    {
        public PropertyClassControl(){ }

        protected void OnKeyUp(object sender, RoutedEventArgs e)
        {
            Search();
            PopulateCombo();
        }

        protected override void Init()
        {
            Add(__combobox);
            __combobox.IsEditable = true;
            __combobox.IsTextSearchEnabled = false;
            __combobox.KeyUp += OnKeyUp;
            __combobox.GotFocus += OnGotFocus;
            __combobox.PreviewMouseDown += OnMouseDown;

            // virtualisation du combobox
            __combobox.ItemsPanel = new ItemsPanelTemplate();
            FrameworkElementFactory stackPanelTemplate = new FrameworkElementFactory(typeof (VirtualizingStackPanel));
            __combobox.ItemsPanel.VisualTree = stackPanelTemplate;
        }

        public override void Update()
        {
            __objectsToDisplay = ((PropertyClassEditControlConfig)Config).ObjectsToDisplay;
            if(__items == null || __items.Count == 0)
            {
                BuildMemberExpressionsAndItems();
                PopulateCombo();
            }

            __combobox.SelectionChanged -= OnSelectionChanged;
            object _value = Value;
            if(_value != null)
            {
                if(!Value.GetType().IsSubclassOf(typeof(Base)))
                    throw new Exception("La propriété visée par ce control est de type " + _value.GetType().Name + " alors qu'elle devrait être de type Base.");
                int _index = ((PropertyClassEditControlConfig)Config).IndexOf((Base)_value);
                if(_index > -1)
                    __combobox.SelectedIndex = _index;
            }
            __combobox.SelectionChanged += OnSelectionChanged;
        }

        protected void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if(__combobox.SelectedIndex >= 0 && __combobox.SelectedIndex < ((PropertyClassEditControlConfig)Config).ObjectsToDisplay.Count)
                Value = ((PropertyClassEditControlConfig)Config).DisplayedObjectAt(__combobox.SelectedIndex);
        }

        protected void OnGotFocus(object sender, RoutedEventArgs e)
        {
            __combobox.IsDropDownOpen = true;
        }

        protected void OnMouseDown(object sender, RoutedEventArgs e)
        {
            if(__combobox.IsKeyboardFocusWithin)
                __combobox.IsDropDownOpen = true;
        }

        private void PopulateCombo()
        {
            __itemsToDisplay = new List<string>();
            if(__items != null && __objectsToDisplay != null)
            {
                foreach(Base _toDisplay in __objectsToDisplay)
                {
                    __itemsToDisplay.Add(__items[_toDisplay]);
                }
            }
            __combobox.ItemsSource = __itemsToDisplay;
            __combobox.SelectedIndex = -1;
        }

        #region Search

        private void Search()
        {
            __objectsToDisplay = new List<Base>();

            if(string.IsNullOrWhiteSpace(__combobox.Text))
            {
                __objectsToDisplay.AddRange(((PropertyClassEditControlConfig)Config).ObjectsToDisplay);
            }
            else
            {
                SelectByPertinence _pertinenceFinder = new SelectByPertinence();
                _pertinenceFinder.Seach(__combobox.Text, ((PropertyClassEditControlConfig)Config).ObjectsToDisplay, __memberExpressions);
                
                __objectsToDisplay.AddRange(_pertinenceFinder.ListPertinence1);
                __objectsToDisplay.AddRange(_pertinenceFinder.ListPertinence2);
                __objectsToDisplay.AddRange(_pertinenceFinder.ListPertinence3);
                __objectsToDisplay.AddRange(_pertinenceFinder.ListPertinence4);
                __objectsToDisplay.AddRange(_pertinenceFinder.ListPertinence5);
                __objectsToDisplay.AddRange(_pertinenceFinder.ListPertinence6);
            }
        }

        private void BuildMemberExpressionsAndItems()
        {
            __items = new Dictionary<Base, string>();
            __memberExpressions = new List<REMemberExpression>();

            if(Config is PropertyClassEditControlConfig _config && _config.IsComplete())
            {
                foreach(REExpression _expr in _config.DataDisplay.Elements)
                {
                    if(_expr is REMemberExpression _memberExpression)
                        __memberExpressions.Add(_memberExpression);
                }

                foreach(Base _base in _config.ObjectsToDisplay)
                {
                    _config.DataDisplay.Update(_base);
                    string _item = _config.DataDisplay.Display();
                    __items[_base] = _item;
                }
            }
        }

        #endregion Search

        // Les objets dont les membres désignés par les REMemberExpression sont à afficher dans le combobox.
        // Au départ, __objectToDisplay est la liste entière Config.ObjectsToDisplay,
        // après une recherche suite à un caractère tapé, __objectsToDisplay devient le résultat de la rechèrche.
        private List<Base> __objectsToDisplay = null;

        private Dictionary<Base, string> __items = null;

        private List<string> __itemsToDisplay = null;

        private List<REMemberExpression> __memberExpressions = null;

        ComboBox __combobox = new ComboBox();
    }

    public class PropertyEnumClontrol : PropertyEditControl
    {
        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void Init()
        {
            Add(__combobox);
            __combobox.IsEditable = true;
            __combobox.IsTextSearchEnabled = false;
            __combobox.KeyUp += OnKeyUp;
        }

        protected void OnKeyUp(object sender, RoutedEventArgs e)
        {

        }

        ComboBox __combobox = new ComboBox();
    }

}
