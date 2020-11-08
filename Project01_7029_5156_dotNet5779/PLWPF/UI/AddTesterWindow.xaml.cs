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
using System.Threading;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTesterWindow.xaml
    /// </summary>
    public partial class AddTesterWindow : Window
    {
        BL.IBL bl;
        private Tester tester;
        private Tester t = null;
        public AddTesterWindow(Tester t)
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();
            textBox_ID.Text = t.Id;
            textBox_FirstName.Text = t.FirstName;
            textBox_LastName.Text = t.LastName;
            DatePicker_birthDate.Text = t.BirthDate.ToString();
            if (t.Gender == Gender.male)
                radioButton_gender_male.IsChecked = true;
            else
                radioButton_gender_female.IsChecked = true;
            textBox_PhoneNumber.Text = t.PhoneNumber;
            textBox_Experience.Text = t.ExperienceYears.ToString();
            textBox_MaxLessons.Text = t.MaxWeeklyTests.ToString();
            textBox_MaxDistance.Text = t.MaxDistance.ToString();
            textBoxaddress_Tester_city.Text = t.AddressTester.city;
            textBoxaddress_Tester_number.Text = t.AddressTester.number.ToString();
            textBoxaddress_Tester_street.Text = t.AddressTester.street;
            comboBoxKind.Text = t.VehicleKind.ToString();
            tester = new Tester();
            tester.ScheduleTester = t.ScheduleTester;
            textBox_ID.IsEnabled = false;

        }
        public AddTesterWindow()
        {
            InitializeComponent();
            tester = new Tester();
            bl = BL.FactoryBL.GetBL();

        }

        public bool IsValid()
        {

            bool valid = true;
            try
            {
                if (textBox_ID.Text == "")
                    throw new Exception("השדה ריק");
                tester.Id = textBox_ID.Text;
                lbErrID.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                lbErrID.Content = ex.Message;
                lbErrID.Visibility = Visibility.Visible;
                valid = false;
            }
            try
            {
                if (textBox_FirstName.Text == "")
                    throw new Exception("השדה ריק");
                tester.FirstName = textBox_FirstName.Text;
                lbErrLname.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                lbErrLname.Content = ex.Message;
                lbErrLname.Visibility = Visibility.Visible;
                valid = false;

            }
            try
            {
                if (textBox_LastName.Text == "")
                    throw new Exception("השדה ריק");
                tester.LastName = textBox_LastName.Text;
                lbErrFname.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {

                lbErrFname.Content = ex.Message;
                lbErrFname.Visibility = Visibility.Visible;
                valid = false;
            }
            try
            {
                if (DatePicker_birthDate.DisplayDate == null)
                    throw new Exception("השדה ריק");
                tester.BirthDate = DatePicker_birthDate.DisplayDate;
                lbErrBirthDate.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                lbErrBirthDate.Content = ex.Message;
                lbErrBirthDate.Visibility = Visibility.Visible;
                valid = false;
            }
            try
            {
                if (radioButton_gender_male.IsChecked == true)
                    tester.Gender = Gender.male;
                else
                {
                    if (radioButton_gender_female.IsChecked == true)
                        tester.Gender = Gender.female;
                    else
                        throw new Exception("השדה ריק");
                }
                lbErrGender.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                lbErrGender.Content = ex.Message;
                lbErrGender.Visibility = Visibility.Visible;
                valid = false;
            }
            try
            {
                if (textBox_PhoneNumber.Text == "")
                    throw new Exception("השדה ריק");
                tester.PhoneNumber = textBox_PhoneNumber.Text;
                lbErrPhone.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                lbErrPhone.Content = ex.Message;
                lbErrPhone.Visibility = Visibility.Visible;
                valid = false;
            }
            try
            {
                if (textBoxaddress_Tester_street.Text == "" || textBoxaddress_Tester_number.Text == "" || BE.Validation.IsHebrewLetters(textBoxaddress_Tester_number.Text) || textBoxaddress_Tester_city.Text == "")
                    throw new Exception("השדה ריק");
                tester.AddressTester = new Address(textBoxaddress_Tester_street.Text, Convert.ToInt32(textBoxaddress_Tester_number.Text), textBoxaddress_Tester_city.Text);
                lbErrAddress.Visibility = Visibility.Hidden;
            }

            catch (Exception ex)
            {
                lbErrAddress.Content = ex.Message;
                lbErrAddress.Visibility = Visibility.Visible;
                valid = false;

            }
            try
            {
                if (textBox_Experience.Text == "")
                    throw new Exception("השדה ריק");
                tester.ExperienceYears = Convert.ToDouble(textBox_Experience.Text);
                lbErrExperience.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                lbErrExperience.Content = ex.Message;
                lbErrExperience.Visibility = Visibility.Visible;
                valid = false;
            }

            try
            {
                if (textBox_MaxLessons.Text == "")
                    throw new Exception("השדה ריק");
                tester.MaxWeeklyTests = Convert.ToInt32(textBox_MaxLessons.Text);
                lbErrMaxLessons.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                lbErrMaxLessons.Content = ex.Message;
                lbErrMaxLessons.Visibility = Visibility.Visible;
                valid = false;
            }

            try
            {
                if (textBox_MaxDistance.Text == "")
                    throw new Exception("השדה ריק");
                tester.MaxDistance = Convert.ToDouble(textBox_MaxDistance.Text);
                lbErrMaxDistance.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                lbErrMaxDistance.Content = ex.Message;
                lbErrMaxDistance.Visibility = Visibility.Visible;
                valid = false;
            }

            try
            {
                switch (comboBoxKind.Text)
                {

                    case "A":
                        tester.VehicleKind = VehicleKind.A;
                        break;

                    case "B":
                        tester.VehicleKind = VehicleKind.B;
                        break;

                    case "C":
                        tester.VehicleKind = VehicleKind.C;
                        break;


                    case "C1":
                        tester.VehicleKind = VehicleKind.C1;
                        break;


                    default:
                        throw new Exception("בחר סוג כלי רכב");

                }
            }
            catch (Exception ex)
            {
                lbErrKind.Content = ex.Message;
                lbErrKind.Visibility = Visibility.Visible;
                valid = false;
            }
            return valid;
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                AddStatus addstatus = AddStatus.Add;

                if (IsValid())
                {
                    addstatus = bl.AddTester(tester);
                    tester = new Tester();
                    switch (addstatus)
                    {
                        case AddStatus.Add:
                            lblUpdateMesssage.Content = "נוסף בהצלחה";
                            lblUpdateMesssage.Visibility = Visibility.Visible;
                            break;
                        case AddStatus.Update:
                            lblUpdateMesssage.Content = "עודכן בהצלחה";
                            lblUpdateMesssage.Visibility = Visibility.Visible;
                            break;
                        default:
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                lblUpdateMesssage.Content = ex.Message;
                lblUpdateMesssage.Visibility = Visibility.Visible;
            }

			Thread.Sleep(2000);
			this.Close();

        }

        private void button_Click1(object sender, RoutedEventArgs e)
        {
            ScheduleWindow schedule = new ScheduleWindow(tester);
            schedule.tester = tester;
            schedule.ShowDialog();
            SAVE.IsEnabled = true;
        }

		private void btnBack_Click1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
