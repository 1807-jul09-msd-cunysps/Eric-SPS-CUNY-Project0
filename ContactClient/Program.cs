using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ContactLibrary;
using Newtonsoft.Json;

namespace ContactClient
{
    class Program
    {

        static void Main(string[] args)
        {
            Directory addressBook = new Directory();

            Console.WriteLine("What do you wish to do?" +
                "1. Add a person" +
                "2. Delete a person(s)" +
                "3. Update a person" +
                "4. Search for people" +
                "5. Push directory onto SQL database" +
                "6. Read contacts from SQL database");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        addressBook.Add(createPerson());
                        break;
                    }
                case "2":
                    {
                        addressBook.Delete();
                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("What is the ID of the contact you wish to update? " +
                            "If you do not know, use the search function to find it.");
                        long pid = Console.Read();
                        addressBook.Update(pid);
                        break;
                    }
                case "4":
                    {
                        Search();
                        break;
                    }
                case "5":
                    {
                        pushToDB(addressBook);
                        break;
                    }
                case "6":
                    {
                        pullFromDB();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Input.");
                        break;
                    }
            }

        }
        public static void QueryInsertion(Person somePerson, SqlCommand address, SqlCommand phone, SqlCommand person)
        {
            address.Parameters.Add("@houseNum", SqlDbType.VarChar, 255).Value = somePerson.address.houseNum;
            address.Parameters.Add("@street", SqlDbType.VarChar, 255).Value = somePerson.address.street;
            address.Parameters.Add("@city", SqlDbType.VarChar, 255).Value = somePerson.address.city;
            address.Parameters.Add("@state", SqlDbType.VarChar, 255).Value = somePerson.address.State;
            address.Parameters.Add("@Country", SqlDbType.VarChar, 255).Value = somePerson.address.Country;
            address.Parameters.Add("@zipcode", SqlDbType.VarChar, 255).Value = somePerson.address.zipcode;
            
            phone.Parameters.Add("@countryCode", SqlDbType.Int).Value = somePerson.phone.countrycode;
            phone.Parameters.Add("@areaCode", SqlDbType.Int).Value = somePerson.phone.areaCode;
            phone.Parameters.Add("@number", SqlDbType.Int).Value = somePerson.phone.number;
            phone.Parameters.Add("@ext", SqlDbType.Int).Value = somePerson.phone.ext;

            person.Parameters.Add("@firstName", SqlDbType.VarChar, 255).Value = somePerson.firstName;
            person.Parameters.Add("@lastName", SqlDbType.VarChar, 255).Value = somePerson.lastName;

            address.ExecuteNonQuery();
            phone.ExecuteNonQuery();
            person.ExecuteNonQuery();

        }
        public static Person createPerson()
        {
            Person addition = new Person();
            Console.WriteLine("Please enter the following bits of information.");
            Console.WriteLine("family name:");
            addition.lastName = Console.ReadLine();
            Console.WriteLine("given name:");
            addition.firstName = Console.ReadLine();
            Console.WriteLine("house number:");
            addition.address.houseNum = Console.ReadLine();
            Console.WriteLine("street:");
            addition.address.street = Console.ReadLine();
            Console.WriteLine("city:");
            addition.address.city = Console.ReadLine();
            Console.WriteLine("state:");
            string state = Console.ReadLine();
            addition.address.State = (State)Enum.Parse(typeof(State), state);
            Console.WriteLine("country:");
            addition.address.Country = (Country)Enum.Parse(typeof(Country),Console.ReadLine());
            Console.WriteLine("zipcode:");
            addition.address.zipcode = Console.ReadLine();
            Console.WriteLine("country code:");
            int countryCode = Int32.Parse(Console.ReadLine());
            addition.phone.countrycode = (Country)countryCode;
            Console.WriteLine("area code:");
            addition.phone.areaCode = Console.ReadLine();
            Console.WriteLine("number:");
            addition.phone.number = Console.ReadLine();
            Console.WriteLine("extension:");
            addition.phone.ext = Console.ReadLine();
            return addition;
        }
        public static void pushToDB(Directory addressBook)
        {
            SqlConnection con = null;
            string insertAddress = "INSERT INTO PersonAddress (houseNum,street,city,state,Country,zipcode) " +
                "Values (@houseNum,@street,@city,@state,@Country,@zipcode)";
            string insertPhone = "INSERT INTO PersonPhone (countryCode,areaCode,number,ext) " +
                "Values (@countryCode,@areaCode,@number,@ext)";
            string insertPerson = "INSERT INTO Person (firstName,lastName,address,phone) " +
                "Values (@firstName,@lastName,(SELECT COUNT(*) from PersonAddress),(SELECT COUNT(*) from PersonPhone))";

            string conStr = "Data Source=rev-training.database.windows.net;Initial Catalog=Rev-Training-Eric;Persist Security Info=True;User ID=tslanchor;Password=Lol159753";

            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                foreach (Person element in addressBook.directory)
                {
                    SqlCommand addressInsertion = new SqlCommand(insertAddress, con);
                    SqlCommand phoneInsertion = new SqlCommand(insertPhone, con);
                    SqlCommand personInsertion = new SqlCommand(insertPerson, con);


                    QueryInsertion(element, addressInsertion, phoneInsertion, personInsertion);

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public static void pullFromDB()
        {
            SqlConnection con = null;
            string displayCommand = "select ID, CONCAT(firstname,' ',lastname) as Name," +
                "(select CONCAT(houseNum, ' ', street, ', ', city, ', ', State, ', ', zipcode, ', ', Country) from PersonAddress WHERE Person.address = ID) AS HomeAddress," +
                "(select CONCAT(countrycode, '(', areaCode, ')', number, '-', ext) from PersonPhone where Person.phone = ID) as PhoneNumber " +
                "from Person";

            string conStr = "Data Source=rev-training.database.windows.net;Initial Catalog=Rev-Training-Eric;Persist Security Info=True;User ID=tslanchor;Password=Lol159753";

            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                SqlCommand displayCmd = new SqlCommand(displayCommand, con);
                SqlDataReader dr = displayCmd.ExecuteReader();

                Console.WriteLine("Id".PadRight(4) + "Name".PadRight(20) + "Address".PadRight(50) + "Phone Number".PadRight(12));
                while (dr.Read())
                {
                    Console.WriteLine((dr[0] + " ").PadRight(4) + (dr[1] + " ").PadRight(20) + (dr[2] + " ").PadRight(50) + (dr[3] + "").PadRight(12));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public static void Search()
        {
            Console.WriteLine("What would you like to search by? \n" +
                "1. ID" +
                "2. First name" +
                "3. Last name" +
                "4. Zip code" +
                "5. City" +
                "6. Phone Number");
            switch (Console.ReadLine().ToLower())
            {
                case "6":
                    {
                        Console.WriteLine("Please enter the phone number in a [countrycode]([ areaCode])[ number]-[ ext] format:");
                        
                        break;
                    }
            }
        }
    }
}
