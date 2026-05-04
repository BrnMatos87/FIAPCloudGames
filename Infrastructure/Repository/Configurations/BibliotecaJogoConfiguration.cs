using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class BibliotecaJogoConfiguration : IEntityTypeConfiguration<BibliotecaJogo>
    {
        public void Configure(EntityTypeBuilder<BibliotecaJogo> builder)
        {
            builder.ToTable(typeof(BibliotecaJogo).Name);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Status).HasColumnType("INT").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.DataAlteracao).HasColumnType("DATETIME");

            builder.Property(p => p.UsuarioId).HasColumnType("INT").IsRequired();
            builder.Property(p => p.JogoId).HasColumnType("INT").IsRequired();
            builder.Property(p => p.DataAquisicao).HasColumnType("DATETIME").IsRequired();

            builder.HasOne(p => p.Usuario)
            .WithMany(c => c.BibliotecaJogos)
            .HasPrincipalKey(c => c.Id);

            builder.HasOne(p => p.Jogo)
           .WithMany(c => c.BibliotecaJogos)
           .HasPrincipalKey(c => c.Id);
        }
    }
}
