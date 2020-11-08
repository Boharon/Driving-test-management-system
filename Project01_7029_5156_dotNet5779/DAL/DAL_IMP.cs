using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
namespace DAL
{

    
    public class DAL_IMP: IDAL
    {


        #region TesterAction
        public void AddTester(Tester tester)
        {
            Tester myTester = GetTester(tester.Id);
            if (myTester != null)
                throw new Exception("Tester with the same id already exists");
            DataSource.testerList.Add(tester);
        }
        public bool RemoveTester(string id)
        {
            Tester myTester = GetTester(id);
            if (myTester == null)
                throw new Exception("Tester with the same id was not found");
            if (GetAllTests(tt => (tt.Id_tester == id) && (tt.TestDate >= DateTime.Now)).Any())
                throw new Exception("Tester has future tests");
            return DataSource.testerList.Remove(myTester);
        }

        public void UpdateTester(Tester tester)
        {
            int index = DataSource.testerList.FindIndex(t => t.Id == tester.Id);
            if(index==-1)
                throw new Exception("Tester with the same id was not found");
            DataSource.testerList[index] = tester;

        }

        public Tester GetTester(string id)
        {
            return DataSource.testerList.FirstOrDefault(t => t.Id == id);
        }

        //public List<Tester> GetAllTesters()
        //{
        //    return DataSource.testerList;
        //}

        //public List<Test> GetAllTests()
        //{
        //    return DataSource.testList;
        //}

        //public List<Trainee> GetAllTrainees()
        //{
        //    List<Trainee> mylist = new List<Trainee>();
        //    mylist = DataSource.traineeList;
        //    return mylist;
        //}


        public IEnumerable<Tester> GetAllTesters(Func<Tester,bool> predicate =null)
        {
            if (predicate == null)
                return DataSource.testerList.AsEnumerable();
            return DataSource.testerList.Where(predicate);
        }
        #endregion

        #region TraineeAction
        public void AddTrainee(Trainee trainee)
        {
            //Trainee myTrainee = GetTrainee(trainee.Id);
            //if (myTrainee != null)
            //    throw new Exception("Trainee with the same id already exists");
            DataSource.traineeList.Add(trainee);
        }
        public bool RemoveTrainee(string id)
        {
            Trainee myTrainee = GetTrainee(id);
            if (myTrainee == null)
                throw new Exception("Trainee with the same id was not found");
            if (GetAllTests(tt => (tt.Id_trainee == id) && (tt.TestDate >= DateTime.Now)).Any())
                throw new Exception("Trainee has future tests");
            return DataSource.traineeList.Remove(myTrainee);
        }

        public void UpdateTrainee(Trainee trainee)
        {
            int index = DataSource.traineeList.FindIndex(t => t.Id == trainee.Id);
            if (index == -1)
                throw new Exception("Trainee with the same id was not found");
            DataSource.traineeList[index] = trainee;

        }

        public Trainee GetTrainee(string id)
        {
            return DataSource.traineeList.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.traineeList.AsEnumerable();
            return DataSource.traineeList.Where(predicate);
        }
        #endregion

        #region TestAction
        public void AddTest(Test test)
        {
            Test myTest = GetTest(test.Code);
            if (myTest != null)
                throw new Exception("Test with the same code already exists");
            DataSource.testList.Add(test);
        }

        public void UpdateTest(Test test)
        {
            int index = DataSource.testList.FindIndex(t => t.Code == test.Code);
            if (index == -1)
                throw new Exception("Test with the same code was not found");
            DataSource.testList[index] = test;

        }

        public bool RemoveTest(long code)
        {
            foreach (Test t in DataSource.testList)
            {
                if (t.Code == code)
                    DataSource.testList.Remove(t);
            }
            return true;
        }

        public Test GetTest(long code)
        {
            return DataSource.testList.FirstOrDefault(t => t.Code == code);
        }

        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.testList.AsEnumerable();
            return DataSource.testList.Where(predicate);
        }
        #endregion


        public Tester searchTester(string id)
        {
            foreach (Tester tester in DataSource.testerList)
                if (tester.Id == id)
                    return tester;
            return null;
        }
        public Trainee searchTrainee(string id)
        {
            foreach (Trainee trainee in DataSource.traineeList)
                if (trainee.Id == id)
                    return trainee;
            return null;
        }



        public List<Test> GetAllTestsOfTesters(string id)
        {
            List<Test> myList = new List<Test>();
            foreach (Test t in DataSource.testList)
                if (t.Id_tester == id)
                    myList.Add(t);
            return myList;
        }

        public bool removeTestInTesterList(string id,long code)
        {
            foreach(Tester tester in DataSource.testerList)
            {
                if(tester.Id==id)
                {
                    foreach (Test test in tester.TestsOfTester)
                        if (test.Code == code)
                        {
                            tester.TestsOfTester.Remove(test);
                            return true;
                        }
                }

            }
            return false;
        }
    }
}
