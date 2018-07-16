using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactLibrary
{
    class Directory
    {
        List<Person> directory;
        public Directory()
        {
            directory = new List<Person>();
        }
        public void Add(Person person)
        {
            directory.Add(person);
        }
        public void Update(long pid)
        {
            directory.Find(x => x.Pid == pid);
        }
        public List<Person> Search(string firstName = "", string lastName = "", string zipCode = "", string city = "", string phoneNumber = "")
        {
            if(firstName != "")
            {
                return directory.FindAll(x => x.firstName == firstName);
            }
            else if(lastName != "")
            {
                return directory.FindAll(x => x.lastName == lastName);
            }
            else if(zipCode != "")
            {
                return directory.FindAll(x => x.address.zipcode == zipCode);
            }
            else if(city != "")
            {
                return directory.FindAll(x => x.address.city == city);
            }
            else if(phoneNumber != "")
            {
                return directory.FindAll(x => x.phone.ToString() == phoneNumber);
            }
            else
            {
                Console.WriteLine("No search criteria had been given");
                return null;
            }
        }
        
    }
}
