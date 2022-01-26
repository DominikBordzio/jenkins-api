using Microsoft.EntityFrameworkCore;
using vagrant2_api.Models;

namespace vagrant2_api
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }
    }
}