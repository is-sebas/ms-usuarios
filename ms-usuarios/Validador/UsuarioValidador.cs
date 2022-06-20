using FluentValidation;
using ms_usuarios.Entities;

namespace ms_usuarios.Validador
{
    public class UsuarioValidador : AbstractValidator<RequestUsuario>
    {
        public UsuarioValidador()
        {

            CascadeMode = CascadeMode.Stop;

            //----------------------------------------------------------------------------------------------------------------\\
            #region Nombres:

            RuleFor(x => x.Nombres)
                .NotNull().WithErrorCode("1").WithMessage("Nombre nulo o vacío")
                .NotEmpty().WithErrorCode("1").WithMessage("Nombre nulo o vacío")
                .MaximumLength(100).WithErrorCode("1").WithMessage("El nombre no pueden superar los 100 caracteres");

            #endregion

            #region Apellidos:

            RuleFor(x => x.Apellidos)
                    .NotNull().WithErrorCode("2").WithMessage("Apellido nulo o vacío")
                    .NotEmpty().WithErrorCode("2").WithMessage("Apellido nulo o vacío")
                    .MaximumLength(100).WithErrorCode("2").WithMessage("El Apellido no puede superar los 100 caracteres");

            #endregion

            #region Cédula:

            RuleFor(x => x.Cedula)
                    .NotNull().WithErrorCode("3").WithMessage("La Cédula es requerido")
                    .NotEmpty().WithErrorCode("3").WithMessage("La Cédula es requerido")
                    .MaximumLength(15).WithErrorCode("3").WithMessage("La Cédula no puede superar los 15 caracteres");

            #endregion

            #region Sexo:

            RuleFor(x => x.Sexo)
                    .NotNull().WithErrorCode("4").WithMessage("El sexo es requerido")
                    .NotEmpty().WithErrorCode("4").WithMessage("El sexo es requerido");

            #endregion

            #region Email:

            RuleFor(x => x.Email)
                    .NotNull().WithErrorCode("5").WithMessage("El email/correo es requerido")
                    .NotEmpty().WithErrorCode("5").WithMessage("El email/correo es requerido")
                    .MaximumLength(70).WithErrorCode("5").WithMessage("El email/correo no puede superar los 70 caracteres");

            #endregion

            #region Dirección:

            RuleFor(x => x.Direccion)
                    .NotNull().WithErrorCode("6").WithMessage("La dirección es requerida")
                    .NotEmpty().WithErrorCode("6").WithMessage("La dirección es requerida")
                    .MaximumLength(100).WithErrorCode("6").WithMessage("La dirección no puede superar los 100 caracteres");

            #endregion

            #region Perfil:

            RuleFor(x => x.Perfil)
                    .NotNull().WithErrorCode("7").WithMessage("El perfil es requerido")
                    .NotEmpty().WithErrorCode("7").WithMessage("El perfil es requerido");

            #endregion

            #region Password:

            RuleFor(x => x.Password)
                    .NotNull().WithErrorCode("8").WithMessage("La contraseña es requerido")
                    .NotEmpty().WithErrorCode("8").WithMessage("La contraseña es requerido")
                    .MaximumLength(80).WithErrorCode("8").WithMessage("La contraseña no puede superar los 80 caracteres");

            #endregion

        }
    }
}
