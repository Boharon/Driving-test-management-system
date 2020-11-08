using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{

    public interface IBL
    {
 

        #region Actions_Tester
        AddStatus AddTester(Tester tester);
        bool RemoveTester(string id);
        bool UpdateTester(Tester tester);

        #endregion

        #region Actions_Trainee
        AddStatus AddTrainee(Trainee trainee);
        bool RemoveTrainee(string id);
        AddStatus UpdateTrainee(Trainee trainee);

        #endregion

        #region Actions_Test
        bool AddTest(Test test, List<Tester> validTesters);
        bool RemoveTest(long code, DateTime date);
        bool UpdateTest(Test test);

		#endregion



		// The function checks if there is at least 7 days between tests
		bool isRangeTestValid(Test t);

        // The function returns true if trainee is available and false otherwise
        bool isTraineeAvailable(string id, DateTime d);

        //check if The trainee did learn on this vehicle kind and return the index on arry
        int checkTheKind(string id, Test test);

        // The function returns true if trainee's number of lessons is valid and false otherwise
        bool numLessonsValid(string id, int index);

        // The function returns true if trainee already has licence on a specific vehicle and false otherwise
        bool isPassAlready(string id, int index);

        // The function returns the id of an available tester by his specialezition
        string FindTester(DateTime date, DateTime hour, VehicleKind vehicleKind , List<Tester> validTesters);

        // The function returns list of all the available testers 
        List<Tester> GetAvaileableTesters(DateTime date, DateTime hour, List<Tester> validTesters);

        // The function calculates the amount of tests per a week
        int calculateWeeklyTests(Tester t, DateTime date);

        // The function returns true if tester is available and false otherwise
        bool isAvailable(DateTime date, DateTime hour, Tester t);

        // The function counts the number of tests that trainee had by a specific kind
        int numOfTraineeTestsByKind(Trainee trainee, VehicleKind kind);

        // The function returns true if trainee is eligible for license by the vehicle kind
        bool isDeservedLicenseByKind(Trainee trainee, VehicleKind kind);

        // The function returns the series number of the test that trainee passed and got license
        int whichTestTraineePassedByKind(Trainee trainee, VehicleKind kind);

        // The function returns if the trainee's gender and the tester's gender are the same in order to enable to trainee possibility of choosing
        bool isMatchGender(Trainee trainee, Tester tester);

        // The function updates shifts schedule of the tester
        void UpDateScheduleTester(Tester t, int day, int hour, bool status);

        // The function returns a list of the tests that occured in the last 3 months
        IEnumerable<Test> getTestsOf3LastMonths(Tester t);

        // The function returns the id of the tester that most of trainees passed their test at
        string theBestTester();

        // The function returns the number of all tests that trainee had 
        int numberOfTest(Trainee tt);

    
        // The function returns array of vehicle kinds that trainee has license on.
        VehicleKind[] TestedAndPassed(Trainee trainee);

        // The function suggests other date & hour for test in case that the wanted date was not available
        DateTime SuggestAnotherDateForTest(DateTime date, DateTime hour, VehicleKind vehicleKind, List<Tester> validTesters);

		// The function adds a test to the tests list of the tester
		void addTestToTester(string id, Test t); 

		// The function returns list of tests by month
		IEnumerable<Test> GetTestsByMonth(int month, int year);

		#region Grouping
		IEnumerable<IGrouping<VehicleKind, Tester>> specializationGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> twoWheeledVehicleschoolGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> privateCarschoolGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> heavyTruckschoolGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> mediumTruckschoolGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> twoWheeledVehicleDrivingTeacherGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> privateCarDrivingTeacherGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> heavyTruckdrivingTeacherGroup(bool b = false);
        IEnumerable<IGrouping<string, Trainee>> mediumTruckdrivingTeacherGroup(bool b = false);
        IEnumerable<IGrouping<int, Trainee>> numberOfTestGroup(bool b = false);
		#endregion

		void PrintTestsbyTrainee(string id);
        List<Test> getAllTestbySomeKind(Predicate<Test> predicate);
        bool TestByA(Test t);
  

        bool IsTheSame(Object ob1, Object ob2);
       

        Tester searchTester(String id);

		IEnumerable<Trainee> GetTraineeByPrefixId(string preId);
        IEnumerable<Tester> GetTesterByPrefixId(string preId);
        IEnumerable<Test> GetTestByPrefixId(string preId);

        IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicat = null);
        IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicat = null);
        IEnumerable<Test> GetAllTests(Func<Test, bool> predicat = null);

        IEnumerable<Test> GetAllTestsOfTrainee(string id);
        IEnumerable<Test> GetAllTestsOfTester(string id);

        //Trainee searchTrainee(String id);

        //The function return all trainees that Lname is their last name
        IEnumerable<Trainee> searchTraineeLname(string Lname);
        //The function return all trainees that Fname is their First name
        IEnumerable<Trainee> searchTraineeFname(string Fname);
        //The function return all testers that Lname is their last name
        IEnumerable<Tester> searchTesterLname(string Lname);
        //The function return all testers that Fname is their First name
        IEnumerable<Tester> searchTesterFname(string Fname);
    }
}
