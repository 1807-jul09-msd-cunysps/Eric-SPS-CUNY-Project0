using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ContactLibrary;

namespace DirectoryTest
{
    [TestClass]
    public class TestDirectory
    {
        [TestMethod]
        public void TestMethod()
        {
            string[] testValues = { "Kenny", "Li", "1329", "76th street", "Brooklyn", "NY", "11229", "US", "1", "718", "331", "0868" };
            PersonDirectory addressBook = new PersonDirectory();
            Person addition = new Person();
            addition.lastName = testValues[1];
            addition.firstName = testValues[0];
            addition.address.houseNum = testValues[2];
            addition.address.street = testValues[3];
            addition.address.city = testValues[4];
            string state = testValues[5];
            addition.address.State = (State)Enum.Parse(typeof(State), state);
            addition.address.Country = (Country)Enum.Parse(typeof(Country), testValues[7]);
            addition.address.zipcode = testValues[6];
            int countryCode = Int32.Parse(testValues[8]);
            addition.phone.countrycode = (Country)countryCode;
            addition.phone.areaCode = testValues[9];
            addition.phone.number = testValues[10];
            addition.phone.ext = testValues[11];

            addressBook.Add(addition);

            Assert.AreEqual(("ID: 0 \n" +
                "Name: Li, Kenny \n" +
                $"Address: 1329 76th street, Brooklyn, NY, 11229, US \n" +
                $"Phone Number: 1(718)331-0868 \n"), addressBook.ToString());
        }
    }
}
