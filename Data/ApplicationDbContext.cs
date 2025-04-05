using ClienteService.Models;
using Microsoft.EntityFrameworkCore;


namespace ClienteService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
    }

}
