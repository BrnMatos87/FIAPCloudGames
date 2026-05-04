using Core.Enum;

namespace Core.Dto
{
    public class BibliotecaJogoResumoUsuarioDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int JogoId { get; set; }
        public DateTime DataAquisicao { get; set; }

        public UsuarioResumoDto? Usuario { get; set; }

        public StatusType Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
