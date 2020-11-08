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
    public partial class SearchTrainee : Window
    {
        BL.IBL bl;
        bool manage;
        Trainee trainee = new Trainee();
        public SearchTrainee(bool ismanage)
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
            IEnumerable<Trainee> find;
            try
            {
                if ((txtSearchID.Text == ""))
                {
                    if (txtSearchF.Text == "")
                    {
                        if (txtSearchL.Text == "")
                        {
                            throw new Exception("יש להזין ערכים לחיפוש");
                        }
                        else
                        {
                            find = bl.searchTraineeLname(txtSearchL.Text);
                        }
                    }
                    else
                    {
                        find = bl.searchTraineeFname(txtSearchF.Text);
                    }
                }
                else
                {
                    find = bl.GetTraineeByPrefixId(txtSearchID.Text);
                }

                findGrid.ItemsSource = find;

                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
            //trainee = bl.searchTrainee(txtSearch.Text);
            if (trainee == null)
                throw new Exception("התלמיד לא קיים במערכת");
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
                MessageBox.Show("יש לבחור תלמיד לעידכון");
                return;
            }
            Window addTrainee = new AddTraineeWindow((Trainee)findGrid.SelectedItem);
            addTrainee.Show();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (findGrid.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור תלמיד למחיקה");
                return;
            }
            try
            {
                MessageBoxResult result = MessageBox.Show(" תלמיד זה ימחק. האם אתה בטוח?", "אישור", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.RemoveTrainee(((Trainee)findGrid.SelectedItem).Id);
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
