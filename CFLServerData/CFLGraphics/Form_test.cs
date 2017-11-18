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
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CFL_1.CFL_System.MSTD;
using CFL_1.CFL_System.DB;
using MongoDB.Bson.Serialization;
using CFL_1.CFL_Data;
using MongoDB.Bson;
using MSTD.Mongo;

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
            Defunt dft = new Defunt();
            dft.Identite.Nom = "DUPONT";
            dft.Identite.Prenom = "Marcel";
            dft.Deces.lieu.Nom = "domicile";
            dft.Deces.lieu.Coordonnees.adress1 = "Chemin du Bois";
            dft.Deces.lieu.Coordonnees.commune = new Commune(){ nom = "Aix-les-Bains"};
            MEB meb = new MEB();
            meb.commentaire = "no comment";
            meb.date = new DateTime(2017, 11, 02);
            meb.heure = new TimeSpan(10, 30, 0);
            meb.lieu = new CFL_Data.Lieu()
            {
                Nom = "cf",
                Coordonnees = new CFL_Data.Coordonnees()
                {
                    adress1 = "86 square Louis Sève",
                    commune = new Commune(){ nom = "Chambery"}
                }
            };

            dft.OperationsFuneraires.Add(meb);


            CFLDBConnection_mongo _connection = CFLDBConnection_mongo.Instance;

            BsonDocument _document = Serializer.Document(dft);
            _connection.DataBase.GetCollection(typeof(Defunt), "Defunt").Insert(typeof(Defunt), _document);
            //_connection.DataBase.GetCollection(typeof(Defunt), "Defunt").Insert(typeof(Defunt), dft);
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
