using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactLibrary
{
    [SerializableAttribute]
    public class Directory
    {
        public List<Person> directory;
        public Directory()
        {
            directory = new List<Person>();
        }
        public override string ToString()
        {
            string data = "";
            foreach(Person element in directory)
            {
                data += element;
            }
            return data;
        }
        public void Add(Person person)
        {
            directory.Add(person);
        }
        public void Delete()
        {
            List<Person> deleteList = new List<Person>();
            string searchCriteria;
            Console.WriteLine("Please enter the method of search: \n" +
                "1. ID \n" +
                "2. First name \n" +
                "3. Last name \n");
            searchCriteria = Console.ReadLine().Trim();
            switch (searchCriteria.ToLower())
            {
                case "id":
                case "1":
                    {
                        Console.WriteLine("Please enter the ID of the contact you wish to delete");
                        deleteList = Search(pid: Console.Read());
                        break;
                    }
                case "first name":
                case "2":
                    {
                        Console.WriteLine("Please enter the first name of the contact(s) you wish to delete");
                        deleteList = Search(firstName: Console.ReadLine().Trim());
                        break;
                    }
                case "last name":
                case "3":
                    {
                        Console.WriteLine("Please enter the last name of the contact(s) you wish to delete");
                        deleteList = Search(lastName: Console.ReadLine().Trim());
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Search Method.");
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
            string changes;
            Console.WriteLine(updatePerson + "\n \n" +
                "What would you like to change? Please type in the field. ID cannot be changed.");
            changes = Console.ReadLine().Trim();
            switch (changes.ToLower())
            {
                case "phone number":
                    {
                        Console.WriteLine("Please enter the following bits of information.");
                        /*
                        Console.WriteLine("country code:");
                        int countryCode;
                        while (true)
                        {
                            countryCode = Console.Read();
                            if (!Enum.IsDefined(typeof(Country), countryCode))
                            {
                                Console.WriteLine("Unknown Country Code.");
                                break;
                            }
                        }
                        updatePerson.phone.countrycode = (Country)countryCode;
                        */
                        Console.WriteLine("area code:");
                        updatePerson.phone.areaCode = Console.ReadLine().Trim();
                        Console.WriteLine("number:");
                        updatePerson.phone.number = Console.ReadLine().Trim();
                        Console.WriteLine("extension:");
                        updatePerson.phone.ext = Console.ReadLine().Trim();
                        break;
                    }
                case "address":
                    {
                        Console.WriteLine("Please enter the following bits of information.");
                        Console.WriteLine("house number:");
                        updatePerson.address.houseNum = Console.ReadLine().Trim();
                        Console.WriteLine("street:");
                        updatePerson.address.street = Console.ReadLine().Trim();
                        Console.WriteLine("city:");
                        updatePerson.address.city = Console.ReadLine().Trim();
                        Console.WriteLine("state:");
                        string state;
                        while (true)
                        {
                            state = Console.ReadLine().Trim().ToUpper();
                            if (!Enum.IsDefined(typeof(State), state))
                            {
                                Console.WriteLine("Unknown State.");
                            }
                            else
                            {
                                updatePerson.address.State = (State)Enum.Parse(typeof(State), state);
                                break;
                            }
                        }
                        Console.WriteLine("country:");
                        string country;
                        while (true)
                        {
                            country = (Console.ReadLine()).ToUpper().Trim();
                            if (!Enum.IsDefined(typeof(Country), country))
                            {
                                Console.WriteLine("Unknown Country.");
                            }
                            else
                            {
                                updatePerson.address.Country = (Country)Enum.Parse(typeof(Country), country);
                                break;
                            }
                        }
                        updatePerson.phone.countrycode = updatePerson.address.Country;
                        Console.WriteLine("zipcode:");
                        updatePerson.address.zipcode = Console.ReadLine().Trim();
                        break;

                    }
                case "name":
                    {
                        Console.WriteLine("Please enter the following bits of information.");
                        Console.WriteLine("family name:");
                        updatePerson.lastName = Console.ReadLine().Trim();
                        Console.WriteLine("given name:");
                        updatePerson.firstName = Console.ReadLine().Trim();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Non-existent or non-modifiable field.");
                        break;
                    }
            }

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
                Console.WriteLine("Invalid or non-existent search criteria had been given");
                return null;
            }
        }
        
    }
}
