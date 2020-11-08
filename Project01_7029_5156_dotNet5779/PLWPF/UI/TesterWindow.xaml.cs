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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TraineeWindow.xaml
    /// </summary>
    public partial class TesterWindow : Window
    {
        BL.IBL bl;
        bool manage;
        public TesterWindow(bool ismanage)
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();
            if (ismanage)
            {
                manage = true;
                btnBack.Visibility = Visibility.Visible;
                ButtonBestTeacher.Visibility = Visibility.Visible;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Window addTester = new AddTesterWindow();
            addTester.ShowDialog();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            SearchTester searchTester = new SearchTester(manage);
            searchTester.ShowDialog();
        }



        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonBestTester_Click(object sender, RoutedEventArgs e)
        {
            string result = bl.theBestTester();
            MessageBox.Show(result);
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            TestsOfTraineeWindow searchTest = new TestsOfTraineeWindow(true);
            searchTest.ShowDialog();
        }
    }
}
