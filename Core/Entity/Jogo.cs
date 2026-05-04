using Core.Enum;

namespace Core.Entity
{
    public class Jogo : EntityBase
    {
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public decimal Preco { get; private set; }
        public decimal? PrecoPromocional { get; private set; }

        public virtual ICollection<BibliotecaJogo> BibliotecaJogos { get; private set; } = new List<BibliotecaJogo>();

        protected Jogo() { }

        public static Jogo Criar(string nome, string categoria, decimal preco)
        {
            Validar(nome, categoria, preco);

            return new Jogo
            {
                Nome = nome.Trim(),
                Categoria = categoria.Trim(),
                Preco = preco,
                Status = StatusType.Ativo,
                DataCriacao = DateTime.UtcNow
            };
        }

        public void AtualizarDados(string nome, string categoria, decimal preco)
        {
            GarantirAtivo();

            Validar(nome, categoria, preco);

            Nome = nome.Trim();
            Categoria = categoria.Trim();
            Preco = preco;
            DataAlteracao = DateTime.UtcNow;
        }

        public void AplicarPromocao(decimal precoPromocional)
        {
            GarantirAtivo();

            if (precoPromocional < 0)
                throw new InvalidOperationException("Preço promocional não pode ser negativo.");

            if (precoPromocional > Preco)
                throw new InvalidOperationException("Preço promocional não pode ser maior que o preço.");

            PrecoPromocional = precoPromocional;
            DataAlteracao = DateTime.UtcNow;
        }

        public void RemoverPromocao()
        {
            GarantirAtivo();

            if (!PrecoPromocional.HasValue)
                throw new InvalidOperationException("Não existe promoção para remover.");

            PrecoPromocional = null;
            DataAlteracao = DateTime.UtcNow;
        }

        public void Inativar()
        {
            if (Status == StatusType.Inativo)
                throw new InvalidOperationException("Jogo já está inativo.");

            Status = StatusType.Inativo;
            DataAlteracao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            if (Status == StatusType.Ativo)
                throw new InvalidOperationException("Jogo já está ativo.");

            Status = StatusType.Ativo;
            DataAlteracao = DateTime.UtcNow;
        }

        private static void Validar(string nome, string categoria, decimal preco)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new InvalidOperationException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(categoria))
                throw new InvalidOperationException("Categoria é obrigatória.");

            if (preco < 0)
                throw new InvalidOperationException("Preço não pode ser negativo.");
        }

        private void GarantirAtivo()
        {
            if (Status != StatusType.Ativo)
                throw new InvalidOperationException("Jogo inativo.");
        }
    }
}