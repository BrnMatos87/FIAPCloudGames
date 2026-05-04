using Core.Enum;

namespace Core.Dto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilUsuario Perfil { get; set; }
        public StatusType Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public ICollection<BibliotecaJogoResumoDto> BibliotecaJogos { get; set; } = new List<BibliotecaJogoResumoDto>();
    }
}
