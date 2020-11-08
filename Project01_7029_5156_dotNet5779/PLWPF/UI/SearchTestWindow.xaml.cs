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
    public partial class SearchTestWindow : Window
    {
        BL.IBL bl;
        public bool manage;
        Test test = new Test();
        public SearchTestWindow()
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearchCode.Text == "")
                    throw new Exception("יש להקיש קוד לחיפוש");

                IEnumerable<Test> find = bl.GetTestByPrefixId(txtSearchCode.Text);
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
                MessageBoxResult result = MessageBox.Show("טסט זה ימחק. האם אתה בטוח?", "אישור", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.RemoveTest(((Test)findGrid.SelectedItem).Code, ((Test)findGrid.SelectedItem).TestDate);
                    MessageBox.Show("התלמיד נמחק בהצלחה !", "אישור");
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
    }
}
