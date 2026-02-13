using bibliosys.be.domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace bibliosys.be.infrastructure.Database
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Bibliotecario> Bibliotecarios { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Libro> Libros { get; set; } = null!;
        public DbSet<Prestamo> Prestamos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bibliotecario>(entity =>
            {
                entity.ToTable("Bibliotecarios");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre_Completo).HasColumnName("nombre_completo").HasMaxLength(150).IsRequired();
                entity.Property(e => e.Cedula).HasColumnName("cedula").HasMaxLength(20).IsRequired();
                entity.Property(e => e.Correo_Electronico).HasColumnName("correo_electronico").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(255);
                entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasIndex(e => e.Cedula).IsUnique();
                entity.HasIndex(e => e.Correo_Electronico).IsUnique();
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.ToTable("Libros");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Codigo).HasColumnName("codigo").HasMaxLength(50).IsRequired();
                entity.Property(e => e.Titulo).HasColumnName("titulo").HasMaxLength(200).IsRequired();
                entity.Property(e => e.Autor).HasColumnName("autor").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Editorial).HasColumnName("editorial").HasMaxLength(100);
                entity.Property(e => e.Anio_Publicacion).HasColumnName("anio_publicacion");
                entity.Property(e => e.Genero).HasColumnName("genero").HasMaxLength(50);
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.Ubicacion).HasColumnName("ubicacion").HasMaxLength(100);
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasIndex(e => e.Codigo).IsUnique();
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.ToTable("Prestamos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Usuario_Id).HasColumnName("usuario_id");
                entity.Property(e => e.Libro_Id).HasColumnName("libro_id");
                entity.Property(e => e.Fecha_Prestamo).HasColumnName("fecha_prestamo");
                entity.Property(e => e.Fecha_Limite_Devolucion).HasColumnName("fecha_limite_devolucion");
                entity.Property(e => e.Fecha_Real_Devolucion).HasColumnName("fecha_real_devolucion");
                entity.Property(e => e.Observacion).HasColumnName("observacion");
                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.Usuario_Id)
                    .HasConstraintName("FK_Prestamos_Usuarios");

                entity.HasOne(e => e.Libro)
                    .WithMany()
                    .HasForeignKey(e => e.Libro_Id)
                    .HasConstraintName("FK_Prestamos_Libros");
            });
        }
    }
}
