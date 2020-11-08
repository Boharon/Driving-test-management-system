using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using BE;
using DAL;

namespace BL
{
    public class FactoryBL
    {
        static IBL bl = null;
        public static IBL GetBL()
        {
            bl = new IBL_IMP();
            return bl;
        }
    }

    public class IBL_IMP : IBL
    {
        DAL.IDAL dal;
        public IBL_IMP()
        {
            dal = DAL.FactoryDal.GetDal();
        }


        #region Tester
        public AddStatus AddTester(Tester t)
        {
            AddStatus addStatus = AddStatus.Add;
            Tester tester = null;
            tester = dal.searchTester(t.Id);
            if (tester != null)
            {
                addStatus = AddStatus.Update;
                UpdateTester(t);
            }
            else
            {
                dal.AddTester(t);
            }
            return addStatus;
        }
        public bool RemoveTester(string id)
        {
            return dal.RemoveTester(id);
        }
        public bool UpdateTester(Tester tester)
        {
            //if (!CheckTesterAge(tester))
            //    throw new Exception("Tester age is not valid");
            dal.UpdateTester(tester);
            return true;
        }
        #endregion

        #region Trainee
        public AddStatus AddTrainee(Trainee t)
        {
          
            AddStatus addStatus = AddStatus.Add;
            Trainee trainee = null;
            trainee = dal.searchTrainee(t.Id);
            if (trainee != null)
            {
                addStatus = AddStatus.Update;
                UpdateTrainee(t);
            }
            else
            {
                dal.AddTrainee(t);
            }
            return addStatus;

        }
        public bool RemoveTrainee(string id)
        {
            return dal.RemoveTrainee(id);
        }
        public AddStatus UpdateTrainee(Trainee trainee)
        {
            //if (!checkTraineeAge(trainee))
            //    throw new Exception("Trainee age is not valid");
            dal.UpdateTrainee(trainee);
            return AddStatus.Update;
        }

        #endregion

        #region Test
        public bool RemoveTest(long code,DateTime date)
        {
            if (DateTime.Now > date)
                throw new Exception("לא ניתן למחוק מבחן כבר התרחש");


            return dal.RemoveTest(code);
        }
        public bool isRangeTestValid(Test t)
        {
            List<Test> MyList;
            MyList = GetAllTestsOfTrainee(t.Id_trainee).ToList();
            int i = MyList.Count();
            if (i == 0)
                return true;
            if (((MyList[i-1].TestDate.AddDays(Configuration.rangeOfTests)) > t.TestDate)&&((MyList[i - 1].TestDate< t.TestDate)))
                return false;
            return true;
        }
        public bool isTraineeAvailable(string id, DateTime d)
        {
            IEnumerable <Test> MyList;
            MyList = GetAllTestsOfTrainee(id);
            foreach (Test t in MyList)
                if (t.TestDate == d)
                    return false;
            return true;
        }

        public int checkTheKind(string id, Test test)
        {
            Trainee trainee = dal.searchTrainee(id);
            for (int i = 0; i < 4; i++)
                if (trainee.Drivinglesson[i].VehicleKind == test.VehicleKind)
                    return i;
            throw new Exception("The trainee did not learn on this vehicle kind");
        }
        public bool numLessonsValid(string id, int index)
        {
            Trainee trainee = dal.searchTrainee(id);

            if (trainee.Drivinglesson[index].Lessons >= Configuration.minLessons)
                return true;
            return false;
        }
        public bool isPassAlready(string id, int index)
        {
            Trainee trainee = dal.searchTrainee(id);
            return trainee.Drivinglesson[index].IsPass;
        }

        public Trainee searchTrainee(String id)
        {
           return dal.searchTrainee(id);
        }
        public Tester searchTester(String id)
        {
            return dal.searchTester(id);
        }

