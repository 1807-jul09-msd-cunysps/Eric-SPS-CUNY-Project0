using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactDAL;
using ContactLibrary;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace ContactAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PersonController : ApiController
    {
        PersonCrud crud = new PersonCrud();
        //READ
        [HttpGet]
        public IHttpActionResult Get()
        {
            var person = crud.GetPersons();
            if (person != null)
            {
                return Ok(person);
            }
            else
            {
                return BadRequest();
            }
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
        public IHttpActionResult Put(Person p)
        {
            if (p != null)
            {
                crud.UpdatePerson(p);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        // to do Delete
        [HttpDelete]
        public IHttpActionResult Delete(Person p)
        {
            if (p != null)
            {
                crud.DeletePerson(p);
                return Ok();    
            }
            else
            {
                return BadRequest();
            }
        }
    }
}