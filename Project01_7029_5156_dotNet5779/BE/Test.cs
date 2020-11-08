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
    public class Test
    {

        #region Properties_Test
        private long code;
        private string id_tester;
        private string id_trainee;
        private DateTime testDate;
        private DateTime testHour;
        private Address addressExit;
        private VehicleKind vehicleKind;
        private GearboxKind gearboxKind;

        private string testerNote;
        public DrivingCriterion DrivingCheck;

        public bool IsPass;
		#endregion


		#region Actions ; Get & Set
		public Test()
        {
            id_tester = "";
            id_trainee = "";
            testerNote = "";
         }

        public long Code
        {
            get { return code; }
            set { code = value; }

        }
        public string Id_tester
        {
            get { return id_tester; }
            set { id_tester = value;}
        }
        public string Id_trainee
        {
            get { return id_trainee; }
            set { id_trainee = value; }
        }
        public DateTime TestDate
        {
            get { return testDate; }
            set { testDate = value; }
        }
        public DateTime TestHour
        {
            get { return testHour; }
            set { testHour = value; }
        }

        public Address AddressExit
        {
            get { return addressExit; }
            set
            {
                if (!Validation.IsHebrewLettersAndNumbers(value.ToString()))
                    throw new Exception("Address is not valid");
                addressExit = value;
            }
        }
        public VehicleKind VehicleKind
        {
            get { return vehicleKind; }
            set { vehicleKind = value; }

        }
        public GearboxKind GearboxKind
        {
            get { return gearboxKind; }
            set { gearboxKind = value; }
        }
        public  string TesterNote
        {
            get { return testerNote; }
            set { testerNote = value; }
        }

        //public string TempDrivingCheck
        //{
        //    get { return DrivingCheck.ToString(); }
        //    set
        //    {
        //        if (value != null && value.Length > 0)
        //        {
        //            string[] values = value.Split(',');
        //            DrivingCheck.keepDistance = bool.Parse(values[0]);
        //            DrivingCheck.reverseParking = bool.Parse(values[1]);
        //            DrivingCheck.mirrorsWatcing = bool.Parse(values[2]);
        //            DrivingCheck.signs = bool.Parse(values[3]);
        //            DrivingCheck.isAllowedSpeed = bool.Parse(values[4]);
        //        }
        //    }
        //}
		#endregion
	}
}
