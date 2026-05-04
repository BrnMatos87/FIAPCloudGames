using System.ComponentModel.DataAnnotations;

namespace Core.Input
{
    public class JogoInput
    {
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo de 100 caracteres.")]
        public required string Nome { get; set; }
        [MaxLength(100, ErrorMessage = "A categoria deve ter no máximo de 100 caracteres.")]
        public required string Categoria { get; set; }
        public required decimal Preco { get; set; }
    }
}
