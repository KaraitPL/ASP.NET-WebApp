using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApp.Models.Kontakt>? Kontakt { get; set; }
        public DbSet<WebApp.Models.Kategoria>? Kategoria { get; set; }
        public DbSet<WebApp.Models.Podkategoria>? Podkategoria { get; set; }
    }
}