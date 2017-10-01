using System;
using System.Collections.Generic;
using System.Windows.Controls;
using BoxLayouts;

namespace DailySchedule
{
    public class ActionControl : HBoxLayout
    {
        public ActionControl()
        {
            Init();
        }

        protected void Init()
        {
            Add(TextBoxHeure);
            Add(TextBoxAction);
        }

        private ActionInfo __actionInfo = null;

        public TextBox TextBoxHeure = new TextBox() { MaxWidth = 40, IsReadOnly = true };
        public TextBox TextBoxAction = new TextBox() { IsReadOnly = true };
    }
}
