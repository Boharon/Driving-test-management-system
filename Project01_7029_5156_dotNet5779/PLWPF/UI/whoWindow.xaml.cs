using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
    /// Interaction logic for whoWindow.xaml
    /// </summary>
    public partial class whoWindow : Window
    {
        public whoWindow()
        {
            InitializeComponent();
        }



        private void rdbTrainee_Checked(object sender, RoutedEventArgs e)
        {
            TraineeWindow tw = new TraineeWindow(false);
            tw.Show();
            this.Close();


        }

        private void rdbTester_Checked(object sender, RoutedEventArgs e)
        {

            TesterWindow tw = new TesterWindow(false);
            tw.Show();
            this.Close();

        }
    }
}
