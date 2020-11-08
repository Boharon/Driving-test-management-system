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

namespace PLWPF 
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {
        public Tester tester;
        bool[,] btn;
        public  ScheduleWindow(Tester t)
        {
            InitializeComponent();
            tester = t;
            btn = new bool[5, 7];

            foreach (var item in schedulesGrid.Children.OfType<Button>())
            {
                if (t.ScheduleTester[(((Convert.ToInt32(item.Tag)) / 10)), ((Convert.ToInt32(item.Tag)) % 10)] == true)
                    item.Background = Brushes.Gold;
            }
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button bt = (Button)sender;
                if (bt.Background == Brushes.White)
                {
                    bt.Background = Brushes.Gold;
                    btn[(((Convert.ToInt32(bt.Tag)) / 10)), ((Convert.ToInt32(bt.Tag)) % 10)] = true;
                }
                else
                {
                    bt.Background = Brushes.White;
                    btn[(((Convert.ToInt32(bt.Tag)) / 10)), ((Convert.ToInt32( bt.Tag) )% 10)] = false;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


            private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show("האם אתה בטוח?", "אישור", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                tester.ScheduleTester = btn;
                this.Close();
            }
        }
    }
}
