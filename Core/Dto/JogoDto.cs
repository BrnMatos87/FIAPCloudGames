using Core.Enum;

namespace Core.Dto
{
    public class JogoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public decimal? PrecoPromocional { get; set; }
        public StatusType Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public ICollection<BibliotecaJogoResumoUsuarioDto> BibliotecaJogos { get; set; } = new List<BibliotecaJogoResumoUsuarioDto>();
    }
}
