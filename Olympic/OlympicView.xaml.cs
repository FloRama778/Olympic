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
using Olympic.ModelView;

namespace Olympic
{
    /// <summary>
    /// Logica di interazione per OlympicView.xaml
    /// </summary>
    public partial class OlympicView : Window
    {
        private OlympicsModelView vm;
        public OlympicView()
        {
            InitializeComponent();
            vm = new OlympicsModelView();
            DataContext = vm;
        }


        private void MenuEsci_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Florim Ramadani");
        }
        private void AzzeraFiltri_Click(object sender, RoutedEventArgs e)
        {
            vm.AzzeraFiltri();
        }
        private void VaiAvanti_Click(object sender, RoutedEventArgs e)
        {
            vm.VaiAvanti();
        }
        private void VaiIndietro_Click(object sender, RoutedEventArgs e)
        {
            vm.VaiIndietro();
        }
        private void VaiPagIniziale_Click(object sender, RoutedEventArgs e)
        {
            vm.VaiPagIniziale();
        }
        private void VaiUltimaPag_Click(object sender, RoutedEventArgs e)
        {
            vm.VaiUltimaPag();
        }
    }
}
