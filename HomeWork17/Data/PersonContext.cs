using HomeWork17.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork17.Data
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext>options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addressess { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=HomeWork17SQL;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
