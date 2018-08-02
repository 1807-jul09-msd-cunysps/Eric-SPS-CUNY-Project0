using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ContactLibrary;

namespace ContactDAL
{
    public class PersonCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        string conStr = "Data Source=rev-training.database.windows.net;Initial Catalog=Rev-Training-Eric;Persist Security Info=True;User ID=tslanchor;Password=Lol159753";
        string cmdStr = "";
        //GetPersons
        public List<Person> GetPersons()
        {
            using (con = new SqlConnection(conStr))
            {
                List<Person> persons = new List<Person>();

                cmdStr = "Person.ID, firstName, lastName, address, phone, " +
                    "PersonAddress.ID AS addressID, houseNum, street, city, state, Country, zipcode, " +
                    "PersonPhone.ID AS phoneID, countryCode, areaCode, number, ext " +
                    "FROM (Person left join PersonAddress " +
                    "ON Person.address = PersonAddress.ID) " +
                    "left join PersonPhone ON Person.phone = PersonPhone.ID;";
                con.Open();
                cmd = new SqlCommand(cmdStr, con);
                try
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Person p = new Person();
                        //Person Table
                        p.Pid = Convert.ToInt32(dr["ID"]);
                        p.firstName = dr["firstName"].ToString();
                        p.lastName = dr["lastName"].ToString();
                        //Address Table
                        p.address.Pid = Convert.ToInt32(dr["addressID"]);
                        p.address.houseNum = dr["houseNum"].ToString();
                        p.address.street = dr["street"].ToString();
                        p.address.city = dr["city"].ToString();
                        p.address.State = (State)Enum.Parse(typeof(State), dr["state"].ToString());
                        p.address.Country = (Country)Enum.Parse(typeof(Country),dr["Country"].ToString());
                        p.address.zipcode = dr["zipcode"].ToString();
                        //Phone Table
                        p.phone.Pid = Convert.ToInt32(dr["phoneID"]);
                        p.phone.countrycode = (Country)Enum.Parse(typeof(Country), dr["countryCode"].ToString());

                        persons.Add(p);
                        p = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    dr.Close();
                }
                return persons;
            }
        }
        //GetPersonById
        //Insert
        public void 
        //Delete
        //update
    }
}