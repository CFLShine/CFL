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

        //private ctrl_select_communes __ctrl_communes = new ctrl_select_communes();


        private void buttonExe_Click(object sender, RoutedEventArgs e)
        {
            Defunt _defunt = new Defunt();
            _defunt.identite.nom = "DUPONT";

            ObjectEditControlLayout _objectEdit = new ObjectEditControlLayout(_defunt)
            {
                LabelsMinimumWidth = 100,
                ControlsMinimumWidth = 200,
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

        VBoxLayout __vlayout = null;
        private void buttonB_Click(object sender, RoutedEventArgs e)
        {
            RootLayout.Remove(__vlayout);
            __vlayout = new VBoxLayout(){ Background = Brushes.Beige };
            AddElementToRootLayout(__vlayout);
        
            HBoxLayout h_layout = new HBoxLayout();
            h_layout.Add(new Glue(10));
            h_layout.Add(new TextBox(){ Text = "h_layout,MinWidth = 100, Height = 50,\nfolowed by Glue(30) ",MinWidth = 100, MinHeight = 50 });
            h_layout.Add(new Glue(30));
            h_layout.Add(new TextBox(){ Text = "h_layout, MinHeight = 30, MinWidth = 300,\nfolowed by FixedSpacer(30)", MinHeight = 30, MinWidth = 300});
            h_layout.Add(new FixedSpacer(30));
            h_layout.Add(new Glue(0));
            h_layout.Add(new TextBox(){ Text = "h_layout, Width = 80", Width = 80});

            __vlayout.Add(new TextBox(){ Text = "Minheight = 50, MinWidth = 150,\nfolowed by h_layout",MinHeight = 50, MinWidth = 150 });
            __vlayout.Add(h_layout);
            __vlayout.Add(new TextBox() { Text = "MinHeigh = 60,\nfolowed by spacer(20)", MinHeight = 60});
            __vlayout.Add(new Spacer(20));

        }

        void init()
        {
            buttonExe = new Button() { Content = "exe" };
            buttonExe.MaxWidth = 150;
            buttonExe.Click += buttonExe_Click;
            AddElementToLayoutMenuTop(buttonExe);

            buttonB = new Button() { Content = "exe 2" };
            buttonB.MaxWidth = 150;
            buttonB.Click += buttonB_Click;
            AddElementToLayoutMenuTop(buttonB);
        }
    }
    
}
