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
    /// Interaction logic for manageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        bool manage = true;
        public ManageWindow()
        {
            InitializeComponent();
        }

        private void ButtonTrainee_Click(object sender, RoutedEventArgs e)
        {
            TraineeWindow traineeWindow = new TraineeWindow(manage);
            traineeWindow.ShowDialog();
        }

        private void ButtonTester_Click(object sender, RoutedEventArgs e)
        {
            TesterWindow testerWindow = new TesterWindow(manage);
            testerWindow.ShowDialog();
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {

            TestWindow testWindow = new TestWindow(manage);
            testWindow.ShowDialog();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            DisplayWindow displayWindow = new DisplayWindow();
            displayWindow.Show();
            this.Close();
        }
    }
}
