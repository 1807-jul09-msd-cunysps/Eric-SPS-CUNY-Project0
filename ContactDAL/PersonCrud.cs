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
        public void InsertPerson(Person p)
        {
            using (con = new SqlConnection(conStr))
            {
                string insertAddress = "INSERT INTO PersonAddress (houseNum,street,city,state,Country,zipcode) " +
                    "Values (@houseNum,@street,@city,@state,@Country,@zipcode)";
                string insertPhone = "INSERT INTO PersonPhone (countryCode,areaCode,number,ext) " +
                    "Values (@countryCode,@areaCode,@number,@ext)";
                string insertPerson = "INSERT INTO Person (firstName,lastName,address,phone) " +
                    "Values (@firstName,@lastName,(SELECT COUNT(*) from PersonAddress),(SELECT COUNT(*) from PersonPhone))";

                try
                {
                    con.Open();

                    SqlCommand address = new SqlCommand(insertAddress, con);
                    SqlCommand phone = new SqlCommand(insertPhone, con);
                    SqlCommand person = new SqlCommand(insertPerson, con);

                    address.Parameters.Add("@houseNum", SqlDbType.VarChar, 255).Value = p.address.houseNum;
                    address.Parameters.Add("@street", SqlDbType.VarChar, 255).Value = p.address.street;
                    address.Parameters.Add("@city", SqlDbType.VarChar, 255).Value = p.address.city;
                    address.Parameters.Add("@state", SqlDbType.VarChar, 255).Value = p.address.State;
                    address.Parameters.Add("@Country", SqlDbType.VarChar, 255).Value = p.address.Country;
                    address.Parameters.Add("@zipcode", SqlDbType.VarChar, 255).Value = p.address.zipcode;

                    phone.Parameters.Add("@countryCode", SqlDbType.Int).Value = p.phone.countrycode;
                    phone.Parameters.Add("@areaCode", SqlDbType.Int).Value = p.phone.areaCode;
                    phone.Parameters.Add("@number", SqlDbType.Int).Value = p.phone.number;
                    phone.Parameters.Add("@ext", SqlDbType.Int).Value = p.phone.ext;

                    person.Parameters.Add("@firstName", SqlDbType.VarChar, 255).Value = p.firstName;
                    person.Parameters.Add("@lastName", SqlDbType.VarChar, 255).Value = p.lastName;

                    address.ExecuteNonQuery();
                    phone.ExecuteNonQuery();
                    person.ExecuteNonQuery();
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
        }
        //Delete
        public void DeletePerson(Person p)
        {
            using (con = new SqlConnection(conStr))
            {
                cmdStr = "Delete from Person where ID = @pid" +
                    "Delete from PersonAddress where ID = @aid" +
                    "Delete from PersonPhone where ID = @phid";

                try
                {
                    con.Open();

                    cmd = new SqlCommand(cmdStr, con);

                    cmd.Parameters.Add("@pid", SqlDbType.Int).Value = p.Pid;
                    cmd.Parameters.Add("@aid", SqlDbType.Int).Value = p.address.Pid;
                    cmd.Parameters.Add("@phid", SqlDbType.Int).Value = p.phone.Pid;

                    cmd.ExecuteNonQuery();
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
        }
        //update
        public void UpdatePerson(Person p)
        {
            using (con = new SqlConnection(conStr))
            {
                cmdStr = "UPDATE Person set firstName = @firstN, lastName = @lastN where ID = @pid;" +
                    "UPDATE PersonAddress set houseNum = @houseNum, street = @street, city = @city, state = @state, Country = @country, zipcode = @zip " +
                    "where ID = (SELECT address from Person where ID = @pid);" +
                    "UPDATE PersonPhone set countryCode = @cc, areaCode = @ac, number = @num, ext = @ext " +
                    "where ID = (SELECT phone from Person where ID = @pid";

                try
                {
                    cmd = new SqlCommand(cmdStr, con);

                    cmd.Parameters.Add("@pid", SqlDbType.Int).Value = p.Pid;
                    cmd.Parameters.Add("@firstN", SqlDbType.VarChar).Value = p.firstName;
                    cmd.Parameters.Add("@lastN", SqlDbType.VarChar).Value = p.lastName;
                    cmd.Parameters.Add("@houseNum", SqlDbType.VarChar).Value = p.address.houseNum;
                    cmd.Parameters.Add("@street", SqlDbType.VarChar).Value = p.address.street;
                    cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = p.address.city;
                    cmd.Parameters.Add("@state", SqlDbType.VarChar).Value = p.address.State;
                    cmd.Parameters.Add("@country", SqlDbType.VarChar).Value = p.address.Country;
                    cmd.Parameters.Add("@zip", SqlDbType.VarChar).Value = p.address.zipcode;
                    cmd.Parameters.Add("@cc", SqlDbType.Int).Value = p.phone.countrycode;
                    cmd.Parameters.Add("@ac", SqlDbType.Int).Value = p.phone.areaCode;
                    cmd.Parameters.Add("@num", SqlDbType.Int).Value = p.phone.number;
                    cmd.Parameters.Add("@ext", SqlDbType.Int).Value = p.phone.ext;

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}