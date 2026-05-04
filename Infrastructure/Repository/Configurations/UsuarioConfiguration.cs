using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(typeof(Usuario).Name);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Status).HasColumnType("INT").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.DataAlteracao).HasColumnType("DATETIME");

            builder.Property(p => p.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Senha).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Perfil).HasColumnType("INT").IsRequired();
        }
    }
}
