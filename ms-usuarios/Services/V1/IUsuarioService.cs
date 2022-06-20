using ms_usuarios.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ms_usuarios.Services.V1
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ListarUsuarios();

        Task<RequestUsuario> InsertarUsuario(RequestUsuario usuario);

        Task ActualizarUsuario(Usuario usuario);

        Task EliminarUsuario(string IdUsuario);

        Task<Usuario> ObtenerUsuario(string IdUsuario);

        Task<string> Login(string Email, string Password);
    }
}
