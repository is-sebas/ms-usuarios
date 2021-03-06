using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_usuarios.Entities
{
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO", TypeName = "VARCHAR(100)")]
        [Description("Identificador/Secuencia del Usuario")]
        public string IdUsuario { get; set; }

        [Column("NOMBRES", TypeName = "VARCHAR(100)")]
        [Description("Nombre/s")]
        public string Nombres { get; set; }

        [Column("APELLIDOS", TypeName = "VARCHAR(100)")]
        [Description("Apellido/s")]
        public string Apellidos { get; set; }

        [Column("CEDULA", TypeName = "VARCHAR(15)")]
        [Description("Nro. de Cédula")]
        public string Cedula { get; set; }

        [Column("RUC", TypeName = "VARCHAR(15)")]
        [Description("Nro. de Identificación del Registro Único del Contribuyente: Persona Física")]
        public string Ruc { get; set; }

        [Column("SEXO", TypeName = "VARCHAR(1)")]
        [Description("Sexo de la persona: M - (Masculino), F (Femenino)")]
        public string Sexo { get; set; }

        [Column("EMAIL", TypeName = "VARCHAR(70)")]
        [Description("Correo personal del usuario")]
        public string Email { get; set; }

        [Column("DIRECCION", TypeName = "VARCHAR(100)")]
        [Description("Dirección de residencia")]
        public string Direccion { get; set; }

        [Column("PERFIL", TypeName = "FLOAT")]
        [Description("Tipo de Perfil del Usuario: 1 - (Administrador), 2 - (Mesero), 3 - (Cliente)")]
        public float Perfil { get; set; }

        [Column("PASSWORD", TypeName = "VARCHAR(80)")]
        [Description("Clave/Contraseña Cifrada")]
        public string Password { get; set; }

        [Column("FECHACREACION", TypeName = "DATE")]
        [Description("Fecha de Registro del usuario")]
        public System.DateTime FechaCreacion { get; set; }

        [Column("ESTADOUSUARIO", TypeName = "FLOAT")]
        [Description("Estado del Usuario, 1 - (Activo), 2 - (Inactivo)")]
        public float EstadoUsuario { get; set; }
    }
}
