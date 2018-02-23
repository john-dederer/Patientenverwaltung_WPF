using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            DataContext = CurrentContext.GetSettings();

            cmbBox.ItemsSource = Enum.GetValues(typeof(Savetype));
            cmbBox.SelectedIndex = cmbBox.Items.IndexOf(CurrentContext.GetSettings().Savetype);
        }

        private void btnSelectSavelocation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            txtSaveLocation.Text = $@"{folderBrowserDialog.SelectedPath}\";
        }

        private void btnSaveSettings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentContext.GetSettings().Savetype = (Savetype)cmbBox.SelectedItem;

            CurrentContext.GetSettings().UpdateJSON();

            Close();
        }
    }
}
