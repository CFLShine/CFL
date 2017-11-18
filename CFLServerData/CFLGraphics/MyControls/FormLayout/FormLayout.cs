using System.Windows;
using System.Windows.Controls;

namespace CFL_1.CFLGraphics
{
    public class FormLayout : Grid
    {
        public FormLayout()
        {
            init();
        }

        public virtual void Clear()
        {
            Children.Clear();
            ColumnDefinitions.Clear();
            RowDefinitions.Clear();
            init();
        }

        public void Append(Label _label, FrameworkElement _element, int _height = 27)
        {
            _label.Height = _height;

            int _count = RowDefinitions.Count;
            RowDefinition _row = new RowDefinition();
            RowDefinitions.Add(_row);

            SetRow(_label, _count);
            SetColumn(_label, 0);
            SetRow(_element, _count); 
            SetColumn(_element, 1);

            _row.Height = new GridLength(_height);
            _label.Height = _height;
            _element.Height = _height;

            Children.Add(_label);
            Children.Add(_element);
        }

        public void Append(string _label, FrameworkElement _element, int _height = 27)
        {
            Label _l = new Label() { Content = _label };
            Append(_l, _element, _height);
        }

        public int Count
        {
            get
            {
                return RowDefinitions.Count;
            }
        }

        public void SetHeightToContent()
        {
            MaxHeight = MinHeight;
        }

        //private:

        private void init()
        {
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
        }
    }
}
