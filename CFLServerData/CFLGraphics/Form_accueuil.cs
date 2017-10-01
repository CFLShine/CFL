using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CFL_1.CFL_System.SqlServerOrm;

namespace CFL_1.CFLGraphics
{
    public class Form_accueuil : CFLForm
    {
        public Form_accueuil()
        {
            initialiseComponents();
        }

        public override void BecomeCurrent()
        {}

        public override void GetNotification(DBNotification _notification)
        { /* pas de notification utile à form_accueil */}

        public override bool Save()
        { return true; }
        public override bool NewOne()
        { return true; }
        public override bool DeleteCurrent()
        { return true; }

        public override void Documents()
        {
            throw new NotImplementedException();
        }

        private Label __topLabel;
        private Calendar __calendar;

        //private:

        private void initialiseComponents()
        {
            // top Label
            __topLabel = new Label();
            __topLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            __topLabel.VerticalContentAlignment   = VerticalAlignment.Center;
            
            LinearGradientBrush _gradient = new LinearGradientBrush
            (Colors.Black, Colors.White, new Point(0, 0.5), new Point(1, 0.5));
            __topLabel.Background = _gradient;
            __topLabel.Content = "CFL";
            __topLabel.FontSize = 36;
            __topLabel.FontWeight = FontWeights.Bold;

            // calendar
            __calendar = new Calendar();

            AddElementToRootLayout(__topLabel);
            AddElementToRootLayout(__calendar);
            
        }
    }
}
