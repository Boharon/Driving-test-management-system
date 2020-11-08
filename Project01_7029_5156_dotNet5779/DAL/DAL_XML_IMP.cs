using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using BE;
using DS;

namespace DAL
{
    class Dal_XML_IMP : IDAL
    {
        XElement traineeRoot;
        string pathTrainee = @"traineeXml.xml";
        string pathTester = @"testerXml.xml";
        string pathTest = @"testXml.xml";

        public List<Tester> testerList;
        public List<Test> testList;


        public Dal_XML_IMP()
        {
            XElement help = XElement.Load("..//..//..//XML_Files//codeXml.xml");
            Configuration.testCode = int.Parse(help.Element("code").Value);


            if (!File.Exists(pathTrainee))
            {
                List<Trainee> traineeList = new List<Trainee>();//empty list for start
                SaveToXML<List<Trainee>>(traineeList, pathTrainee);
            }

            if (!File.Exists(pathTester))
            {
                List<Tester> testerList = new List<Tester>();//empty list for start
                SaveToXML<List<Tester>>(testerList, pathTester);
            }

            if (!File.Exists(pathTest))
            {
                List<Test> testList = new List<Test>();//empty list for start
                SaveToXML<List<Test>>(testList, pathTest);
            }

            if (!File.Exists(pathTrainee))//we have to add new file
                CreateFileTrainee();
            else//ensure all data is according to current information
                LoadDataTrainee();
        }

        private void LoadDataTrainee()//load from file to program
        {
            try
            {
                traineeRoot = XElement.Load(pathTrainee);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        private void CreateFileTrainee()//for new file
        {
            traineeRoot = new XElement("trainees");
            traineeRoot.Save(pathTrainee);//add new main element
        }

        public static void SaveToXML<T>(T source, string path)//save objects like elements from program to  file
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)//save elements like objects from file to program 
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }


        #region trainee

        #region trainee
        public void AddTrainee(Trainee trainee)
        {

            LoadDataTrainee();

            Trainee ch = searchTrainee(trainee.Id);//check if this id already exits
            if (ch != null)//it means there is already such a child
                throw new Exception("The trainee is already exists in system");

            XElement id = new XElement("id", trainee.Id);
            XElement firstName = new XElement("firstName", trainee.FirstName);
            XElement lastName = new XElement("lastName", trainee.LastName);
            XElement birthDate = new XElement("birthDate", trainee.BirthDate);
            XElement gender = new XElement("gender", trainee.TempGender);
            XElement phoneNumber = new XElement("phoneNumber", trainee.PhoneNumber);
            XElement addressTrainee = new XElement("addressTrainee", trainee.TempAddress);
            XElement drivinglessons = new XElement("drivinglessons", trainee.TempDrivinglessons);


            XElement complete = new XElement("trainee", id, firstName, lastName, birthDate, gender, phoneNumber, addressTrainee, drivinglessons);

            traineeRoot.Add(complete);//add new child to all children in file

            traineeRoot.Save(pathTrainee);

        }


        public Trainee searchTrainee(string id)
        {
            LoadDataTrainee();
            Trainee returnCh;
            returnCh = (from chElement in traineeRoot.Elements()
                        where (chElement.Element("id").Value) == id
                        select new Trainee()//convert from element in file to object of child
                        {

                            Id = chElement.Element("id").Value,
                            FirstName = chElement.Element("firstName").Value,
                            LastName = chElement.Element("lastName").Value,
                            BirthDate = DateTime.Parse(chElement.Element("birthDate").Value),
                            TempGender = (chElement.Element("gender").Value),
                            PhoneNumber = (chElement.Element("phoneNumber").Value),
                            TempAddress =(chElement.Element("addressTrainee").Value),
                            TempDrivinglessons =(chElement.Element("drivinglessons").Value),
                        }).FirstOrDefault();

            return returnCh;
        }

        public IEnumerable<Trainee> GetAllTrainees(Func<Trainee, bool> predicate = null)
        {
            LoadDataTrainee();
            //collect elments of children to ienumerable
            IEnumerable<Trainee> allTrainee = from chElement in traineeRoot.Elements()
                                              select new Trainee()//convert from element in file to object of child
                                              {
                                                  Id = chElement.Element("id").Value,
                                                  FirstName = chElement.Element("firstName").Value,
                                                  LastName = chElement.Element("lastName").Value,
                                                  BirthDate = DateTime.Parse(chElement.Element("birthDate").Value),
                                                  TempGender = (chElement.Element("gender").Value),
                                                  PhoneNumber = (chElement.Element("phoneNumber").Value),
                                                  TempAddress = (chElement.Element("addressTrainee").Value),
                                                  TempDrivinglessons = (chElement.Element("drivinglessons").Value),

                                              };

            if (predicate == null)//no condition
                return allTrainee;
            else
                return allTrainee.Where(predicate);
        }

        public bool RemoveTrainee(string id)
        {
            LoadDataTrainee();
            XElement removeTraineeElement;
            //find wanted to be deleted
            removeTraineeElement = (from chElement in traineeRoot.Elements()
                                    where (chElement.Element("id").Value) == id
                                    select chElement).FirstOrDefault();
            if (removeTraineeElement == null)//cant remove cause didnt find
                throw new Exception("no such trainee");

            //delete contracts of child:
            List<Test> testList = GetAllTests().ToList();
            testList.RemoveAll(test => test.Id_trainee == id);
            SaveToXML<List<Test>>(testList, pathTest);//save contracts to file without those of children


            removeTraineeElement.Remove();//delete from root

            traineeRoot.Save(pathTrainee);//save to file
            return true;//stam
        }

