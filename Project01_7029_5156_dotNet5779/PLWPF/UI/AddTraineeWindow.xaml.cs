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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;
using System.Threading;
namespace PLWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddTraineeWindow : Window
    {
        BL.IBL bl;

        private Trainee trainee;
        private Trainee tToUd = new Trainee();
        public AddTraineeWindow(Trainee t)
        {
            bl = BL.FactoryBL.GetBL();
            InitializeComponent();
            tToUd = t;
            textBox_ID.Text = t.Id;
            textBox_FirstName.Text = t.FirstName;
            textBox_LastName.Text = t.LastName;
            DatePicker_birthDate.Text = t.BirthDate.ToString();
            if (t.Gender == Gender.male)
                radioButton_gender_male.IsChecked = true;
            else
                radioButton_gender_female.IsChecked = true;
            textBox_PhoneNumber.Text = t.PhoneNumber;
            textBoxaddress_Trainee_city.Text = t.AddressTrainee.city;
            textBoxaddress_Trainee_number.Text = t.AddressTrainee.number.ToString();
            textBoxaddress_Trainee_street.Text = t.AddressTrainee.street;

            lbErrDrivingLesson.Content = "בחר סוג כלי רכב";
            lbErrDrivingLesson.Visibility = Visibility.Visible;
            tToUd = t;
            trainee = new Trainee();

        }
        public AddTraineeWindow()
        {
            InitializeComponent();
            trainee = new Trainee();
            bl = BL.FactoryBL.GetBL();

        }
        public void GearVisibility()
        {
            groupBox_gear.Visibility = Visibility.Visible;
            radioButton_automatic.Visibility = Visibility.Visible;
            radioButton_manual.Visibility = Visibility.Visible;
        }
        public void GearUnvisibility()
        {
            groupBox_gear.Visibility = Visibility.Hidden;
            radioButton_automatic.Visibility = Visibility.Hidden;
            radioButton_manual.Visibility = Visibility.Hidden;
        }
        public bool IsValid()
        {

            bool valid = true;
            try
            {
                if (textBox_ID.Text == "")
                    throw new Exception("השדה ריק");
                trainee.Id = textBox_ID.Text;
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
                trainee.FirstName = textBox_FirstName.Text;
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
                trainee.LastName = textBox_LastName.Text;
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
                trainee.BirthDate = DatePicker_birthDate.DisplayDate.Date;
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
                    trainee.Gender = Gender.male;
                else
                {
                    if (radioButton_gender_female.IsChecked == true)
                        trainee.Gender = Gender.female;
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
                trainee.PhoneNumber = textBox_PhoneNumber.Text;
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
                if (textBoxaddress_Trainee_street.Text == "" || textBoxaddress_Trainee_number.Text == "" || BE.Validation.IsHebrewLetters(textBoxaddress_Trainee_number.Text) || textBoxaddress_Trainee_city.Text == "")
                    throw new Exception("השדה ריק");
                trainee.AddressTrainee = new Address(textBoxaddress_Trainee_street.Text, Convert.ToInt32(textBoxaddress_Trainee_number.Text), textBoxaddress_Trainee_city.Text);
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
                GearboxKind gearbox;
                switch (comboBoxKind.Text)
                {

                    case "A":
                        trainee.Drivinglesson[0] = new Drivinglessons(VehicleKind.A, GearboxKind.automatic, textBox_school.Text, textBox_teacher.Text, Convert.ToInt32(textBox_numberLesson.Text));
                        break;

                    case "B":
                        GearVisibility();
                        if (radioButton_automatic.IsChecked == true)
                            gearbox = GearboxKind.automatic;
                        else
                            gearbox = GearboxKind.manual;
                        trainee.Drivinglesson[1] = new Drivinglessons(VehicleKind.B, gearbox, textBox_school.Text, textBox_teacher.Text, Convert.ToInt32(textBox_numberLesson.Text));
                        break;

                    case "C":
                        GearVisibility();
                        if (radioButton_automatic.IsChecked == true)
                            gearbox = GearboxKind.automatic;
                        else
                            gearbox = GearboxKind.manual;

                        trainee.Drivinglesson[2] = new Drivinglessons(VehicleKind.C, gearbox, textBox_school.Text, textBox_teacher.Text, Convert.ToInt32(textBox_numberLesson.Text));
                        break;

                    case "C1":
                        GearVisibility();
                        if (radioButton_automatic.IsChecked == true)
                            gearbox = GearboxKind.automatic;
                        else
                            gearbox = GearboxKind.manual;

                        trainee.Drivinglesson[3] = new Drivinglessons(VehicleKind.C1, gearbox, textBox_school.Text, textBox_teacher.Text, Convert.ToInt32(textBox_numberLesson.Text));
                        break;

                    default:
                        throw new Exception("בחר סוג כלי רכב");

                }
                lbErrDrivingLesson.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                lbErrDrivingLesson.Content = ex.Message;
                lbErrDrivingLesson.Visibility = Visibility.Visible;
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
                    addstatus = bl.AddTrainee(trainee);
                    trainee = new Trainee();
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
        }


        private void comboBoxKind_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBoxKind.SelectedIndex != 0)
                GearVisibility();
            else
                GearUnvisibility();

            if (tToUd != null)
            {
                lbErrDrivingLesson.Visibility = Visibility.Hidden;
                switch (comboBoxKind.Text)
                {
                    case "":
                        break;
                    case "A":
                        textBox_school.Text = tToUd.Drivinglesson[0].DrivingSchool;
                        textBox_teacher.Text = tToUd.Drivinglesson[0].Teacher;
                        textBox_numberLesson.Text = tToUd.Drivinglesson[0].Lessons.ToString();
                        break;

                    case "B":
                        GearVisibility();
                        textBox_school.Text = tToUd.Drivinglesson[1].DrivingSchool;
                        textBox_teacher.Text = tToUd.Drivinglesson[1].Teacher;
                        textBox_numberLesson.Text = tToUd.Drivinglesson[1].Lessons.ToString();

                        if (tToUd.Drivinglesson[1].GearboxKind == GearboxKind.automatic)
                            radioButton_automatic.IsChecked = true;
                        else
                            radioButton_manual.IsChecked = true;
                        break;

                    case "C":
                        GearVisibility();
                        textBox_school.Text = tToUd.Drivinglesson[2].DrivingSchool;
                        textBox_teacher.Text = tToUd.Drivinglesson[2].Teacher;
                        textBox_numberLesson.Text = tToUd.Drivinglesson[2].Lessons.ToString();

                        if (tToUd.Drivinglesson[2].GearboxKind == GearboxKind.automatic)
                            radioButton_automatic.IsChecked = true;
                        else
                            radioButton_manual.IsChecked = true;
                        break;

                    case "C1":
                        GearVisibility();
                        textBox_school.Text = tToUd.Drivinglesson[3].DrivingSchool;
                        textBox_teacher.Text = tToUd.Drivinglesson[3].Teacher;
                        textBox_numberLesson.Text = tToUd.Drivinglesson[3].Lessons.ToString();

                        if (tToUd.Drivinglesson[3].GearboxKind == GearboxKind.automatic)
                            radioButton_automatic.IsChecked = true;
                        else
                            radioButton_manual.IsChecked = true;
                        break;
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
