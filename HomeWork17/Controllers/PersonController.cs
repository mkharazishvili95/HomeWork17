using HomeWork17.Data;
using HomeWork17.Models;
using HomeWork17.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _personContext;
        public PersonController(PersonContext personContext)
        {
            _personContext = personContext;
        }
        [HttpPost("CreatePerson")]
        public ActionResult<Person> CreatePerson(Person person)
        {
            var validator = new PersonValidator();
            var validatorResult = validator.Validate(person);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }
            else
            {
                _personContext.Add(person);
                _personContext.SaveChanges();
                return Ok("Person successfully created!");
            }
        }
        [HttpGet("GetById")]
        public ActionResult GetById(int id)
        {
            var person = _personContext.Persons.Find(id);
            if(person == null)
            {
                return NotFound("There is not any person by this ID!");
            }
            else
            {
                return Ok(person);
            }
        }
        [HttpGet("GetAllPerson")]
        public ActionResult<IEnumerable<Person>> GetAllPerson() {
         return (_personContext.Persons);
        }

        [HttpGet("FilterByCountry")]
        public ActionResult<IEnumerable<Person>> FilterByCountry(string country)
        {
            var filtred = _personContext.Persons.Where(x => x.PersonAddress.Country.ToUpper() == country.ToUpper());
            
                return Ok(filtred);
        }
        [HttpPut("UpdatePerson")]
        public IActionResult UpdatePerson(Person updatedPerson, int id)
        {
            var person = _personContext.Persons.Include(p => p.PersonAddress).SingleOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound("There is no any person by this ID!");
            }

            var validator = new PersonValidator();
            var validatorResult = validator.Validate(updatedPerson);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors);
            }

            if (person.PersonAddress != null)
            {
                person.PersonAddress.HomeNumber = updatedPerson.PersonAddress.HomeNumber;
                person.PersonAddress.Country = updatedPerson.PersonAddress.Country;
                person.PersonAddress.City = updatedPerson.PersonAddress.City;
            }

            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Salary = updatedPerson.Salary;
            person.JobPosition = updatedPerson.JobPosition;
            person.WorkExperience = updatedPerson.WorkExperience;
            person.Email = updatedPerson.Email;

            _personContext.Update(person);
            _personContext.SaveChanges();

            return Ok("Person Has Updated!");
        }


        [HttpDelete("DeletePerson")]
        public IActionResult DeletePerson(int id)
        {
            var person = _personContext.Persons.Include(p => p.PersonAddress).SingleOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound("There is no person by this ID!");
            }

            if (person.PersonAddress != null)
            {
                _personContext.Addressess.Remove(person.PersonAddress);
            }

            _personContext.Persons.Remove(person);
            _personContext.SaveChanges();
            
            return Ok("Person has successfully deleted from the database!");
        }
    }
}
