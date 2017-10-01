using System;
using System.Windows;
using System.Windows.Controls;

namespace BoxLayouts
{
    public enum ELEMENTTYPE
    {
        SPACER,
        FIXEDSPACER,
        GLUE,
        ANY
    }

    public enum ALIGNMENT
    {
        PROXIMAL,
        DISTAL,
        CENTER
    }

    public abstract class CellInfo
    {
        public CellInfo(BoxLayoutModel model, FrameworkElement e)
        {
            Element = e;
            Model = model;
        }

        public DefinitionBase Cell { get; set; }

        public BoxLayoutModel Model { get; private set; }

        public CellInfo Previous { get; set; }
        public CellInfo Next { get; set; }

        public ELEMENTTYPE ELEMENTTYPE { get; private set; }

        public bool IsPreviousElementType(ELEMENTTYPE _elementType)
        {
            return Previous != null && Previous.ELEMENTTYPE == _elementType;
        }

        public bool IsNextElementType(ELEMENTTYPE _elementType)
        {
            return Next != null && Next.ELEMENTTYPE == _elementType;
        }

        public FrameworkElement Element
        {
            get => __element;
            set
            {
                __element = value??throw new ArgumentNullException("value");

                if(__element is Spacer)
                    ELEMENTTYPE = ELEMENTTYPE.SPACER;
                else
                    if(__element is FixedSpacer)
                    ELEMENTTYPE = ELEMENTTYPE.FIXEDSPACER;
                else 
                    if(__element is Glue)
                    ELEMENTTYPE = ELEMENTTYPE.GLUE;
                else
                    ELEMENTTYPE = ELEMENTTYPE.ANY;
            }
        }

        public abstract double PerpendicularMinElementSize();

        public abstract double OrientedMinimumElementSize();

        public abstract double OrientedMaximumElementSize();

        public abstract double ActualOrientedCellMinSize();
        
        public abstract double ActualOrientedCellMaxSize();

        public abstract bool SetOrientedMinCellSize(double _size);

        public abstract bool SetOrientedMaxCellSize(double _size);

        public abstract void SetElementAlignment(ALIGNMENT alignment);

        protected double MinHeight()
        {
            return MesureHelper.MinHeight(Element);
        }

        protected double MinWidth()
        {
            return MesureHelper.MinWidth(Element);
        }

        private FrameworkElement __element = null;
    }

    public class VCellInfo : CellInfo
    {
        public VCellInfo(BoxLayoutModel model, FrameworkElement e)
            : base(model, e){ }

        public override double PerpendicularMinElementSize()
        {
            switch (ELEMENTTYPE)
            {
                case ELEMENTTYPE.SPACER:

                    return 0;

                case ELEMENTTYPE.FIXEDSPACER:

                    return 0;

                case ELEMENTTYPE.GLUE:
                    
                    return 0;

                case ELEMENTTYPE.ANY:
                    return MinWidth();
            }
            throw new Exception("ELEMENTTYPE non pris en compte.");
        }

        public override double OrientedMinimumElementSize()
        {
            switch (ELEMENTTYPE)
            {
                case ELEMENTTYPE.SPACER:

                    return ((Spacer)Element).MinSpace;

                case ELEMENTTYPE.FIXEDSPACER:

                    return ((FixedSpacer)Element).Space;

                case ELEMENTTYPE.GLUE:
                    
                    return 0;

                case ELEMENTTYPE.ANY:
                    return MinHeight();
            }
            throw new Exception("ELEMENTTYPE non pris en compte.");
        }

        public override double OrientedMaximumElementSize()
        {
            switch (ELEMENTTYPE)
            {
                case ELEMENTTYPE.SPACER:

                    return ((Spacer)Element).MaxSpace;
                    
                case ELEMENTTYPE.FIXEDSPACER:
                    
                    return ((FixedSpacer)Element).Space;

                case ELEMENTTYPE.GLUE:
                    
                    return((Glue)Element).MaxSpace;

                case ELEMENTTYPE.ANY:

                    return Element.MaxHeight;
            }
            throw new Exception("ELEMENTTYPE non pris en compte.");
        }

        public override double ActualOrientedCellMinSize()
        {
            return ((RowDefinition)Cell).MinHeight;
        }

