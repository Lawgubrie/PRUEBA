using API_Cine.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Cine.Contexts;

public partial class CineDbContext : DbContext
{
    public CineDbContext()
    {
    }

    public CineDbContext(DbContextOptions<CineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<PeliculaSalaCine> PeliculaSalaCines { get; set; }

    public virtual DbSet<SalaCine> SalaCines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CINE_DB_FIRST;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PK__Pelicula__E239B742E002E92A");

            entity.ToTable("Pelicula");

            entity.Property(e => e.IdPelicula).HasColumnName("Id_Pelicula");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<PeliculaSalaCine>(entity =>
        {
            entity.HasKey(e => e.IdPeliculaSala).HasName("PK__Pelicula__BDBF0AFC7A89232E");

            entity.ToTable("Pelicula_SalaCine");

            entity.Property(e => e.IdPeliculaSala).HasColumnName("Id_Pelicula_Sala");
            entity.Property(e => e.FechaFin).HasColumnName("Fecha_Fin");
            entity.Property(e => e.FechaPublicacion).HasColumnName("Fecha_Publicacion");
            entity.Property(e => e.IdPelicula).HasColumnName("Id_Pelicula");
            entity.Property(e => e.IdSalaCine).HasColumnName("Id_Sala_Cine");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.PeliculaSalaCines)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pelicula_SalaCine_Pelicula");

            entity.HasOne(d => d.IdSalaCineNavigation).WithMany(p => p.PeliculaSalaCines)
                .HasForeignKey(d => d.IdSalaCine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pelicula_SalaCine_SalaCine");
        });

        modelBuilder.Entity<SalaCine>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("PK__Sala_Cin__ACDC9E2398A415FE");

            entity.ToTable("Sala_Cine");

            entity.Property(e => e.IdSala).HasColumnName("Id_Sala");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