        public void UpdateTrainee(Trainee trainee)
        {
            LoadDataTrainee();
            //find element of this child
            XElement updateChElement = (from chElement in traineeRoot.Elements()
                                        where (chElement.Element("id").Value) == trainee.Id
                                        select chElement).FirstOrDefault();

            if (updateChElement == null)//cant update child that doesnt exist
                throw new Exception("no such trainee");

            //put new values,it has reference to root
            updateChElement.Element("firstName").Value = trainee.FirstName;
            updateChElement.Element("lastName").Value = trainee.LastName;
            updateChElement.Element("birthDate").Value = trainee.BirthDate.ToString();
            updateChElement.Element("gender").Value = trainee.TempGender;
            updateChElement.Element("phoneNumber").Value = trainee.PhoneNumber;
            updateChElement.Element("addressTrainee").Value = trainee.TempAddress;
            updateChElement.Element("drivinglessons").Value = trainee.TempDrivinglessons;

            //save new to file
            traineeRoot.Save(pathTrainee);
        }


        #endregion



        #endregion

        #region test
        public void AddTest(Test test)
        {
            //test.Code = Configuration.testCode;
            Configuration.testCode++;//update num of contracts
            XElement help = XElement.Load("..//..//..//XML_Files//codeXml.xml");
            help.Element("code").Value = Configuration.testCode.ToString();
            help.Save("..//..//..//XML_Files//codeXml.xml");


            testList = LoadFromXML<List<Test>>(pathTest);//we are sure there is file cause created once in ctor
            testList.Add(test);//add
            SaveToXML<List<Test>>(testList, pathTest);//save in file
        }

        public IEnumerable<Test> GetAllTests(Func<Test, bool> predicate = null)
        {
            if (predicate == null)//if there isnt condition we return all list that was converted to enumerable from file
                return LoadFromXML<List<Test>>(pathTest).AsEnumerable();
            return LoadFromXML<List<Test>>(pathTest).Where(predicate);//return by condition
        }

        public Test SearchTest(long code)
        {
            testList = LoadFromXML<List<Test>>(pathTest);//we are sure there is file cause created once in ctor
            return testList.FirstOrDefault(t => t.Code == code);//find wanted con and return him

        }

        public bool RemoveTest(long code)
        {
            Test testToRemove = SearchTest(code);
            if (testToRemove == null)
                throw new Exception("no such test");

            testList = LoadFromXML<List<Test>>(pathTest);//we are sure there is file cause created once in ctor
            testList.RemoveAll(t => t.Code == code);//delete
            SaveToXML<List<Test>>(testList, pathTest);//save in file
            return true;
        }

        public void UpdateTest(Test test)
        {
            testList = LoadFromXML<List<Test>>(pathTest);//we are sure there is file cause created once in ctor
            int test_Index = testList.FindIndex(t => t.Code == test.Code);//return index of wanted contract in list
            if (test_Index == -1)//didnt find
                throw new Exception("no such test");//cant update if contract doesnt exist
            testList[test_Index] = test;//update contract in his place
            SaveToXML<List<Test>>(testList, pathTest);//save in file after changes
        }

        #endregion

        #region tester

        public void AddTester(Tester t)
        {
            Tester tester = searchTester(t.Id);//check if this id already exits
            if (tester != null)//it means there is already such a mother
                throw new Exception("The tester is already exists in system");

            testerList= LoadFromXML<List<Tester>>(pathTester);//we are sure there is file cause created once in ctor

            //List<Tester> testerList = LoadFromXML<List<Tester>>(pathTester);//we are sure there is file cause created once in ctor
            testerList.Add(t);
            SaveToXML<List<Tester>>(testerList, pathTester);//save in file

        }

        public IEnumerable<Tester> GetAllTesters(Func<Tester, bool> predicate = null)
        {
            if (predicate == null)//if there isnt condition we return all list that was converted to enumerable from file
                return LoadFromXML<List<Tester>>(pathTester).AsEnumerable();
            return LoadFromXML<List<Tester>>(pathTester).Where(predicate);//return by condition

        }

        public Tester searchTester(string id)
        {
            return LoadFromXML<List<Tester>>(pathTester).FirstOrDefault(tester => tester.Id == id);
        }

        public bool RemoveTester(string id)
        {
            Tester testerToRemove = searchTester(id);
            if (testerToRemove == null)//cant remove if doesnt exist
                throw new Exception("no such tester");

            testerList = LoadFromXML<List<Tester>>(pathTester);//we are sure there is file cause created once in ctor
            testerList.RemoveAll(t => t.Id == id);
            SaveToXML<List<Tester>>(testerList, pathTest);//save in file without this mom

            return true;//stam

        }

        public void UpdateTester(Tester tester)
        {
            testerList = LoadFromXML<List<Tester>>(pathTester);//we are sure there is file cause created once in ctor
            int tester_Index = testerList.FindIndex(t => t.Id == tester.Id);//return index of wanted mother in list
            if (tester_Index == -1)//didnt find
                throw new Exception("no such tester");//cant update if mother doesnt exist
            testerList[tester_Index] = tester;//update mother in her place 
            SaveToXML<List<Tester>>(testerList, pathTester);//save in file after changes
        }
        #endregion

        public Trainee searchTraineeFname(string Fname)
        {
            return LoadFromXML<List<Trainee>>(pathTrainee).FirstOrDefault(trainee => trainee.FirstName == Fname);
        }

        public Trainee searchTraineeLname(string Lname)
        {
            return LoadFromXML<List<Trainee>>(pathTrainee).FirstOrDefault(trainee => trainee.LastName == Lname);
        }

    }
}
