using BigBirds.Domain.Entities;
using System.Data.Entity;

namespace BigBirds.Domain.Contexts
{
    public class BigBirdsContext : DbContext
    {
        public BigBirdsContext() : base(Constants.BIG_BIRDS_CONNECTION)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>().ToTable("TB_ESTADO");
            modelBuilder.Entity<Categoria>().ToTable("TB_CATEGORIA");
            modelBuilder.Entity<SituacaoReclamacao>().ToTable("TB_SITUACAO_RECLAMACAO");
            modelBuilder.Entity<Localizacao>().ToTable("TB_LOCALIZACAO");
            modelBuilder.Entity<Orgao>().ToTable("TB_ORGAO");
            modelBuilder.Entity<Reclamacao>().ToTable("TB_RECLAMACAO");
        }
    }
}
