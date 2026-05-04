using Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace Core.Input
{
    public class UsuarioInput
    {
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo de 100 caracteres.")]
        public required string Nome { get; set; }
        [MaxLength(100, ErrorMessage = "O e-mail deve ter no máximo de 100 caracteres.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public required string Email { get; set; }
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [RegularExpression(
           @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_]).{8,}$",
           ErrorMessage = "A senha deve conter números, letras e caracteres especiais."
       )]
        [MaxLength(100, ErrorMessage = "A senha deve ter no máximo de 100 caracteres.")]
        public required string Senha { get; set; }
        public required PerfilUsuario Perfil { get; set; }
    }
}
