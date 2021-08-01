using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Data.Mappings
{
    class FilmesMapping : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(f => f.Categoria)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(f => f.Sinopse)
                .IsRequired()
                .HasColumnType("varchar(2000)");

            builder.Property(f => f.Imagem)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.ToTable("Filmes");
        }
    }

    class DistrubidoraMapping : IEntityTypeConfiguration<Distribuidora>
    {
      

        public void Configure(EntityTypeBuilder<Distribuidora> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(d => d.Imagem)
             .IsRequired()
             .HasColumnType("varchar(200)");

            builder.Property(d => d.Sobre)
              .IsRequired()
              .HasColumnType("varchar(2000)");

            // 1: N

            builder.HasMany(d => d.Filmes)
                .WithOne(d => d.Distribuidora)
                .HasForeignKey(d => d.DistribuidoraId);

            builder.ToTable("Distribuidoras");
        }
    }
}
