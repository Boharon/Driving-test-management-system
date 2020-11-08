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
    public class Tester
    {
        #region Properties_Tester
        private string id;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private Gender gender;
        private string phoneNumber;
        private Address addressTester;
        private double experienceYears;
        private int maxWeeklyTests;
        private VehicleKind vehicleKind;
        private bool[ , ] scheduleTester;
        private double maxDistance;
        private List<Test> testsOfTester;

        public Tester()
        {
            id = "";
            firstName = "";
            lastName = "";
            phoneNumber = "";
            scheduleTester = new bool[5, 7];
            TestsOfTester = new List<Test>();
        }


        public string Id 
        {

            get { return id; }
            set
            {
                if (!Validation.IsIdentify(value))
                    throw new Exception("Id is not valid");
                id = value;
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
        public Address AddressTester
        {

            get { return addressTester; }
            set
            {
                if (!Validation.IsHebrewLettersAndNumbers(value.ToString()))
                    throw new Exception("Address is not valid");
                addressTester = value;
            }
        }
        public double ExperienceYears
        {

            get { return experienceYears; }
            set { experienceYears = value; }
        }
        public int MaxWeeklyTests
        {
            get { return maxWeeklyTests; }
            set { maxWeeklyTests = value; }

        }
        public VehicleKind VehicleKind
        {
            get { return vehicleKind; }
            set { vehicleKind = value; }
        }
        [XmlIgnore]
        public bool[,] ScheduleTester
        {
            get { return scheduleTester; }
            set { scheduleTester = value; }
        }
        public double MaxDistance
        {
            get { return maxDistance; }
            set { maxDistance = value; }
        }
        [XmlIgnore]
        public List<Test> TestsOfTester
        {
            get { return testsOfTester; }
            set { testsOfTester = value; }
        }
		#endregion

		#region Actions ; Get & Set
		public string TempScheduleTester
        {
            get
            {
               if (ScheduleTester == null)
                  return null;
                string result = "";
                if (ScheduleTester != null)
                {
                    int sizeA = ScheduleTester.GetLength(0);
                    int sizeB = ScheduleTester.GetLength(1);
                    result += "" + sizeA + "," + sizeB;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            result += "," + ScheduleTester[i, j];
                }
                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    ScheduleTester = new bool[sizeA, sizeB];
                    int index = 2;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            ScheduleTester[i, j] = bool.Parse(values[index++]);
                }
            }
        }
       
        public string TempTestsOfTester
        {
            get { return ToXMLstring<List<Test>>(TestsOfTester); }
            set { TestsOfTester = ToObject<List<Test>>(value); }

        }
        
        


        string ToXMLstring<T>(T toSerialize)
        {
            StringWriter textWriter = new StringWriter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(textWriter, toSerialize);
            string result = textWriter.ToString();
            textWriter.Close();
            return result;
        }
        T ToObject<T>(string toDeserialize)
        {
            StringReader textReader = new StringReader(toDeserialize);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(textReader);
            textReader.Close();
            return result;
        }

        public override string ToString()
        {
            return "Tester Details:\n ID: " + id + " First Name: " + firstName + " Last Name: " + lastName + " Birth Date: " + birthDate + " Gender: " + gender + " Phone Number: " + phoneNumber + " Address: " + AddressTester + " Experience Years: "+ experienceYears+ " Max Weekly Tests: "+ maxWeeklyTests+ " Vehicle Kind: "+ vehicleKind+ " Max Distance: "+ maxDistance;
        }
		#endregion
	}
}
