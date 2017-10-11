

using System.Collections.Generic;
using System.Reflection;
using MSTD;
using MSTD.ShBase;

namespace ObjectEdit
{
    public class PropertyEditControlConfig
    {
        /// <summary>
        /// L'objet de class <see cref="Base"/> édité par le <see cref="ObjectEditControl"/> contenant
        /// le <see cref="PropertyEditControl"/> qui recevra ce <see cref="PropertyEditControlConfig"/>.
        /// Appelle <see cref="PropertyEditControl.Update"/> si <see cref="IsComplete"/> == true.
        /// </summary>
        public Base Object
        {
            get => __object;
            set
            {
                __object = value;
                if(IsComplete())
                    Control.Update();
            }
        }

        public PropertyInfo PropertyInfo
        {
            get => __prInfo;
            set
            {
                __prInfo = value;
                if(IsComplete())
                    Control.Update();
            }
        }

        /// <summary>
        /// Le <see cref="PropertyEditControl"/> qui recevra ce <see cref="PropertyEditControlConfig"/>.
        /// Appelle <see cref="PropertyEditControl.Update"/> si <see cref="IsComplete"/> == true.
        /// </summary>
        public virtual PropertyEditControl Control
        {
            get => __controle;
            set
            {
                __controle = value;
                if(IsComplete())
                    Control.Update();
            }
        }

        public object Value
        {
            get
            {
                if(Object != null && PropertyInfo != null)
                    return PropertyInfo.GetValue(Object);
                return null;
            }

            set
            {
                if(Object != null && PropertyInfo != null)
                    PropertyInfo.SetValue(Object, value);
            }
        }

        public virtual bool IsComplete()
        {
            return Object != null && PropertyInfo != null && Control != null;
        }

        private Base __object = null;
        private PropertyInfo __prInfo = null;
        private PropertyEditControl __controle = null;
    }

    public class PropertyClassEditControlConfig : PropertyEditControlConfig
    {
        public PropertyClassEditControlConfig()
        {}

        public override bool IsComplete()
        {
            return base.IsComplete()
                && DataDisplay != null && ObjectsToDisplay != null && ObjectsToDisplay.Count != 0;
        }

        public DataDisplay DataDisplay
        {
            get => __dataDisplay;
            set
            {
                __dataDisplay = value;
                if(IsComplete())
                    Control.Update();
            }
        }

        public List<Base> ObjectsToDisplay
        { 
            get => __objectsToDisplay; 
            set
            {
                __objectsToDisplay = value;
                if(IsComplete())
                    Control.Update();
            }
        }

        public int IndexOf(Base _base)
        {
            if(_base == null || __objectsToDisplay == null)
                return -1;
            int _i = 0;
            foreach(Base _b in __objectsToDisplay)
            {
                if(_b != null && _b.ID == _base.ID)
                    return _i;
                ++_i;
            }
            return -1;
        }

        public Base DisplayedObjectAt(int _index)
        {
            return __objectsToDisplay[_index];
        }

        private List<Base> __objectsToDisplay = null;
        private DataDisplay __dataDisplay = null;
    }
}
