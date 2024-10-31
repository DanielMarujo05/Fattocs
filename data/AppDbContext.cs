using Fattocs.Models;
using Microsoft.EntityFrameworkCore;

namespace Fattocs.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ListaTarefas> ListaTarefas { get; set; }
    }

}
