using ms_usuarios.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ms_usuarios.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ListarUsuarios();

        Task<RequestUsuario> InsertarUsuario(RequestUsuario usuario);

        Task ActualizarUsuario(Usuario usuario);

        Task EliminarUsuario(string IdUsuario);

        Task<Usuario> ObtenerUsuario(string IdUsuario);

        Task<string> Login(string Email, string Password);
    }
}
