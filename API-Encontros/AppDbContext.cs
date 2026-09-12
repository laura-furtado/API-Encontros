using Microsoft.EntityFrameworkCore;

namespace API_Encontros
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Encontro> Encontros { get; set; }
        public DbSet<Clube> Clubes { get; set; }
        public DbSet<LivroClube> LivroClubes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encontro>(entity =>
            {

                entity.ToTable("encontros");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.titulo).HasColumnName("titulo");
                entity.Property(e => e.descricao).HasColumnName("descricao");
                entity.Property(e => e.data_hora).HasColumnName("data_hora");
                entity.Property(e => e.local).HasColumnName("local");
                entity.Property(e => e.link_encontro).HasColumnName("link_encontro");
                entity.Property(e => e.criado_em).HasColumnName("criado_em");
                entity.Property(e => e.atualizado_em).HasColumnName("atualizado_em");
                entity.Property(e => e.clube_id).HasColumnName("clube_id");
                entity.Property(e => e.livro_clube_id).HasColumnName("livro_clube_id");

                entity.Property(e => e.situacao)
                      .HasColumnName("situacao")
                      .HasConversion<string>();
            });

            modelBuilder.Entity<Clube>(entity =>
            {
                entity.ToTable("clubes");
                entity.Property(e => e.id).HasColumnName("id");
            });

            modelBuilder.Entity<LivroClube>(entity =>
            {
                entity.ToTable("livros_clube");
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.clube_id).HasColumnName("clube_id");
            });
        }
    }
}