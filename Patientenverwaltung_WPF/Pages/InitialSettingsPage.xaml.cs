using System.Windows.Controls;
using System.Windows.Forms;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für InitialSettingsPage.xaml
    /// </summary>
    public partial class InitialSettingsPage : Page
    {
        bool CloseAfterSave = false;

        public InitialSettingsPage(bool closeAfterSave = false)
        {
            InitializeComponent();

            CloseAfterSave = closeAfterSave;

            DataContext = CurrentContext.GetSettings();
        }

        private void btnSaveSettings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CurrentContext.GetSettings().Savetype = (Savetype)cmbBoxStorageType.SelectedItem;

            CurrentContext.GetSettings().UpdateJSON();

            // Get back to login page
            MainWindow.UpdatePage(Constants.LoginPageUri);            
        }

        private void btnSelectSavelocation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

            txtBoxSaveloaction.Text = $@"{folderBrowserDialog.SelectedPath}\";
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Init combobox
            //cmbBoxStorageType.Items.Add(Constants.Storagetype_JSON);
            //cmbBoxStorageType.Items.Add(Constants.Storagetype_SQL);
            //cmbBoxStorageType.Items.Add(Constants.Storagetype_XML);

            cmbBoxStorageType.ItemsSource = System.Enum.GetValues(typeof(Savetype));

            cmbBoxStorageType.SelectedIndex = cmbBoxStorageType.Items.IndexOf(CurrentContext.GetSettings().Savetype);
        }

        private void cmbBoxStorageType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }    
}
