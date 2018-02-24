using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für InfoMessageWindow.xaml
    /// </summary>
    public partial class InfoMessageWindow : Window
    {
        public InfoMessageWindow(string toDisplay = "", int timeToDisplay = 3)
        {
            InitializeComponent();

            txtField.Text = toDisplay;

            CloseInSeconds(timeToDisplay);

            // Center message box
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Owner = Application.Current.MainWindow;

        }

        private void CloseInSeconds(int timeToDisplay)
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(timeToDisplay)
            };

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ((DispatcherTimer)sender).Stop();
            ((DispatcherTimer)sender).Tick -= Timer_Tick;
            Close();
        }

        
    }
}
