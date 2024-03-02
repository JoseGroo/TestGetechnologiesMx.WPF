using Microsoft.EntityFrameworkCore;
using TestGetechnologiesMx.Repository.Entities;

namespace TestGetechnologiesMx.Repository
{
    public class ProjectModel : DbContext
    {
        public ProjectModel(DbContextOptions<ProjectModel> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
