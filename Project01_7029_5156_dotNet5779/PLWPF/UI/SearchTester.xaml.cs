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
    /// Interaction logic for SearchTrainee.xaml
    /// </summary>
    public partial class SearchTester : Window
    {
        BL.IBL bl;
        bool manage;
        Tester tester = new Tester();
        public SearchTester(bool ismanage)
        {

            bl = BL.FactoryBL.GetBL();
            InitializeComponent();
            if (ismanage)
            {
                manage = true;
                btnDelete.Visibility = Visibility.Visible;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Tester> find;
            try
            {
                if ((txtSearchID.Text == ""))
                {
                    if ((txtSearchF.Text == ""))
                    {
                        if ((txtSearchL.Text == ""))
                        {
                            throw new Exception("יש להזין ערכים לחיפוש");
                        }
                        else
                        {
                            find = bl.searchTesterLname(txtSearchL.Text);
                        }
                    }
                    else
                    {
                        find = bl.searchTesterFname(txtSearchF.Text);
                    }
                }
                else
                {
                    find = bl.GetTesterByPrefixId(txtSearchID.Text);
                }

                findGrid.ItemsSource = find;

                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                //trainee = bl.searchTrainee(txtSearch.Text);
                //if (trainee == null)
                //   throw new Exception("התלמיד לא קיים במערכת");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (findGrid.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור בוחן לעידכון");
                return;
            }
            Window addTester = new AddTesterWindow((Tester)findGrid.SelectedItem);
            addTester.Show();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (findGrid.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור בוחן למחיקה");
                return;
            }
            try
            {
                MessageBoxResult result = MessageBox.Show(" בוחן זה ימחק. האם אתה בטוח?", "אישור", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.RemoveTester(((Tester)findGrid.SelectedItem).Id);
                    MessageBox.Show("הבוחן נמחק בהצלחה !", "אישור");
                }
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
        private void Button3months_Click(object sender, RoutedEventArgs e)
        {
            if (findGrid.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור בוחן ");
                return;
            }
            threeMonthTestsWindow1 monthsTests = new threeMonthTestsWindow1((Tester)findGrid.SelectedItem);
            monthsTests.Show();
        }


    }
}
