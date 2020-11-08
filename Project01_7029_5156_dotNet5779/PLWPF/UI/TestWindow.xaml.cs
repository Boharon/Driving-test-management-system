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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        bool manage;
        public TestWindow(bool ismanage)
        {
            InitializeComponent();
            if (ismanage)
            {
                manage = true;
                ButtonAllOfTest.Visibility = Visibility.Visible;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddTestWindow addTestWindow = new AddTestWindow();
            addTestWindow.ShowDialog();
        }


        private void ButtonAllTest_Click(object sender, RoutedEventArgs e)
        {
            TestsOfTraineeWindow searchTest = new TestsOfTraineeWindow(false);
            searchTest.ShowDialog();
        }

        private void ButtonAllOfTest_Click(object sender, RoutedEventArgs e)
        {
            SearchTestWindow searchTest = new SearchTestWindow();
            searchTest.ShowDialog();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            DisplayWindow displayWindow = new DisplayWindow();
            displayWindow.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
