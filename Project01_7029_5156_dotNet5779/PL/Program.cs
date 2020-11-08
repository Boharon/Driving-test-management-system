using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                
                Test test1 = new Test
                {
                    Id_trainee = "123456789",
                    TestDate = new DateTime(2018, 2, 1),
                    TestHour = new DateTime(0, 0, 0, 5, 0, 0),
                    addressExit = new Address("שורש ", 14, "בית שמש"),

                    VehicleKind = VehicleKind.A,
                    GearboxKind = GearboxKind.automatic

                };
        
                
            }
            catch(Exception e)
            {
                Console.Write(e);
            }

        }
    }
}
