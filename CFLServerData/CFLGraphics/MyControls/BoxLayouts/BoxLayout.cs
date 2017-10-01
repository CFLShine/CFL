using System.Windows;
using System.Windows.Controls;

namespace BoxLayouts
{

    /// <summary>
    /// <see cref="BoxLayout"/> est un layout permetant d'aranger les éléments sur une ligne ou une colone.
    /// Pour imposer un espace entre deux éléments, ajouter un <see cref="Spacer"/>, 
    /// </summary>
    public class BoxLayout : Grid
    {
        public BoxLayout(Orientation _orientation)
        {
            Orientation = _orientation;
            Model = new BoxLayoutModel(this);
            Init();
            ShowGridLines = true;
        }

        public BoxLayoutModel Model { get; private set; }

        public new double MinWidth
        {
            get => base.MinWidth;
            set
            {
                if(!double.IsNaN(value))
                    base.MinWidth = value;
                Model.UpDate();
            }
        }

        public new double MinHeight
        {
            get => base.MinHeight;
            set
            {
                if(!double.IsNaN(value))
                    base.MinHeight = value;
                Model.UpDate();
            }
        }

        public Orientation Orientation
        {
            get;
            protected set;
        }

        public void Add(FrameworkElement e)
        {
            Model.Add(e);
        }

        public virtual void Insert(int index, FrameworkElement e)
        {
            Model.Insert(index, e);
        }

        public void Remove(FrameworkElement e)
        {
            Model.Remove(e);
        }

        public int Count
        {
            get => Model.Count;
        }

        public void Clear()
        {
            Model.Clear();
            Init();
        }

        private void Init()
        {
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    RowDefinitions.Add(new RowDefinition());
                    break;
                
                case Orientation.Vertical:
                    ColumnDefinitions.Add(new ColumnDefinition());
                    break;
            }
        }

    }

    public class VBoxLayout : BoxLayout
    {
        public VBoxLayout()
            :base(Orientation.Vertical)
        { }
    }

    public class HBoxLayout : BoxLayout
    {
        public HBoxLayout()
            :base(Orientation.Horizontal)
        { }
    }
}
