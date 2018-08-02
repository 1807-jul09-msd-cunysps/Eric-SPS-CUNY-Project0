using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

                cmdStr = "SELECT Person.ID, firstName, lastName, address, phone, " +
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
                        p.phone.areaCode = dr["areaCode"].ToString();
                        p.phone.number = dr["number"].ToString();
                        p.phone.ext = dr["ext"].ToString();

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
        public List<Person> SearchPersons(long pid = 0, string firstName = "", string lastName = "", string zipCode = "", string city = "")
        {
            using (con = new SqlConnection(conStr))
            {
                List<Person> persons = new List<Person>();

                cmdStr = "SELECT Person.ID, firstName, lastName, address, phone, " +
                    "PersonAddress.ID AS addressID, houseNum, street, city, state, Country, zipcode, " +
                    "PersonPhone.ID AS phoneID, countryCode, areaCode, number, ext " +
                    "FROM (Person left join PersonAddress " +
                    "ON Person.address = PersonAddress.ID) " +
                    "left join PersonPhone ON Person.phone = PersonPhone.ID";
                if (firstName != "")
                {
                    cmdStr += "WHERE Person.firstName = @searchValue;";
                    con.Open();
                    cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.Add("@searchValue", SqlDbType.VarChar).Value = firstName;
                }
                else if (lastName != "")
                {
                    cmdStr += "WHERE Person.lastName = @searchValue;";
                    con.Open();
                    cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.Add("@searchValue", SqlDbType.VarChar).Value = lastName;
                }
                else if (zipCode != "")
                {
                    cmdStr += "WHERE PersonAddress.zipcode = @searchValue;";
                    con.Open();
                    cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.Add("@searchValue", SqlDbType.VarChar).Value = zipCode;
                }
                else if (city != "")
                {
                    cmdStr += "WHERE PersonAddress.city = @searchValue;";
                    con.Open();
                    cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.Add("@searchValue", SqlDbType.VarChar).Value = city;
                }
                else if (pid == 0)
                {
                    cmdStr += "WHERE Person.ID = @searchValue;";
                    con.Open();
                    cmd = new SqlCommand(cmdStr, con);
                    cmd.Parameters.Add("@searchValue", SqlDbType.Int).Value = pid;
                }
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
                        p.address.Country = (Country)Enum.Parse(typeof(Country), dr["Country"].ToString());
                        p.address.zipcode = dr["zipcode"].ToString();
                        //Phone Table
                        p.phone.Pid = Convert.ToInt32(dr["phoneID"]);
                        p.phone.countrycode = (Country)Enum.Parse(typeof(Country), dr["countryCode"].ToString());
                        p.phone.areaCode = dr["areaCode"].ToString();
                        p.phone.number = dr["number"].ToString();
                        p.phone.ext = dr["ext"].ToString();

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
        //Insert
        public void InsertPersons()
        {

        }
        //Delete
        public void DeletePersons()
        {

        }
        //update
        public void UpdatePerson()
        {

        }
    }
}