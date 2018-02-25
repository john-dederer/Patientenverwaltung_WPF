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
using Patientenverwaltung_WPF.ViewModel;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für HealthinsuranceTestWindow.xaml
    /// </summary>
    public partial class HealthinsuranceTestWindow : Window
    {
        public HealthinsuranceTestWindow()
        {
            InitializeComponent();

            DataContext = HealthinsuranceViewModel.SharedViewModel();
            this.Loaded += HealthinsuranceTestWindow_Loaded;
        }

        void HealthinsuranceTestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var errors = Grid.GetValue(Validation.ErrorsProperty);
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) HealthinsuranceViewModel.Errors += 1;
            if (e.Action == ValidationErrorEventAction.Removed) HealthinsuranceViewModel.Errors -= 1;
        }
    }
}
