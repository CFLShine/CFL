using System.Windows;
using System.Windows.Controls;
using CFL_1.CFL_Data;
using System;
using CFL_1.CFL_System.SqlServerOrm;
using BoxLayouts;

namespace CFL_1.CFLGraphics
{
    public class Form_config_connection : CFLForm
    {
        public Form_config_connection()
        {
            init();
            buttonSave = true;
        }

        public override void BecomeCurrent()
        {
            load();
        }

        public override void GetNotification(DBNotification _notification)
        {
            // 
        }

        public void load()
        {
            Config _config = new Config();
            
            Config.load(ref _config);
            ip.Text = _config.hostname;
            userName.Text = _config.username;
            pass.Text = _config.password;
            dbName.Text = _config.dbname;
        }

        public override bool Save()
        {
            //TODO 
            return false;
        }

        public override bool NewOne()
        { return true; }
        public override bool DeleteCurrent()
        { return true; }

        public override void Documents()
        {
            throw new NotImplementedException();
        }
        
        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            textbox_sql.Clear();
        }

        private void init()
        {
            HBoxLayout _layoutTop = new HBoxLayout();
            AddElementToRootLayout(_layoutTop);

            groupBoxConfigDB.Header = "Base de données";
            groupBoxConfigDB.Content = _layoutFormConfigDB;
            _layoutTop.Add(groupBoxConfigDB);

            _layoutFormConfigDB.Append("ip", ip, 25);
            _layoutFormConfigDB.Append("Nom utilisateur", userName, 25);
            _layoutFormConfigDB.Append("Mot de pass", pass, 25);
            _layoutFormConfigDB.Append("Nom DB", dbName, 25);

            dbName.Text = "cfl";

            HBoxLayout _layoutBottom = new HBoxLayout();
            AddElementToRootLayout(_layoutBottom);

            // buttons

            VBoxLayout __layoutLeft = new VBoxLayout() ;
            
            _layoutBottom.Add(__layoutLeft);

            // textbox_sql

            textbox_sql = new TextBox();
            textbox_sql.AcceptsReturn = true;

            _layoutBottom.Add(textbox_sql);
        }

        public TextBox ip = new TextBox();
        public TextBox userName = new TextBox();
        public TextBox pass = new TextBox();
        public TextBox dbName = new TextBox();

        public TextBox textbox_sql = new TextBox();

        private GroupBox groupBoxConfigDB = new GroupBox();
        private FormLayout _layoutFormConfigDB = new FormLayout();

    }
}
