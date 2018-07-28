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
        public void Delete(long pid)
        {
            List<Person> deleteList = new List<Person>();
            string searchCriteria;
            searchCriteria = Console.ReadLine();
            switch (searchCriteria.ToLower())
            {
                case "first name":
                    {
                        break;
                    }
                case "last name":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Search Criteria");
                        break;
                    }
            }
            foreach (Person element in deleteList)
            {
                directory.Remove(element);
            }
        }
        public void Update(long pid)
        {
            Person updatePerson = directory.Find(x => x.Pid == pid);

        }
        public List<Person> Search(long pid = 0, string firstName = "", string lastName = "", string zipCode = "", string city = "", string phoneNumber = "")
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
            else if(pid == 0)
            {
                return directory.FindAll(x => x.Pid == pid);
            }
            else
            {
                Console.WriteLine("No search criteria had been given");
                return null;
            }
        }
        
    }
}
