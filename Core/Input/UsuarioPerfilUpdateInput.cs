using Core.Enum;

namespace Core.Input
{
    public class UsuarioPerfilUpdateInput
    {
        public int UsuarioId { get; set; }
        public PerfilUsuario Perfil { get; set; }
    }
}