        public bool AddTest(Test test ,List<Tester> validTesters)
        {

            if (dal.searchTrainee(test.Id_trainee) == null)
                throw new Exception("Trainee was not found in the system");
            int index = checkTheKind(test.Id_trainee, test);
            if (!numLessonsValid(test.Id_trainee, index))
                throw new Exception("Trainee's number of lessons is less than the minimum");
            if (isPassAlready(test.Id_trainee, index))
                throw new Exception("The trainee has already licence on this kind of vehicle");
            if(Validation.IsDateValid(test.TestDate))
                throw new Exception("Date is not valid !");
            if (!isRangeTestValid(test))
                throw new Exception("Seven days have not passed from the last test yet");
            if (!isTraineeAvailable(test.Id_trainee, test.TestDate))
                throw new Exception("HONEY CHECK YOUR SCHEDULE !");

            test.Id_tester = FindTester(test.TestDate, test.TestHour, test.VehicleKind,validTesters); //  חיפוש מורה פנוי ועדכון שמו בטסט 
            if (test.Id_tester == null) //אם לא מצאנו טסטר פנוי
                return false;
          //  Tester t =dal.searchTester(test.Id_tester);//הוספת הטסט לרשימת הטסטים של המורה
            test.Code = Configuration.testCode;
            // t.TestsOfTester.Add(test);
            addTestToTester(test.Id_tester,test);
            dal.AddTest(test);
            //Configuration.testCode++;

            return true;
        }
        public void addTestToTester(string id, Test t)
        {
            IEnumerable<Tester> testerList = dal.GetAllTesters();
            foreach (Tester tester in testerList)
            {
                if (tester.Id == id)
                {
                    tester.TestsOfTester.Add(t);
                    dal.UpdateTester(tester);
                    return;
                }
            }
        }


       
        public List<Tester> GetAvaileableTesters(DateTime date, DateTime hour, List<Tester> testerslist)
        {
            //List<Tester> testerslist = dal.GetAllTesters();
          //  IEnumerable<Tester> testerslist = dal.GetAllTesters();
            List<Tester> availableTesters = new List<Tester>();

            foreach (Tester t in testerslist)
            {
                if (isAvailable(date, hour, t))
                {
                    availableTesters.Add(t);
                }
            }
            return availableTesters;
        }
        public bool isAvailable(DateTime date, DateTime hour, Tester t)
        {
            if (t.MaxWeeklyTests <= calculateWeeklyTests(t, date))
                return false;
            foreach (Test test in t.TestsOfTester)
            {
                if ((test.TestDate == date) && (test.TestHour == hour))
                    return false;
            }
            if (!(t.ScheduleTester[date.DayOfWeek.GetHashCode(), hour.Hour - 9]))
                return false;

            return true;
        }
        public string FindTester(DateTime date, DateTime hour, VehicleKind vehicleKind,List<Tester> validTesters)
        {
            List<Tester> availableTesters;
            availableTesters = GetAvaileableTesters(date, hour, validTesters);
            foreach (Tester t in availableTesters)
                if ((t.VehicleKind == vehicleKind))
                    return t.Id;
            return null;


        }


        public int calculateWeeklyTests(Tester t, DateTime date)//לא עבר מקסימום שבועי
        {
            int dayOftest = date.DayOfWeek.GetHashCode();
            int sum = 0;
            foreach (Test test in t.TestsOfTester)
            {
                if ((date.AddDays(4- dayOftest) >= test.TestDate) && (date.AddDays(-(dayOftest)) <= test.TestDate))
                    sum++;
            }
            return sum;
        }


        public bool UpdateTest(Test t)
        {
            int counter = 0;
            if (t.TesterNote == null)
                throw new Exception("יש לכתוב הערות");
            if (!t.DrivingCheck.keepDistance)
                counter++;
            if (!t.DrivingCheck.mirrorsWatcing)
                counter++;
            if (!t.DrivingCheck.reverseParking)
                counter++;
            if (!t.DrivingCheck.signs)
                counter++;
            if (!t.DrivingCheck.isAllowedSpeed)
                counter++;
            if ((counter > 2) && (t.IsPass))
                throw new Exception("לא יתכן שהתלמיד עבר");
            dal.UpdateTest(t);
            return true;

        }
        /*public double calculateDistance(Address address, Tester tester);
         public List<Tester> AllTesteresInDistanceX(double x, Address address)
         {
             List<Tester> myList = new List<Tester>();
             List<Tester> allTesters = dal.GetAllTesters();
             foreach(Tester t in allTesters)
             {
                 if (calculateDistance(address,t)<=x)
                     myList.Add(t);
             }
             return myList;
         }*/
        #endregion

