using Microsoft.EntityFrameworkCore;

namespace RegistroVentas.Api.Data;

public sealed class RegistroDbContext(DbContextOptions<RegistroDbContext> options)
    : DbContext(options)
{
    public DbSet<RegistroOperacion> RegistrosOperaciones => Set<RegistroOperacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegistroOperacion>(entity =>
        {
            entity.ToTable("RegistrosOperaciones");

            entity.HasKey(x => x.Pk);

            entity.Property(x => x.Pk)
                .ValueGeneratedOnAdd();

            entity.Property(x => x.Operacion)
                .HasColumnType("TEXT")
                .IsRequired();

            entity.Property(x => x.Importe)
                .HasColumnType("NUMERIC")
                .IsRequired();

            entity.Property(x => x.Cliente)
                .HasColumnType("TEXT")
                .IsRequired();

            entity.Property(x => x.Referencia)
                .HasColumnType("TEXT")
                .IsRequired();

            entity.HasIndex(x => x.Referencia)
                .IsUnique();

            entity.Property(x => x.Estatus)
                .HasColumnType("TEXT")
                .IsRequired();

            entity.Property(x => x.Secreto)
                .HasColumnType("TEXT")
                .IsRequired();
        });
    }
}