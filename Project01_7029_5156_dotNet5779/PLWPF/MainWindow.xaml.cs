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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddTrainee_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AddTraineeWindow();
           window.Show();
        }

        private void btnSearchTrainee_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SearchTrainee();
            window.Show();
        }

        private void btnAddTester_Click(object sender, RoutedEventArgs e)
        {
            Window window = new AddTesterWindow();
            window.Show();
        }

        private void btnSearchTester_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SearchTester();
            window.Show();
        }
    }
}
