using WukkamanCleaningAgencyApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WukkamanCleaningAgencyApi.Data
{
    public class WukkamanDbContext : IdentityDbContext
    {
        public WukkamanDbContext(DbContextOptions<WukkamanDbContext> options) : base(options) { }

        public DbSet<Employee> Students { get; set; }

        public DbSet<Shift> Shifts { get; set; }
    }
}
