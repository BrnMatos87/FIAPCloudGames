using Core.Enum;

namespace Core.Input
{
    public class UsuarioUpdateInput
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
