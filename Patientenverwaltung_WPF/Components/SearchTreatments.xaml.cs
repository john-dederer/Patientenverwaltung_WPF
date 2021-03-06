﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patientenverwaltung_WPF
{
    /// <summary>
    /// Interaktionslogik für SearchTreatments.xaml
    /// </summary>
    public partial class SearchTreatments : UserControl
    {
        public SearchTreatments()
        {
            InitializeComponent();

            DataContext = TreatmentViewModel.SharedViewModel();
        }

        private void searchFieldTreatment_Changed(object sender, TextChangedEventArgs e)
        {
            TreatmentViewModel.SharedViewModel().FilterPredicate = txtSearchFieldTreatment.Text;

            TreatmentViewModel.SharedViewModel().FilterTreatments();
        }
    }
}