        public int numOfTraineeTestsByKind(Trainee trainee, VehicleKind kind)
        {
            int counter = 0;
            IEnumerable<Test> traineeTests = GetAllTestsOfTrainee(trainee.Id);
            if (traineeTests == null)
                return 0;
            foreach (Test t in traineeTests)
                if (t.VehicleKind == kind)
                    counter++;
            return counter;

        }
        public int whichTestTraineePassedByKind(Trainee trainee, VehicleKind kind)
        {
            int counter = 0;
            IEnumerable<Test> traineeTests = GetAllTestsOfTrainee(trainee.Id);
            if (traineeTests == null)
                return 0;
            foreach (Test t in traineeTests)
            {
                if ((t.VehicleKind == kind) && (!t.IsPass))
                    counter++;
            }
            return counter;

        }

        public bool isDeservedLicenseByKind(Trainee trainee, VehicleKind kind)
        {
            IEnumerable<Test> traineeTests = GetAllTestsOfTrainee(trainee.Id);
            if (traineeTests == null)
                return false;
            foreach (Test t in traineeTests)
            {
                if (t.IsPass && t.VehicleKind == kind)
                    return true;
            }
            return false;

        }


        public bool isMatchGender(Trainee trainee, Tester tester)
        {
            if (trainee.Gender != tester.Gender)
                return false;
            return true;
        }
        public void UpDateScheduleTester(Tester t, int day, int hour, bool status)
        {
            t.ScheduleTester[day - 1, hour - 9] = status;
        }

        public IEnumerable<Test> getTestsOf3LastMonths(Tester t)
        {
            List<Test> in3month = new List<Test>();
            foreach (Test test in t.TestsOfTester)
            {
                if ((DateTime.Now.AddMonths(3) >= test.TestDate) && (DateTime.Now <= test.TestDate))
                    in3month.Add(test);
            }
            return in3month;
        }

        public string theBestTester()
        {
            int counter = 0;
            int max = 0;
            string maxID = "אין בוחן שעברו אצלו";
            IEnumerable<Tester> testers = dal.GetAllTesters();
            foreach (Tester t in testers)
            {
                foreach (Test test in t.TestsOfTester)
                {
                    if (test.IsPass)
                    {
                        counter++;
                    }
                }
                if (counter > max)
                {
                    max = counter;
                    maxID = t.Id;
                }
                counter = 0;
            }
            return maxID;
        }


        public IEnumerable<IGrouping<VehicleKind, Tester>> specializationGroup(bool b = false)
        {
            var groups = dal.GetAllTesters().GroupBy(ts => ts.VehicleKind);
            return groups;
        }

        public IEnumerable<IGrouping<string, Trainee>> twoWheeledVehicleschoolGroup(bool b = false)
        {
            var groups = dal.GetAllTrainees().GroupBy(ts => ts.Drivinglesson[0].DrivingSchool);
            return groups;
        }
        public IEnumerable<IGrouping<string, Trainee>> privateCarschoolGroup(bool b = false)
        {
            var groups = dal.GetAllTrainees().GroupBy(ts => ts.Drivinglesson[1].DrivingSchool);
            return groups;
        }
        public IEnumerable<IGrouping<string, Trainee>> heavyTruckschoolGroup(bool b = false)
        {
            var groups = dal.GetAllTrainees().GroupBy(ts => ts.Drivinglesson[2].DrivingSchool);
            return groups;
        }
        public IEnumerable<IGrouping<string, Trainee>> mediumTruckschoolGroup(bool b = false)
        {
            var groups = dal.GetAllTrainees().GroupBy(ts => ts.Drivinglesson[3].DrivingSchool);
            return groups;
        }

