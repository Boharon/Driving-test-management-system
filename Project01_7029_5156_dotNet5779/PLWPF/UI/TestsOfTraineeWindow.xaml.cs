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
    /// Interaction logic for TestsOfTraineeWindow.xaml
    /// </summary>
    public partial class TestsOfTraineeWindow : Window
    {
        string id;
        BL.IBL bl;
        public TestsOfTraineeWindow(bool t)
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            if(t)
            {
                lblSearch_Copy.Visibility = Visibility.Visible;
                txtSearch_Copy.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Visible;
                lblSearch.Visibility = Visibility.Hidden;
                txtSearch.Visibility = Visibility.Hidden;
                btnDelete.Visibility = Visibility.Hidden;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
                if (txtSearch_Copy.Text == "")
                {
                    MessageBox.Show("יש להקיש תעודת זהות לחיפוש");
                    return;
                }

                else
                {
                    id = txtSearch_Copy.Text;
                    if (id.Length != 9)
                    {
                        MessageBox.Show("יש להקיש תעודת זהות לחיפוש");
                        return;
                    }
                    findGrid.ItemsSource = bl.GetAllTestsOfTester(id);
                    return;
                }
            id = txtSearch.Text;
            if(id.Length!=9)
            {
                MessageBox.Show("יש להקיש תעודת זהות לחיפוש");
                return;
            }

            findGrid.ItemsSource = bl.GetAllTestsOfTrainee(id);

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (findGrid.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור מבחן לעידכון");
                return;
            }
            Window addTest = new AddTestWindow((Test)findGrid.SelectedItem);
            addTest.Show();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (findGrid.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור מבחן למחיקה");
                return;
            }
            try
            {
                MessageBoxResult result = MessageBox.Show("מבחן זה ימחק. האם אתה בטוח?", "אישור", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.RemoveTest(((Test)findGrid.SelectedItem).Code, ((Test)findGrid.SelectedItem).TestDate);
                    MessageBox.Show("המבחן נמחק בהצלחה !", "אישור");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void btnBack_Click1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
