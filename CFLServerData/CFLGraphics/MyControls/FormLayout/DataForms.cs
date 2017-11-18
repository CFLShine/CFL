using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MSTD.ShBase;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;

namespace CFL_1.CFLGraphics.MyControls.FormLayout
{
    public class DataForms : RadLayoutControl
    {
        public DataForms()
        {}

        public void Clear()
        {
            Items.Clear();
        }

        public void AddObject(Base _object)
        {
            DisplayAttribute _display = _object.GetType().GetCustomAttribute<DisplayAttribute>();
            if(_display != null && _display.AutoGenerateField == false)
                return;

            RadDataForm _dataForm = new RadDataForm();
            this.AddChild(_dataForm);
            _dataForm.AutoGeneratingField += AutoGeneratingField;
            _dataForm.AutoEdit = true;
            _dataForm.CommandButtonsVisibility = DataFormCommandButtonsVisibility.None;
            _dataForm.CurrentItem = _object;

            foreach(PropertyInfo _pr in _object.GetType().GetProperties())
            {
                _display = _pr.GetCustomAttribute<DisplayAttribute>();
                if(_display == null || _display.AutoGenerateField == true)
                {
                    object _propertyValue = _pr.GetValue(_object);
                    if(_propertyValue is Base _base)
                    {
                        AddObject(_base);
                    }
                    else
                    if(_propertyValue is ICollection _collection)
                    {
                        foreach(object _o in _collection)
                        {
                            if(_o is Base _b)
                                AddObject(_b);
                        }
                    }
                }
            }
        }
        
        private void AutoGeneratingField(object sender, AutoGeneratingFieldEventArgs e)
        {
            object _component = ((RadDataForm)sender).CurrentItem;

            PropertyInfo _pr = _component.GetType().GetProperty(e.PropertyName);

            if(_pr == null 
            || _pr.CanWrite == false)
            {
                e.Cancel = true;
                return;
            }

            DisplayAttribute _display = _pr.GetCustomAttribute<DisplayAttribute>();

            if(_display != null && _display.AutoGenerateField == false)
            {
                e.Cancel = true;
                return;
            }

            if(e.PropertyType.IsNotPublic
            || e.PropertyType.IsSubclassOf(typeof(Base))
            || e.PropertyType.GetInterface("ICollection") != null)
            {
                e.Cancel = true;
                return;
            }
        }
    }

    public class DataFormTimeSpanField : DataFormDateField
    {
        public DataFormTimeSpanField()
        {
            var _control = this.GetControl();
        }
    }
}
