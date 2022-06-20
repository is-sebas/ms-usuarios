using ms_usuarios.Entities;
using ms_usuarios.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ms_usuarios.Services.V1
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly MySqlDbContext _db;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="usuarioRepository"></param>
        /// <param name="db"></param>
        public UsuarioService(IUsuarioRepository usuarioRepository, MySqlDbContext db)
        {
            this._usuarioRepository = usuarioRepository;
            this._db = db;
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await this._usuarioRepository.ListarUsuarios();
        }

        public async Task<RequestUsuario> InsertarUsuario(RequestUsuario usuario)
        {
            await this._usuarioRepository.InsertarUsuario(usuario);
            return usuario;
        }

        public async Task ActualizarUsuario(Usuario usuario)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            await this._usuarioRepository.ActualizarUsuario(usuario);

            transaction.CommitAsync();
        }

        public async Task EliminarUsuario(string IdUsuario)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            await this._usuarioRepository.EliminarUsuario(IdUsuario);

            transaction.CommitAsync();
        }

        public async Task<Usuario> ObtenerUsuario(string IdUsuario)
        {
            return await this._usuarioRepository.ObtenerUsuario(IdUsuario);
        }

        public async Task<string> Login(string Email, string Password)
        {
            return await this._usuarioRepository.Login(Email, Password);

        }
    }
}
