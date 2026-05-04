using System.ComponentModel.DataAnnotations;

namespace Core.Input
{
    public class UsuarioSenhaUpdateInput
    {
        public required int Id { get; set; }
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [RegularExpression(
          @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_]).{8,}$",
          ErrorMessage = "A senha deve conter números, letras e caracteres especiais."
      )]
        [MaxLength(100, ErrorMessage = "A senha deve ter no máximo de 100 caracteres.")]
        public required string Senha { get; set; }
    }
}