        public IEnumerable<IGrouping<string, Trainee>> twoWheeledVehicleDrivingTeacherGroup(bool b = false)
        {
            var groups = dal.GetAllTrainees().GroupBy(tt => tt.Drivinglesson[0].Teacher);
            return groups;
        }
        public IEnumerable<IGrouping<string, Trainee>> privateCarDrivingTeacherGroup(bool b = false)
        {

            var groups = dal.GetAllTrainees().GroupBy(tt => tt.Drivinglesson[1].Teacher);
            return groups;
        }
        public IEnumerable<IGrouping<string, Trainee>> heavyTruckdrivingTeacherGroup(bool b = false)
        {

            var groups = dal.GetAllTrainees().GroupBy(tt => tt.Drivinglesson[2].Teacher);
            return groups;
        }
        public IEnumerable<IGrouping<string, Trainee>> mediumTruckdrivingTeacherGroup(bool b = false)
        {
            var groups = dal.GetAllTrainees().GroupBy(tt => tt.Drivinglesson[3].Teacher);
            return groups;
        }

        public IEnumerable<IGrouping<int, Trainee>> numberOfTestGroup(bool b = false)
        {
            var groups = dal.GetAllTrainees().GroupBy(tt => numberOfTest(tt));
            return groups;
        }
        public int numberOfTest(Trainee tt)
        {
            IEnumerable<Test> MyList =GetAllTestsOfTrainee(tt.Id);
            int counter = 0;
            foreach (Test t in MyList)
            {
                counter++;
            }
            return counter;
        }


        public delegate bool condition(Test test);
        public List<Test> AlltestsThat(condition Mycondition)
        {
            List<Test> myList = new List<Test>();
            IEnumerable<Test> allTest = dal.GetAllTests();
            foreach (Test t in allTest)
            {
                if (Mycondition(t))
                    myList.Add(t);
            }
            return myList;

        }

        // The function gets list of tests by month
        public IEnumerable<Test> GetTestsByMonth(int month, int year)
        {
			IEnumerable<Test> listTest = dal.GetAllTests();
            var thisMonth = from t in listTest
                            where t.TestDate.Month == month && t.TestDate.Year == year
                            select t;
            return thisMonth;
        }
        public bool isPrefix(string pre,string id)
        {
            for(int i=0;i<pre.Length;i++)
            {
                if (pre[i] != id[i])
                    return false;
            }
            return true;
        }
        public IEnumerable<Trainee> GetTraineeByPrefixId(string preId)
        {
            IEnumerable<Trainee> mytrainee = new List<Trainee>();
            mytrainee= GetAllTrainees();
            var thisPrefix = from t in mytrainee
                             where isPrefix(preId,t.Id)
                            select t;
            return thisPrefix;
        }

        public IEnumerable<Tester> GetTesterByPrefixId(string preId)
        {
            IEnumerable<Tester> mytester = new List<Tester>();
            mytester = GetAllTesters();
            var thisPrefix = from t in mytester
                             where isPrefix(preId, t.Id)
                             select t;
            return thisPrefix;
        }
        public IEnumerable<Test> GetTestByPrefixId(string preCode)
        {
            IEnumerable<Test> mytest = new List<Test>();
            mytest = GetAllTests();
            var thisPrefix = from t in mytest
                             where isPrefix(preCode, t.Code.ToString())
                             select t;
            return thisPrefix;
        }

        public VehicleKind[] TestedAndPassed(Trainee trainee)
        {
            List<Test> tests = AlltestsThat(t => (t.IsPass && t.Id_trainee == trainee.Id));
            VehicleKind[] arr = new VehicleKind[4];
            foreach (Test t in tests)
            {
                arr[t.VehicleKind.GetHashCode()] = t.VehicleKind;
            }
            return arr;
        }

