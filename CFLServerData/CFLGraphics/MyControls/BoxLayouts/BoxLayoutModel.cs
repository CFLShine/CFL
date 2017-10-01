using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BoxLayouts
{
    public class BoxLayoutModel
    {
        public BoxLayoutModel(BoxLayout layout)
        {
            Layout = layout;
        }

        #region public methods

        public bool Updating { get; private set; }

        public void Add(FrameworkElement e)
        {
            Insert(Count, e);
        }

        public void Insert(int index, FrameworkElement e)
        {
            switch (Layout.Orientation)
            {
                case Orientation.Horizontal:
                    InsertHorizontal(index, e);
                    break;
                case Orientation.Vertical:
                    InsertVertical(index, e);
                    break;
            }

            if(e is Spacer _spacer && double.IsPositiveInfinity(_spacer.MaxSpace))
                ++ SpacersInfinite; 

            UpDate();
        }

        public void Remove(FrameworkElement e)
        {
            int _index = IndexOf(e);
            if(RemoveAt(_index))
            {
                if(e is Spacer _spacer && double.IsPositiveInfinity(_spacer.MaxSpace))
                    -- SpacersInfinite;
                UpDate();
            }
        }

        public void Clear()
        {
            Layout.Children.Clear();
            UpDate();
        }

        public int Count
        {
            get
            {
                return Layout.Children.Count;
            }
        }

        public int IndexOf(FrameworkElement e)
        {
            return Layout.Children.IndexOf(e);
        }

        #endregion public methods

        private double PerpendicularLayoutMinSize
        {
            get
            {
                if(Layout.Orientation == Orientation.Horizontal)
                    return Layout.MinHeight;
                return Layout.MinWidth;
            }

            set
            {
                if(Layout.Orientation == Orientation.Horizontal)
                    Layout.MinHeight = value;
                else
                    Layout.MinWidth = value;
            }
        }

        private BoxLayout Layout 
        { 
            get => __layout; 
            set
            {
                __layout = value;
                if(__layout != null)
                {
                    Column = __layout.RowDefinitions;
                    Row = __layout.ColumnDefinitions;
                }
                else
                {
                    Column = null;
                    Row = null;
                }
            }
        }

        private RowDefinitionCollection Column { get; set; } = null;
        private ColumnDefinitionCollection Row { get; set; } = null;
        
        private IEnumerable<CellInfo> Cells()
        {
            switch (Layout.Orientation)
            {
                case Orientation.Horizontal:
                    foreach(ColumnDefinition _column in Row)
                    {
                        yield return ((HCell)_column).CellInfo;
                    }
                    break;
                case Orientation.Vertical:
                    foreach(RowDefinition _row in Column)
                    {
                        yield return ((VCell)_row).CellInfo;
                    }
                    break;
            }
        }

        private CellInfo VCellInfoAt(int index)
        {
            if(index < 0 || index >= Column.Count)
                return null;
            return ((VCell)Column[index]).CellInfo;
        }

        private CellInfo HCellAt(int index)
        {
            if(index < 0 || index >= Row.Count)
                return null;
            return ((HCell)Row[index]).CellInfo;
        }

        private VCell InsertVertical(int index, FrameworkElement e)
        {
            VCellInfo cellInfo = new VCellInfo(this, e);
            VCell _vcell = new VCell(cellInfo);
            _vcell.CellInfo.Previous = VCellInfoAt(index);
            Column.Insert(index, _vcell);
            _vcell.CellInfo.Next = VCellInfoAt(index + 1);

            Grid.SetColumn(e, 0);
            Grid.SetRow(e,index);
            Layout.Children.Insert(index, e);

            return _vcell;
        }

        private HCell InsertHorizontal(int index, FrameworkElement e)
        {
            HCellInfo cellInfo = new HCellInfo(this, e);
            HCell _hcell = new HCell(cellInfo);
            _hcell.CellInfo.Previous = HCellAt(index);
            Row.Insert(index, _hcell);
            _hcell.CellInfo.Next = HCellAt(index + 1);

            Grid.SetColumn(e, index);
            Grid.SetRow(e, 0);
            Layout.Children.Insert(index, e);

            return _hcell;
        }

        private bool RemoveAt(int index)
        {
            Layout.Children.RemoveAt(index);
           
            if(index < 0 || index >= Layout.Children.Count)
                return false;

            CellInfo _cellInfo = null;

            switch (Layout.Orientation)
            {
                case Orientation.Horizontal:
                    _cellInfo =((HCell)Row[index]).CellInfo;
                    Row.RemoveAt(index);
                    break;
                case Orientation.Vertical:
                    _cellInfo = ((VCell)Column[index]).CellInfo;
                    Column.RemoveAt(index);
                    break;
            }

            if(_cellInfo.Previous != null)
                _cellInfo.Previous.Next = _cellInfo.Next;
            if(_cellInfo.Next != null)
                _cellInfo.Next.Previous = _cellInfo.Previous;

            return true;
        }

        private int SpacersInfinite { get; set; }

        #region Update

        public void UpDate()
        {
            if(Updating)
                return;

            bool _changes = false;

            double _orientedMinElementSize = 0;
            double _orientedMaxElementSize = 0;
            double _perpendicularMinElementsSize = 0;

            foreach(CellInfo _cellInfo in Cells())
            {
                _cellInfo.SetElementAlignment(ALIGNMENT.CENTER);

                _orientedMinElementSize = _cellInfo.OrientedMinimumElementSize();
                _orientedMaxElementSize = _cellInfo.OrientedMaximumElementSize();

                _perpendicularMinElementsSize += _cellInfo.PerpendicularMinElementSize();

                if(_cellInfo.ELEMENTTYPE == ELEMENTTYPE.ANY)
                {
                    if(SpacersInfinite > 0)
                    {
                        if(_cellInfo.SetOrientedMaxCellSize(_orientedMinElementSize))
                            _changes = true;
                    }
                    else
                    {
                        if(_cellInfo.IsPreviousElementType(ELEMENTTYPE.GLUE) 
                        && _cellInfo.IsPreviousElementType(ELEMENTTYPE.GLUE))
                        {
                            if(_cellInfo.SetOrientedMaxCellSize(_orientedMinElementSize))
                                _changes = true;
                        }
                        else
                        {
                            if(_cellInfo.IsPreviousElementType(ELEMENTTYPE.GLUE))
                                _cellInfo.SetElementAlignment(ALIGNMENT.PROXIMAL);
                            else
                                _cellInfo.SetElementAlignment(ALIGNMENT.DISTAL);

                            if(_cellInfo.SetOrientedMaxCellSize(_orientedMaxElementSize))
                                _changes = true;
                        }
                    }
                }
                else
                {
                    if(_cellInfo.SetOrientedMaxCellSize(_orientedMaxElementSize))
                        _changes = true;
                }

                // anyway
                if(_cellInfo.SetOrientedMinCellSize(_orientedMinElementSize))
                    _changes = true;
            }

            if(PerpendicularLayoutMinSize < _perpendicularMinElementsSize)
            {
                PerpendicularLayoutMinSize = _perpendicularMinElementsSize;
                _changes = true;
            }

            if(_changes == true && Layout.Parent is BoxLayout _layoutParent)
                _layoutParent.Model.UpDate();
        }

        #endregion Update

        private BoxLayout __layout = null;
        private DefinitionBase __cells = null;
    }

    
}
