using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Cliente.Map(modelBuilder);
            Filme.Map(modelBuilder);
            Locacao.Map(modelBuilder);
        }

        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Filme> filmes { get; set; }
        public DbSet<Locacao> locacoes { get; set; }
    }
}
