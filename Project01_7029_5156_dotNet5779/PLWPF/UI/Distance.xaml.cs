
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Xml;
using System.Threading;
using BE;
namespace PLWPF
{
    public partial class Distance : Window
    {
        String API_KEY = @"s6JPBRnNQ8Gg1sG791Mt775kHAnqARmT";
        BackgroundWorker backgroundworker;
        public List<Tester> list;
        public Tester tester;
        public Distance(Tester t, Address addressExit, Address addressLiving)
        {
            tester = t;
            InitializeComponent();
            backgroundworker = new BackgroundWorker();
            backgroundworker.DoWork += Backgroundworker_DoWork;
            backgroundworker.RunWorkerCompleted += Backgroundworker_RunWorkerCompleted;
            backgroundworker.RunWorkerAsync(new List<BE.Address> { addressExit, addressLiving });
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
            System.Windows.Data.CollectionViewSource addressViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("addressViewSource")));
        }

        private void Backgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            double result = (double)e.Result;
            this.EizeYofi.Text = result.ToString();
            if (tester.MaxDistance >= result)
                list.Add(tester);
            Close();

        }

        private void Backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            double result;

            List<BE.Address> addr = e.Argument as List<BE.Address>;


            string origin = addr[0].street + " " + addr[0].number + " st. " + addr[0].city;
            string destination = addr[1].street + " " + addr[1].number + " st. " + addr[1].city;
            string KEY = API_KEY;

            string url = @"https://www.mapquestapi.com/directions/v2/route" +
            @"?key=" + KEY +
            @"&from=" + origin +
            @"&to=" + destination +
            @"&outFormat=xml" +
            @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
            @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);

                result = (distInMiles * 1.609344);
                e.Result = result;
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                // Console.WriteLine("an error occurred, one of the addresses is not found. try again.");
                MessageBox.Show("an error occurred, one of the addresses is not found. try again.");
                this.Close();
            }
            else //busy network or other error...
            {
                // Console.WriteLine("We have'nt got an answer, maybe the net is busy...");
                MessageBox.Show("We have'nt got an answer, maybe the net is busy...");
                this.Close();
            }
        }
    }
}
