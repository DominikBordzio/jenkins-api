using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vagrant2_api.Dto;
using vagrant2_api.Models;

namespace vagrant2_api.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly PersonDbContext _personDbContext;
        
        public PersonController(PersonDbContext personDbContext)
        {
            _personDbContext = personDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> List() => await _personDbContext.Persons.AsNoTracking().ToListAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var foundEntity = await _personDbContext.Persons.FindAsync(id);

            if (foundEntity is null)
            {
                return NotFound();
            }

            return Ok(foundEntity);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonAddDto personAddOrUpdateDto)
        {
            var entity = await _personDbContext.Persons.AddAsync(personAddOrUpdateDto.ToPerson());
            await _personDbContext.SaveChangesAsync();
            return Created($"api/person/{entity.Entity.PersonId}", entity.Entity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            _personDbContext.Entry(person).State = EntityState.Modified;
            await _personDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personDbContext.Persons.FindAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            _personDbContext.Persons.Remove(person);
            await _personDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}