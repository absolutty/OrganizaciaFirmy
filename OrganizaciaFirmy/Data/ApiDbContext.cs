using Microsoft.EntityFrameworkCore;
using OrganizaciaFirmy.Models;

namespace OrganizaciaFirmy.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Zamestnanec> ZoznamZamestnancov { get; set; }

        public DbSet<Divizia> ZoznamDivizii { get; set; }

        public DbSet<Projekt> ZoznamProjektov { get; set; }

        public DbSet<Oddelenie> ZoznamOddeleni { get; set; }
    }
}