        public DateTime SuggestAnotherDateForTest(DateTime date, DateTime hour, VehicleKind vehicleKind, List<Tester> validTesters)
        {
            if (validTesters.Count == 0)
                throw new Exception("מקום היציאה רחוק מידי לבוחנים שלנו");
            DateTime starthour = new DateTime(1900, 01, 01, 9, 0, 0);
            string testerId = null;
            while (testerId == null)
            {
                hour = hour.AddHours(1);
                if (hour.Hour <= 15)
                {
                    testerId = FindTester(date, hour, vehicleKind, validTesters);
                }
                else
                {
                    date = date.AddDays(1);
                    while(date.DayOfWeek.GetHashCode()>=5)
                        date = date.AddDays(1);
                    hour = starthour;
                    testerId = FindTester(date, hour, vehicleKind,validTesters);
                }
            }
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, hour.Hour, 0, 0);
            return newDate;
        }


        public List<Test> getAllTestbySomeKind(Predicate<Test> predicate)
        {
            
            IEnumerable<Test> listTest = dal.GetAllTests();
            List<Test> mylist;
            mylist = listTest.ToList().FindAll(predicate);
            return mylist;
        }

        public bool TestByA(Test t)
        {
            return (t.VehicleKind == VehicleKind.A);
        }



        public void PrintTestsbyTrainee(string id)
        {
            IEnumerable<Test> listTest = dal.GetAllTests();
            var TheTest = from t in listTest
                          select new { Code = t.Code, Id = t.Id_tester };
            foreach (var t in TheTest)
                Console.WriteLine("The Code: {0,-10}, ID Tester: {1}", t.Code, t.Id);

        }
        public bool IsTheSame(Object ob1,Object ob2)
        {
            return ob1.ToString().Equals(ob2.ToString());
        }

        public IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicat = null)
        {
            return dal.GetAllTrainees(predicat);
        }

        public IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicat = null)
        {
            return dal.GetAllTesters(predicat);
        }

        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicat = null)
        {
            return dal.GetAllTests(predicat);
        }
        public IEnumerable<Test> GetAllTestsOfTrainee(string id)
        {
            List<Test> myList = new List<Test>();
            IEnumerable < Test > allTests= dal.GetAllTests();
            foreach (Test t in allTests)
                if (t.Id_trainee == id)
                    myList.Add(t);
            return myList;
        }
        public IEnumerable<Test> GetAllTestsOfTester(string id)
        {
            List<Test> myList = new List<Test>();
            Tester t = dal.searchTester(id);
            return t.TestsOfTester;
        }


        public void veteranTesters()
        {
            IEnumerable<Tester> testerList = dal.GetAllTesters();
            var t = from item in testerList
                    let veteran = item.ExperienceYears
                    where veteran > 10
                    select item;
            foreach (var item in t)
                Console.WriteLine("name: {0,-10} id: {1,-5} ExperienceYears:{2,-10}", item.FirstName, item.Id, item.ExperienceYears);

        }
        public IEnumerable<Trainee> searchTraineeLname(string Lname)
        {
            IEnumerable<Trainee> mytrainee = new List<Trainee>();
            mytrainee = GetAllTrainees();
            var thisLname = from t in mytrainee
                            where (t.LastName == Lname)
                            select t;
            return thisLname;
        }
        public IEnumerable<Trainee> searchTraineeFname(string Fname)
        {
            IEnumerable<Trainee> mytrainee = new List<Trainee>();
            mytrainee = GetAllTrainees();
            var thisFname = from t in mytrainee
                            where (t.FirstName == Fname)
                            select t;
            return thisFname;
        }
        public IEnumerable<Tester> searchTesterLname(string Lname)
        {
            IEnumerable<Tester> mytester = new List<Tester>();
            mytester = GetAllTesters();
            var thisLname = from t in mytester
                            where (t.LastName == Lname)
                            select t;
            return thisLname;
        }
        public IEnumerable<Tester> searchTesterFname(string Fname)
        {
            IEnumerable<Tester> mytester = new List<Tester>();
            mytester = GetAllTesters();
            var thisFname = from t in mytester
                            where (t.FirstName == Fname)
                            select t;
            return thisFname;
        }


    }
}
