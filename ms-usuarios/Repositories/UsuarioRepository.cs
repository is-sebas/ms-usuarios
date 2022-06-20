using Microsoft.EntityFrameworkCore;
using ms_usuarios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ms_usuarios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MySqlDbContext _db;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(MySqlDbContext db,ILogger<UsuarioRepository> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        /// <summary>
        /// Lista todos los usuarios.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await this._db.Usuarios.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Inserta los datos de un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<RequestUsuario> InsertarUsuario(RequestUsuario usuario)
        {
            try
            {
                await this._db.Usuarios.AddAsync(new Usuario()
                {
                    IdUsuario = System.Guid.NewGuid().ToString(),
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Cedula = usuario.Cedula,
                    Ruc = usuario.RUC,
                    Sexo = usuario.Sexo,
                    Email = usuario.Email,
                    Direccion = usuario.Direccion,
                    Perfil = usuario.Perfil,
                    Password = Seguridad.Seguridad.Encriptar(usuario.Password),
                    FechaCreacion = System.DateTime.Now,
                    EstadoUsuario = 1f
                }) ;

                await this._db.SaveChangesAsync();

            }catch (Exception){
                throw;
            }
            return usuario;
        }

        /// <summary>
        /// Actualiza los datos de un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task ActualizarUsuario(Usuario usuario)
        {
            try
            {
                var actualizar = await this._db.Usuarios.Where(x => x.IdUsuario.Equals(usuario.IdUsuario)).FirstOrDefaultAsync();

                actualizar.Nombres = usuario.Nombres;
                actualizar.Apellidos = usuario.Apellidos;
                actualizar.Cedula = usuario.Cedula;
                actualizar.Ruc = usuario.Ruc;
                actualizar.Sexo = usuario.Sexo;
                actualizar.Email = usuario.Email;
                actualizar.Direccion = usuario.Direccion;
                actualizar.Perfil = usuario.Perfil;
                actualizar.Password = Seguridad.Seguridad.Encriptar(usuario.Password);
                actualizar.FechaCreacion = usuario.FechaCreacion;
                actualizar.EstadoUsuario = usuario.EstadoUsuario;

                await this._db.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Elimina un usuario de la base de datos.
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public async Task EliminarUsuario(string IdUsuario)
        {
            try
            {
                var eliminar = await this._db.Usuarios.Where(x => x.IdUsuario.Equals(IdUsuario)).FirstOrDefaultAsync();

                _db.Remove(eliminar);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Obtiene el listado de un usuario específico.
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public async Task<Usuario> ObtenerUsuario(string IdUsuario)
        {
            return await this._db.Usuarios.Where(x => x.IdUsuario.Equals(IdUsuario)).AsNoTracking().FirstOrDefaultAsync();

        }

        /// <summary>
        /// Verifica si las credenciales ingresadas por el usuario se encuentran registradas para poder loguearse en el sistema.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<string> Login(string Email, string Password)
        {
            try
            {
                var v_existe = await this._db.Usuarios.Where(x => x.Email.Equals(Email) && x.Password == Seguridad.Seguridad.Encriptar(Password)).AsNoTracking().CountAsync();

                if (v_existe > 0)
                {
                    return "OK";
                }

                return "NO_OK";
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
