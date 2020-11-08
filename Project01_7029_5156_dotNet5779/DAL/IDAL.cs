using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;


namespace DAL
{
    public class FactoryDal

    {

        public static IDAL GetDal()
        {
            return new DAL.Dal_XML_IMP();
        }
    }

    public interface IDAL 
    {
        #region Actions_Tester
        void AddTester(Tester tester);
        bool RemoveTester(string id);
        void UpdateTester(Tester tester);
        IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicate = null);
        #endregion

        #region Actions_Trainee
        void AddTrainee(Trainee trainee);
        bool RemoveTrainee(string id);
        void UpdateTrainee(Trainee trainee);
        IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null);
        #endregion

        #region Actions_Test
        void AddTest(Test test);
        void UpdateTest(Test test);
        bool RemoveTest(long code);
        IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null);
        #endregion

        Tester searchTester(string id);
        Trainee searchTrainee(string id);

    }
}
