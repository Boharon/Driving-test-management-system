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
    public partial class TraineeWindow : Window
    {
        bool manage;
        public TraineeWindow(bool ismanage)
        {
            InitializeComponent();
            if (ismanage)
                manage = true;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Window addTrainee = new AddTraineeWindow();
            addTrainee.ShowDialog();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            SearchTrainee searchTrainee = new SearchTrainee(manage);
            searchTrainee.ShowDialog();
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWindow = new TestWindow(manage);
            testWindow.ShowDialog();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
