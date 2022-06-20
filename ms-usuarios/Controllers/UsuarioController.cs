using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ms_usuarios.Entities;
using ms_usuarios.Services.V1;
using ms_usuarios.Validador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ms_usuarios.Controllers
{
    [ApiController]
    [Route("v1/api/[Controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioservice;
        private readonly ILogger<Usuario> _logger;

        public UsuarioController(IUsuarioService usuarioService, ILogger<Usuario> logger)
        {
            this._usuarioservice = usuarioService;
            this._logger = logger;
        }

        /// <summary>
        /// Método que obtiene el listado completo de Usuarios.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Solicitud Realizada Correctamente</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="500">Problema Interno de Aplicación</response>
        [HttpGet]
        [Route("listadoUsuarios")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> listadoUsuarios()
        {
            try
            {
                _logger.LogInformation("-- Validando si existen datos");

                var resultado = await this._usuarioservice.ListarUsuarios();

                // Si no hay datos.
                if (resultado.Count.Equals(0))
                {
                    _logger.LogInformation("-- No hay datos {@resultado}", resultado);
                    return NoContent(); //204
                }

                _logger.LogInformation("-- Comercios encontrados exitosamente", resultado);
                return Ok(resultado); //200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "-- Ocurrio un error al intentar obtener el Listado de usuarios");
                return Problem(ex.Message.ToString()); //500

            }

        }

        /// <summary>
        /// Método que inserta un usuario.
        /// </summary>
        /// <response code="201">Insertado Correctamente</response>
        /// <response code="400">Solicitud Incorrecta</response>
        /// <response code="500">Problema Interno de Aplicación</response>
        [HttpPost]
        [Route("InsertarUsuario")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> InsertarUsuario(RequestUsuario usuario, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("-- Validando datos");
                var validator = new UsuarioValidador();
                var result = await validator.ValidateAsync(usuario, cancellationToken);

                foreach (var failure in result.Errors)
                {
                    _logger.LogInformation("-- Errores encontrados {@Errores}", result.Errors);
                    return BadRequest(failure.ErrorMessage); //400
                }

                var resultado = await this._usuarioservice.InsertarUsuario(usuario);
                _logger.LogInformation("-- Usuario insertado correctamente {@resultado}", resultado);
                return Created("", "Usuario insertado correctamente"); //201
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "-- Ocurrio un error al intentar registrar el usuario");
                return Problem(ex.Message); //500
            }

        }

        /// <summary>
        /// Método que actualiza los datos de un usuario.
        /// </summary>-
        /// <response code="200">Actualizado Correctamente</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="500">Problema interno de aplicación</response>
        [HttpPut]
        [Route("ActualizarUsuario")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> ActualizarUsuario(Usuario usuario)
        {
            try
            {
                _logger.LogInformation("-- Validando si existen datos");
                var resultado = await this._usuarioservice.ObtenerUsuario(usuario.IdUsuario);

                // Si no hay datos.
                if (resultado is null)
                {
                    _logger.LogInformation("-- No hay datos {@resultado}", resultado);
                    return NoContent(); // 204
                }

                await this._usuarioservice.ActualizarUsuario(usuario);

                _logger.LogInformation("-- Usuario Actualizado Correctamente", resultado);
                return Ok(); //200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "-- Ocurrio un error al intentar actualizar el usuario");
                return Problem(ex.Message); //500
            }
        }

        /// <summary>
        /// Método que elimina un usuario de la base de datos.
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <response code="200">Eliminado Correctamente</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="500">Problema interno de aplicación</response>
        [HttpDelete]
        [Route("EliminarUsuario")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> EliminarUsuario(string IdUsuario)
        {

            try
            {
                _logger.LogInformation("-- Validando si existen datos");
                var resultado = await this._usuarioservice.ObtenerUsuario(IdUsuario);

                // Si no hay datos.
                if (resultado is null)
                {
                    _logger.LogInformation("-- No hay datos {@resultado}", resultado);
                    return NoContent(); //204
                }

                _usuarioservice.EliminarUsuario(IdUsuario);

                _logger.LogInformation("-- Usuario Eliminado Correctamente", resultado);
                return Ok(); //200

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "-- Ocurrio un error al intentar eliminar el usuario");
                    return Problem(ex.Message); // 500
            }
        }

        /// <summary>
        /// Método que obtiene el listado de un usuario específico.
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        /// <response code="200">Registro Encontrado</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="500">Problema interno de aplicación</response>
        [HttpGet]
        [Route("ObtenerUsuario")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> ObtenerUsuario(string IdUsuario)
        {
            try
            {
                _logger.LogInformation("-- Validando si existen datos");
                var resultado = await this._usuarioservice.ObtenerUsuario(IdUsuario);

                // Si no hay datos.
                if (resultado is null)
                {
                    _logger.LogInformation("-- No hay datos {@resultado}", resultado);
                    return NoContent(); //204
                }

                _logger.LogInformation("-- Usuario Encontrado correctamente", resultado);
                return Ok(resultado); //200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "-- Ocurrio un error al intentar obtener el usuario");
                return Problem(ex.Message); //500
            }

        }

        /// <summary>
        /// Método que verifica si las credenciales ingresadas por el usuario se encuentran registradas para poder loguearse en el sistema.
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        /// <response code="200">Registro Encontrado</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="500">Problema interno de aplicación</response>
        [HttpGet]
        [Route("Login")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            try
            {
                _logger.LogInformation("-- Validando credenciales");
                var resultado = await this._usuarioservice.Login(Email, Password);

                // Si no hay datos.
                if (resultado.Equals("NO_OK"))
                {
                    _logger.LogInformation("-- Las credenciales no coinciden");
                    return NoContent(); //204
                }

                _logger.LogInformation("-- Credenciales correctas", resultado);
                return Ok(resultado); //200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "-- Ocurrio un error con las credenciales");
                return Problem(ex.Message); //500
            }


        }

    }
}
