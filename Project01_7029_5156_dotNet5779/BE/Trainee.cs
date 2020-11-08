using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;


namespace BE
{
    public class Trainee
    {
        #region Properties_Trainee
        private string id;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private Gender gender;
        private string phoneNumber;
        private Address addressTrainee;
        private Drivinglessons[] drivinglessons;
		#endregion

		#region Actions ; Get & Set
		public Trainee()
        {
            id = "";
            firstName = "";
            lastName = "";
            phoneNumber = "";
            drivinglessons = new Drivinglessons[4];

        }
        public string Id
        {
            get { return id; }
            set
            {
                if (!(Validation.IsIdentify(value)))
                    throw new Exception("Id is not valid");
                id=value;
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (!Validation.IsHebrewLetters(value))
                    throw new Exception("First Name is not valid");
                firstName = value;
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (!Validation.IsHebrewLetters(value))
                    throw new Exception("Last Name is not valid");
                lastName = value;
            }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (!Validation.IsDateValid(value))
                    throw new Exception("Birth date is not valid");
                if(!Validation.checkTraineeAge(value))
                    throw new Exception("Trainee age is not valid");
                birthDate = value;
            }
        }
        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
            
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (!Validation.IsPhoneNumber(value))
                    throw new Exception("Phone nuber is not valid");
                phoneNumber = value;
            }
        }
        public Address AddressTrainee
        {

            get { return addressTrainee; }
            set
            {
                if (!Validation.IsHebrewLettersAndNumbers(value.ToString()))
                    throw new Exception("Address is not valid");
                addressTrainee = value;
            }
        }

        public Drivinglessons[] Drivinglesson
        {
            get { return drivinglessons; }
            set { drivinglessons = value; }
    }
        public string TempGender
        {
            get { return Gender.ToString(); }
            set
            {
                if (value == "female")
                    Gender = Gender.female;
                Gender = Gender.male;
            }
        }
        public string TempAddress
        {
            get { return AddressTrainee.ToStringFromXML(); }
            set
            {
                int i = 0;
                string number = "";
                while (value[i] != ',')
                {
                    addressTrainee.street += value[i];
                    i++;
                }
                i++;
                while (value[i] != ',')
                {
                    number += value[i];
                    i++;
                }
                i++;
                addressTrainee.number = int.Parse(number);
                while (i < value.Length)
                {
                    addressTrainee.city += value[i];
                    i++;
                }

            }
        }
        public string TempDrivinglessons
        {
            get
            {
                //int mone = 0;
                string result = "";
                foreach (Drivinglessons d in drivinglessons)
                {
                    result += d.ToStringFromXml();
                    result += ",";
                }
                return result;
            }
            set
            {

                int j = 0, i = 0;
                string[] values = value.Split(',');
                while (j < values.Length-1)
                {
                    drivinglessons[i].VehicleKind = (VehicleKind)(i);
                    j++;
                    if (values[j] == "automatic")
                        drivinglessons[i].GearboxKind = GearboxKind.automatic;
                    else
                        drivinglessons[i].GearboxKind = GearboxKind.manual;
                    j++;
                    drivinglessons[i].DrivingSchool = values[j];
                    j++;
                    drivinglessons[i].Teacher = values[j];
                    j++;
                    drivinglessons[i].Lessons = int.Parse(values[j]);
                    j++;
                    drivinglessons[i].IsPass = bool.Parse(values[j]);
                    j++;
                    i++;
                }
            }
        }


        public override string ToString()
        {
            string s = "";
            string details = "Trainee Details:\n ID: " + id + " First Name: " + firstName + " Last Name: " + lastName + " Birth Date: " + birthDate + " Gender: " + gender + " Phone Number: " + phoneNumber + " Address: " + AddressTrainee;
            for (int i = 0; i < drivinglessons.Length; i++)
            {
                s = drivinglessons[i].ToString();
            }
            return details + s;

        }
		#endregion
	}

}
