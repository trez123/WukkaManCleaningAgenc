using WukkamanCleaningAgencyApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WukkamanCleaningAgencyApi.Data
{
    public class WukkamanDbContext : DbContext
    {
        public WukkamanDbContext(DbContextOptions<WukkamanDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Shift> Shifts { get; set; }
    }
}
