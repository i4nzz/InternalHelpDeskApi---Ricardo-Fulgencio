using InternalHelpDeskApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternalHelpDeskApi.Infrastructure.Persistence
{
    public class DataBase : DbContext
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
        }
        public DbSet<Solicitante> Solicitantes { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Atendente> Atendentes { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBase).Assembly);
        }
    }
}