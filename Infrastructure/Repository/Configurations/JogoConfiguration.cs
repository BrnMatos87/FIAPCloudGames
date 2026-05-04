using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable(typeof(Jogo).Name);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Status).HasColumnType("INT").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.DataAlteracao).HasColumnType("DATETIME");

            builder.Property(p => p.Nome).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Categoria).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Preco).HasColumnType("DECIMAL(10,2)").IsRequired();
            builder.Property(p => p.PrecoPromocional).HasColumnType("DECIMAL(10,2)");
        }
    }
}
