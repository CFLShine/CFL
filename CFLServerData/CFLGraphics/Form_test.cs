using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CFL_1.CFL_System.SqlServerOrm;
using CFL_1.CFL_Data.Defunts;
using MSTD;
using RuntimeExec;
using System.Collections.Generic;
using ObjectEdit;
using CFL_1.CFL_Data.Communes;
using CFL_1.CFL_System;
using BoxLayouts;
using MSTD.ShBase;

namespace CFL_1.CFLGraphics
{
    public class Form_test : CFLForm
    {
        public Form_test()
        { init(); }

        #region CFLForm methods
        public override void BecomeCurrent()
        {}

        public override void GetNotification(DBNotification _notification)
        {
            //
        }

        public override bool Save()
        { return false; }
        public override bool NewOne()
        { return false; }
        public override bool DeleteCurrent()
        { return false; }
        public override void Documents()
        {}

        #endregion CFLForm methods

        private Button buttonExe;
        private Button buttonB;

        private void buttonExe_Click(object sender, RoutedEventArgs e)
        {
            Defunt _defunt = new Defunt();
            _defunt.Identite.Nom = "DUPONT";

            ObjectEditControlLayout _objectEdit = new ObjectEditControlLayout(_defunt)
            {
                LabelsMinimumWidth = 100,
                LabelsMaximumWidth = 150,
                ControlsMinimumWidth = 100,
                ControlsMaximumWidth = 150,
                BorderThickness = new Thickness(10),
                BorderBrush = Brushes.Black
            };
            
            _objectEdit.Background = Brushes.Beige;

            DataDisplay _dataDisplay = new DataDisplay
            (
                new REMemberExpression(typeof(Commune), ()=>((Commune)null).nom),
                new REValue(" "),
                new REMemberExpression(typeof(Commune), ()=>((Commune)null).codePost)
            );

            List<Tuple<string, string>> _communesCodesPost = new List<Tuple<string, string>>();
            Gate.load.Communes(ref _communesCodesPost);

            List<Base> _communes  = new List<Base>();

            foreach(Tuple<string, string> _t in _communesCodesPost)
                _communes.Add(new Commune(){ nom = _t.Item1, codePost = _t.Item2 });

            _objectEdit.SetConfigFor(typeof(Commune), 
                                     _communes,
                                     _dataDisplay);

            _objectEdit.Build();
          
            AddElementToRootLayout(_objectEdit);
        }

        enum MyEnum
        {
            UN,
            DEUX
        }

        private void buttonB_Click(object sender, RoutedEventArgs e)
        {
            MyEnum _myEnum = MyEnum.UN;
            Type _t = _myEnum.GetType();
        }

        void init()
        {
            buttonExe = new Button() { Content = "exe" };
            buttonExe.Width = 150;
            buttonExe.Click += buttonExe_Click;
            AddElementToLayoutMenuTop(buttonExe);

            buttonB = new Button() { Content = "exe 2" };
            buttonB.Width = 150;
            buttonB.Click += buttonB_Click;
            AddElementToLayoutMenuTop(buttonB);
        }
    }
    
}
