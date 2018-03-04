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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für AddTreatment.xaml
    /// </summary>
    public partial class AddTreatment : Window
    {
        public Treatment Treatment { get; set; }


        public AddTreatment(long patientId)
        {
            InitializeComponent();

            Treatment = new Treatment
            { 
                    PatientId = patientId,
                    Date = DateTime.Now                           
            };


            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Behandlung erfassen
            // First we have to check if the treatment already exists
            if (Factory.Get(CurrentContext.GetSettings().Savetype).Select(Treatment))
            {

            }
            else
            {
                if (Factory.Get(CurrentContext.GetSettings().Savetype).Create(Treatment))
                {
                    // Successfully created 
                    DialogResult = true;

                    InfoMessageWindow infoWindow = new InfoMessageWindow("Behandlung erfolgreich erfasst", 2);
                    infoWindow.ShowDialog();
                    Close();

                }
                else
                {
                    // Treatment already exists
                    InfoMessageWindow infoWindow = new InfoMessageWindow("Behandlung konnte nicht erfasst werden", 2);
                    infoWindow.ShowDialog();
                }
            }
        }
    }
}
