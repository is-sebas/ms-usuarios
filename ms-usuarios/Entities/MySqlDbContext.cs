using Microsoft.EntityFrameworkCore;

namespace ms_usuarios.Entities
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIOS");
                entity.HasKey(u => u.IdUsuario);
                entity.Property(u => u.IdUsuario).HasColumnName("IDUSUARIO");
                entity.Property(u => u.Nombres).HasColumnName("NOMBRES");
                entity.Property(u => u.Apellidos).HasColumnName("APELLIDOS");
                entity.Property(u => u.Cedula).HasColumnName("CEDULA");
                entity.Property(u => u.Ruc).HasColumnName("RUC");
                entity.Property(u => u.Sexo).HasColumnName("SEXO");
                entity.Property(u => u.Email).HasColumnName("EMAIL");
                entity.Property(u => u.Direccion).HasColumnName("DIRECCION");
                entity.Property(u => u.Perfil).HasColumnName("PERFIL");
                entity.Property(u => u.Password).HasColumnName("PASSWORD");
                entity.Property(u => u.FechaCreacion).HasColumnName("FECHACREACION");
                entity.Property(u => u.EstadoUsuario).HasColumnName("ESTADOUSUARIO");
            });
        }
    }
}
