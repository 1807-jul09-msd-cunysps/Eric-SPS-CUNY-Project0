using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactDAL;
using ContactLibrary;
using System.Web.Http.Cors;

namespace ContactAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PersonController : ApiController
    {
        PersonCrud crud = new PersonCrud();
        //READ
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var person = crud.GetPersons();
            return person;
        }
        //ADD Person
        [HttpPost]
        public IHttpActionResult Post(Person p)
        {
            if (p != null)
            {
                crud.InsertPerson(p);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        // to do  Put
        [HttpPut]
        public void Put()
        {

        }
        // to do Delete
        [HttpDelete]
        public void Delete(Person p)
        {
            crud.DeletePerson(p);
        }
    }
}