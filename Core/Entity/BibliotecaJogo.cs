using Core.Enum;

namespace Core.Entity
{
    public class BibliotecaJogo : EntityBase
    {
        public int UsuarioId { get; private set; }
        public int JogoId { get; private set; }
        public DateTime DataAquisicao { get; private set; }

        public virtual Usuario Usuario { get; private set; }
        public virtual Jogo Jogo { get; private set; }

        protected BibliotecaJogo() { }

        public static BibliotecaJogo Criar(int usuarioId, int jogoId, DateTime dataAquisicao)
        {
            if (usuarioId <= 0)
                throw new InvalidOperationException("Usuário inválido.");

            if (jogoId <= 0)
                throw new InvalidOperationException("Jogo inválido.");

            if (dataAquisicao == default)
                throw new InvalidOperationException("Data de aquisição inválida.");

            return new BibliotecaJogo
            {
                UsuarioId = usuarioId,
                JogoId = jogoId,
                DataAquisicao = dataAquisicao,
                Status = StatusType.Ativo,
                DataCriacao = DateTime.UtcNow
            };
        }

        public void Inativar()
        {
            if (Status == StatusType.Inativo)
                throw new InvalidOperationException("Jogo da biblioteca já está inativo.");

            Status = StatusType.Inativo;
            DataAlteracao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            if (Status == StatusType.Ativo)
                throw new InvalidOperationException("Jogo da biblioteca já está ativo.");

            Status = StatusType.Ativo;
            DataAlteracao = DateTime.UtcNow;
        }
    }
}