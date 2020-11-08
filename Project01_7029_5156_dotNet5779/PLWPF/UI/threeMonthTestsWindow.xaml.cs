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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for threeMonthTestsWindow1.xaml
    /// </summary>
    public partial class threeMonthTestsWindow1 : Window
    {
        BL.IBL bl;
        Tester t;
        public threeMonthTestsWindow1(Tester tester)
        {
            try
            {
                bl = BL.FactoryBL.GetBL();
                t = tester;
                InitializeComponent();
                IEnumerable<Test> find = bl.getTestsOf3LastMonths(t);
                findGrid.ItemsSource = find;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
