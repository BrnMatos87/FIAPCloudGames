using Core.Enum;

namespace Core.Entity
{
    public class Usuario : EntityBase
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public PerfilUsuario Perfil { get; private set; }

        public virtual ICollection<BibliotecaJogo> BibliotecaJogos { get; private set; } = new List<BibliotecaJogo>();

        protected Usuario() { }

        public static Usuario Criar(string nome, string email, string senha, PerfilUsuario perfil)
        {
            ValidarNome(nome);
            ValidarEmail(email);
            ValidarSenha(senha);

            return new Usuario
            {
                Nome = nome.Trim(),
                Email = email.Trim().ToLower(),
                Senha = senha,
                Perfil = perfil,
                Status = StatusType.Ativo,
                DataCriacao = DateTime.UtcNow
            };
        }

        public void AtualizarDados(string nome, string email)
        {
            GarantirAtivo();

            ValidarNome(nome);
            ValidarEmail(email);

            Nome = nome.Trim();
            Email = email.Trim().ToLower();
            DataAlteracao = DateTime.UtcNow;
        }

        public void AlterarPerfil(PerfilUsuario perfil)
        {
            GarantirAtivo();

            Perfil = perfil;
            DataAlteracao = DateTime.UtcNow;
        }

        public void AlterarSenha(string senhaHash)
        {
            GarantirAtivo();

            ValidarSenha(senhaHash);

            Senha = senhaHash;
            DataAlteracao = DateTime.UtcNow;
        }

        public void Inativar()
        {
            if (Status == StatusType.Inativo)
                throw new InvalidOperationException("Usuário já está inativo.");

            Status = StatusType.Inativo;
            DataAlteracao = DateTime.UtcNow;
        }

        public void Ativar()
        {
            if (Status == StatusType.Ativo)
                throw new InvalidOperationException("Usuário já está ativo.");

            Status = StatusType.Ativo;
            DataAlteracao = DateTime.UtcNow;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new InvalidOperationException("Nome é obrigatório.");
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidOperationException("E-mail é obrigatório.");
        }

        private static void ValidarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new InvalidOperationException("Senha é obrigatória.");
        }

        private void GarantirAtivo()
        {
            if (Status != StatusType.Ativo)
                throw new InvalidOperationException("Usuário inativo.");
        }
    }
}