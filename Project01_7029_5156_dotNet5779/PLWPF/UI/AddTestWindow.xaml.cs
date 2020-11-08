using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Net;
using System.Xml;
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTestWindow.xaml
    /// </summary>
    public partial class AddTestWindow : Window
    {

        BL.IBL bl;
        IEnumerable<Tester> testers = new List<Tester>();
        List<Tester> validDistance = new List<Tester>();

        private Test test;
        public AddTestWindow(Test t)
        {
            bl = BL.FactoryBL.GetBL();

            InitializeComponent();
            textBox_Code.Text = t.Code.ToString();

            textBox_IDTrainee.Text = t.Id_trainee;
            textBox_IDTrainee.IsEnabled = false;

            textBox_IDTester.Text = t.Id_tester;
            textBox_IDTester.IsEnabled = false;

            DatePicker_Date.Text = t.TestDate.ToString();
            DatePicker_Date.IsEnabled = false;

            comboBoxHour.SelectedItem = t.TestHour.ToString();
            comboBoxHour.IsEnabled = false;

            textBoxaddress_city.Text = t.AddressExit.city;
            textBoxaddress_city.IsEnabled = false;

            textBoxaddress_number.Text = t.AddressExit.number.ToString();
            textBoxaddress_number.IsEnabled = false;

            textBoxaddress_street.Text = t.AddressExit.street;
            textBoxaddress_street.IsEnabled = false;

            SAVE.Visibility = Visibility.Hidden;
            Update.Visibility = Visibility.Visible;


            UpDateVisibility();
            test = new Test();
            test = t;

        }

        public AddTestWindow()
        {

            InitializeComponent();
            test = new Test();
            bl = BL.FactoryBL.GetBL();
            testers = bl.GetAllTesters();
            textBox_Code.Text = Configuration.testCode.ToString();
        }
        public void UpDateVisibility()
        {
            label_Note.Visibility = Visibility.Visible;
            textBox_Note.Visibility = Visibility.Visible;

            label_idTester.Visibility = Visibility.Visible;
            textBox_IDTester.Visibility = Visibility.Visible;

            checkBoxKeepDistance.Visibility = Visibility.Visible;
            groupBoxDrivingCriteria.Visibility = Visibility.Visible;
            checkBoxReverseParking.Visibility = Visibility.Visible;
            checkBoxMirrorsWatcing.Visibility = Visibility.Visible;
            checkBoxSigns.Visibility = Visibility.Visible;
            checkBoxIsAllowedSpeed.Visibility = Visibility.Visible;
            checkBoxIsPass.Visibility = Visibility.Visible;
        }

        public void myFuc()
        {
            foreach (Tester t in testers)
            {
                Distance distance = new Distance(t, test.AddressExit, t.AddressTester);
                //distance.addressExit = test.AddressExit;
                //distance.addressLiving = t.AddressTester;
                distance.list = validDistance;
                // distance.tester = t;
                distance.Show();
            }
        }
        public bool IsValid()
        {

            bool valid = true;

            try
            {
                if (textBox_IDTrainee.Text == "")
                    throw new Exception("השדה ריק");
                test.Id_trainee = textBox_IDTrainee.Text;
                lbErrIDTrainee.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                lbErrIDTrainee.Content = ex.Message;
                lbErrIDTrainee.Visibility = Visibility.Visible;
                valid = false;

            }

            try
            {
                if (DatePicker_Date.DisplayDate == null)
                    throw new Exception("השדה ריק");
                if (DatePicker_Date.DisplayDate.DayOfWeek.GetHashCode() >= 5)
                    throw new Exception("It is impossible to choose a date at Friday or Saturday");
                test.TestDate = DatePicker_Date.DisplayDate;
                lbErrDate.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                lbErrDate.Content = ex.Message;
                lbErrDate.Visibility = Visibility.Visible;
                valid = false;
            }
            try
            {
                if (comboBoxHour.SelectedItem == null)
                    throw new Exception("בחר שעה");
                test.TestHour = new DateTime(2000, 12, 12, ((int.Parse((comboBoxHour.Text[0]).ToString())) * 10 + (int.Parse((comboBoxHour.Text[1]).ToString()))), 0, 0);
            }
            catch (Exception ex)
            {
                lbErrHour.Content = ex.Message;
                lbErrHour.Visibility = Visibility.Visible;
                valid = false;
            }

            try
            {
                switch (comboBoxKind.Text)
                {

                    case "A":
                        test.VehicleKind = VehicleKind.A;
                        test.GearboxKind = GearboxKind.automatic;
                        break;

                    case "B":
                        test.VehicleKind = VehicleKind.B;
                        if (radioButton_automatic.IsChecked == true)
                            test.GearboxKind = GearboxKind.automatic;
                        else
                            test.GearboxKind = GearboxKind.manual;
                        break;

                    case "C":
                        test.VehicleKind = VehicleKind.C;
                        if (radioButton_automatic.IsChecked == true)
                            test.GearboxKind = GearboxKind.automatic;
                        else
                            test.GearboxKind = GearboxKind.manual;
                        break;

                    case "C1":
                        test.VehicleKind = VehicleKind.C1;
                        if (radioButton_automatic.IsChecked == true)
                            test.GearboxKind = GearboxKind.automatic;
                        else
                            test.GearboxKind = GearboxKind.manual;
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
                if (IsValid())
                {
                    bool success;

                    success = bl.AddTest(test, validDistance);
                    if (!success)
                    {

                        MessageBoxResult result = MessageBox.Show(" לא נמצא בוחן פנוי בתאריך ושעה המבוקשים, האם ברצונך שהמערכת תמצא מועד אחר לקביעת המבחן?", "אישור", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            DateTime date;
                            date = bl.SuggestAnotherDateForTest(test.TestDate, test.TestHour, test.VehicleKind, validDistance);
                            DatePicker_Date.Text = date.ToString();
                            comboBoxHour.Text = (date.Hour.ToString("00") + ":00");
                        }
                        return;
                    }

                    test = new Test();
                    lblUpdateMesssage.Content = "נוסף בהצלחה";
                    lblUpdateMesssage.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                lblUpdateMesssage.Content = ex.Message;
                lblUpdateMesssage.Visibility = Visibility.Visible;
            }

        }
        public bool IsValidUpdate()
        {

            bool valid = true;


            if (checkBoxIsAllowedSpeed.IsChecked == true)
                test.DrivingCheck.isAllowedSpeed = true;
            else
                test.DrivingCheck.isAllowedSpeed = false;

            if (checkBoxKeepDistance.IsChecked == true)
                test.DrivingCheck.keepDistance = true;
            else
                test.DrivingCheck.keepDistance = false;

            if (checkBoxMirrorsWatcing.IsChecked == true)
                test.DrivingCheck.mirrorsWatcing = true;
            else
                test.DrivingCheck.mirrorsWatcing = false;

            if (checkBoxReverseParking.IsChecked == true)
                test.DrivingCheck.reverseParking = true;
            else
                test.DrivingCheck.reverseParking = false;

            if (checkBoxSigns.IsChecked == true)
                test.DrivingCheck.signs = true;
            else
                test.DrivingCheck.signs = false;

            try
            {
                if (textBox_Note.Text == "")
                    throw new Exception("השדה ריק");
                test.TesterNote = textBox_Note.Text;
                lbErrNote.Visibility = Visibility.Hidden;

            }
            catch (Exception ex)
            {
                lbErrNote.Content = ex.Message;
                lbErrNote.Visibility = Visibility.Visible;
                valid = false;
            }
            if (checkBoxIsPass.IsChecked == true)
                test.IsPass = true;
            else
                test.IsPass = false;
            return valid;
        }
        private void button_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValidUpdate())
                {
                    bool success;
                    success = bl.UpdateTest(test);
                    if (success)
                    {
                        lblUpdateMesssage.Content = "עודכן בהצלחה";
                        lblUpdateMesssage.Visibility = Visibility.Visible;
                    }
                    test = new Test();

                }
            }
            catch (Exception ex)
            {
                lblUpdateMesssage.Content = ex.Message;
                lblUpdateMesssage.Visibility = Visibility.Visible;
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxaddress_street.Text == "" || textBoxaddress_number.Text == "" || BE.Validation.IsHebrewLetters(textBoxaddress_number.Text) || textBoxaddress_city.Text == "")
                    throw new Exception("השדה ריק");
                test.AddressExit = new Address(textBoxaddress_street.Text, Convert.ToInt32(textBoxaddress_number.Text), textBoxaddress_city.Text);
                lbErrAddress.Visibility = Visibility.Hidden;
            }

            catch (Exception ex)
            {
                lbErrAddress.Content = ex.Message;
                lbErrAddress.Visibility = Visibility.Visible;
            }
            textBoxaddress_street.IsEnabled = false;
            textBoxaddress_number.IsEnabled = false;
            textBoxaddress_city.IsEnabled = false;
            myFuc();
            SAVE.IsEnabled = true;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}