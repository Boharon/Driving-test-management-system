using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
	#region Driving Criterion
	public struct DrivingCriterion
    {
        public bool keepDistance;
        public bool reverseParking;
        public bool mirrorsWatcing;
        public bool signs;
        public bool isAllowedSpeed;


        public DrivingCriterion(bool a, bool b, bool c, bool d, bool e)
        {
            keepDistance = a;
            reverseParking = b;
            mirrorsWatcing = c;
            signs = d;
            isAllowedSpeed = e;
        }
        public override string ToString()
        {
            return keepDistance + "," + reverseParking + "," + mirrorsWatcing + "," + signs + "," + isAllowedSpeed;
        }
      }

	#endregion

	#region Driving Lessons
	public struct Drivinglessons
    {
        public VehicleKind VehicleKind;
        public GearboxKind GearboxKind;
        public string DrivingSchool;
        public string Teacher;
        public int Lessons;
        public bool IsPass;

        public Drivinglessons(VehicleKind MyVehicleKind, GearboxKind MyGearboxKind,string MyDrivingSchool, string MyTeacher, int MyLessons)
        {
            VehicleKind = MyVehicleKind;
            GearboxKind = MyGearboxKind;
            DrivingSchool = MyDrivingSchool;
            Teacher = MyTeacher;
            Lessons = MyLessons;
            IsPass = false;
        }

        //public override string ToString()
        //{
        //    return "Driving Lessons Details:\n Vehicle Kind: " + VehicleKind + " Gearbox Kind: " + GearboxKind + " Driving School: " + DrivingSchool + " Teacher: " + Teacher + "Number of Lessons: " + Lessons + " IsPass: " + IsPass;
        //}
		public string ToStringFromXml()
		{
		return VehicleKind + "," + GearboxKind + "," + DrivingSchool + "," + Teacher + "," + Lessons + "," + IsPass;
		}

	}

	#endregion

	#region Address
	public struct Address
	{
		public string street;
		public int number;
		public string city;

		public Address(string s, int n, string c)
		{
			street = s;
			number = n;
			city = c;
		}
		public override string ToString()
		{
			return street + " " + number + " " + city;
		}
        public  string ToStringFromXML()
        {
            return street + "," + number + "," + city;
        }
    }
	#endregion

	#region Enums

	public enum GearboxKind { automatic, manual };

    public enum Gender { male, female };

    public enum VehicleKind {A,B,C,C1 };//A- two-wheeled vehicle ; B-Private car ; C-heavy truck ;C1 - Medium truck;
    
    public enum AddStatus {Add,Update}; // Define the status of the window

	#endregion
}