        public override double ActualOrientedCellMaxSize()
        {
            return ((RowDefinition)Cell).MaxHeight;
        }

        public override bool SetOrientedMinCellSize(double _size)
        {
            if(((RowDefinition)Cell).MinHeight == _size)
                return false;
            ((RowDefinition)Cell).MinHeight = _size;
            return true;
        }

        public override bool SetOrientedMaxCellSize(double _size)
        {
            if(((RowDefinition)Cell).MaxHeight == _size)
                return false;
            ((RowDefinition)Cell).MaxHeight = _size;
            return true;
        }

        public override void SetElementAlignment(ALIGNMENT alignment)
        {
            switch (alignment)
            {
                case ALIGNMENT.PROXIMAL:
                    Element.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case ALIGNMENT.DISTAL:
                    Element.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case ALIGNMENT.CENTER:
                    Element.VerticalAlignment = VerticalAlignment.Center;
                    break;
            }
        }

    }

    public class HCellInfo : CellInfo
    {
        public HCellInfo(BoxLayoutModel model, FrameworkElement e)
            : base(model, e){ }

        public override double PerpendicularMinElementSize()
        {
            switch (ELEMENTTYPE)
            {
                case ELEMENTTYPE.SPACER:

                    return ((Spacer)Element).MinSpace;

                case ELEMENTTYPE.FIXEDSPACER:

                    return ((FixedSpacer)Element).Space;

                case ELEMENTTYPE.GLUE:
                    
                    return 0;

                case ELEMENTTYPE.ANY:

                    return MinHeight();
            }
            throw new Exception("ELEMENTTYPE non pris en compte.");
        }

        public override double OrientedMinimumElementSize()
        {
            switch (ELEMENTTYPE)
            {
                case ELEMENTTYPE.SPACER:

                    return ((Spacer)Element).MinSpace;

                case ELEMENTTYPE.FIXEDSPACER:

                    return ((FixedSpacer)Element).Space;

                case ELEMENTTYPE.GLUE:
                    
                    return 0;

                case ELEMENTTYPE.ANY:
                    return MinWidth();
            }
            throw new Exception("ELEMENTTYPE non pris en compte.");
        }

        public override double OrientedMaximumElementSize()
        {
            switch (ELEMENTTYPE)
            {
                case ELEMENTTYPE.SPACER:

                    return ((Spacer)Element).MaxSpace;
                    
                case ELEMENTTYPE.FIXEDSPACER:
                    
                    return ((FixedSpacer)Element).Space;

                case ELEMENTTYPE.GLUE:
                    
                    return((Glue)Element).MaxSpace;

                case ELEMENTTYPE.ANY:

                    return Element.MaxWidth;
            }
            throw new Exception("ELEMENTTYPE non pris en compte.");
        }

        public override double ActualOrientedCellMinSize()
        {
            return ((ColumnDefinition)Cell).MinWidth;
        }

        public override double ActualOrientedCellMaxSize()
        {
            return ((ColumnDefinition)Cell).MaxWidth;
        }
        
        public override bool SetOrientedMinCellSize(double size)
        {
            if(((ColumnDefinition)Cell).MinWidth == size)
                return false;
            ((ColumnDefinition)Cell).MinWidth = size;
            return true;
        }

        public override bool SetOrientedMaxCellSize(double size)
        {
            if(((ColumnDefinition)Cell).MaxWidth == size)
                return false;
            ((ColumnDefinition)Cell).MaxWidth = size;
            return true;
        }

        public override void SetElementAlignment(ALIGNMENT alignment)
        {
            switch (alignment)
            {
                case ALIGNMENT.PROXIMAL:
                    Element.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case ALIGNMENT.DISTAL:
                    Element.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case ALIGNMENT.CENTER:
                    Element.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 
    /// </summary>
    public class HCell : ColumnDefinition
    {
        public HCell(CellInfo cellInfo)
        {
            CellInfo = cellInfo;
            CellInfo.Cell = this;
        }

        public CellInfo CellInfo { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VCell : RowDefinition
    {
        public VCell(CellInfo cellInfo)
        {
            CellInfo = cellInfo;
            CellInfo.Cell = this;
        }

        public CellInfo CellInfo { get; private set; }
    }
}
