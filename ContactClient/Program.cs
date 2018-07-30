using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactLibrary;
using Newtonsoft.Json;

namespace ContactClient
{
    class Program
    {

        static void Main(string[] args)
        {
            string [] testValues = {"Eric", "Li", "1329", "76th street","Brooklyn", "NY", "11228","USA","1","718", "331", "0868" };
            Directory addressBook = new Directory();
            Person addition = new Person();
            Console.WriteLine("Please enter the following bits of information.");
            Console.WriteLine("family name:");
            addition.lastName = testValues[1];
            Console.WriteLine("given name:");
            addition.firstName = testValues[0];
            Console.WriteLine("house number:");
            addition.address.houseNum = testValues[2];
            Console.WriteLine("street:");
            addition.address.street = testValues[3];
            Console.WriteLine("city:");
            addition.address.city = testValues[4];
            Console.WriteLine("state:");
            string state = testValues[5];
            addition.address.State = (State)Enum.Parse(typeof(State), state);
            Console.WriteLine("country:");
            addition.address.Country = (Country)Enum.Parse(typeof(Country), testValues[6]);
            Console.WriteLine("zipcode:");
            addition.address.zipcode = testValues[7];
            Console.WriteLine("country code:");
            int countryCode = Int32.Parse(testValues[8]);
            addition.phone.countrycode = (Country)countryCode;
            Console.WriteLine("area code:");
            addition.phone.areaCode = testValues[9];
            Console.WriteLine("number:");
            addition.phone.number = testValues[10];
            Console.WriteLine("extension:");
            addition.phone.ext = testValues[11];
            string yellowPages = JsonConvert.SerializeObject(addressBook);

            addressBook.Add(addition);
            Console.WriteLine(addressBook);
            Console.ReadLine();
        }
    }
}
