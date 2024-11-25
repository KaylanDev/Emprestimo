using Emprestimo.Models;
using Microsoft.EntityFrameworkCore;

namespace Emprestimo.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options)
        {
        }

        public DbSet<EmprestimosModel> Emprestimos { get; set; }
    }
}
