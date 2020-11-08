using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DS
{
    public class DataSource
    {
        public static List<Tester> testerList=new List<Tester>();
        public static List<Trainee> traineeList=new List<Trainee>();
        public static List<Test> testList = new List<Test>();

        public DataSource()
        {
            testerList = new List<Tester>();
            traineeList = new List<Trainee>();
            testList = new List<Test>();
        }
    }
     
}
