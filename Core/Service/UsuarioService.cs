using Core.Dto;
using Core.Entity;
using Core.Enum;
using Core.Input;
using Core.Repository;

namespace Core.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IList<UsuarioResumoDto> ListarResumo()
        {
            var usuarios = _usuarioRepository.ObterTodos()
                .Where(usuario => usuario.Status == StatusType.Ativo);

            return usuarios.Select(usuario => new UsuarioResumoDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Status = usuario.Status,
                DataCriacao = usuario.DataCriacao,
                DataAlteracao = usuario.DataAlteracao
            }).ToList();
        }

        public IList<UsuarioDto> Listar()
        {
            var usuarios = _usuarioRepository.ObterTodos()
                .Where(usuario => usuario.Status == StatusType.Ativo);

            return usuarios.Select(usuario => new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Status = usuario.Status,
                DataCriacao = usuario.DataCriacao,
                DataAlteracao = usuario.DataAlteracao,
                BibliotecaJogos = usuario.BibliotecaJogos?.Select(b => new BibliotecaJogoResumoDto
                {
                    Id = b.Id,
                    UsuarioId = b.UsuarioId,
                    JogoId = b.JogoId,
                    DataAquisicao = b.DataAquisicao,
                    DataCriacao = b.DataCriacao,
                    DataAlteracao = b.DataAlteracao,
                    Jogo = b.Jogo == null ? null : new JogoResumoDto
                    {
                        Id = b.Jogo.Id,
                        Nome = b.Jogo.Nome,
                        Categoria = b.Jogo.Categoria,
                        Preco = b.Jogo.Preco,
                        PrecoPromocional = b.Jogo.PrecoPromocional,
                        DataCriacao = b.Jogo.DataCriacao,
                        DataAlteracao = b.Jogo.DataAlteracao
                    }
                }).ToList() ?? new List<BibliotecaJogoResumoDto>()
            }).ToList();
        }

        public UsuarioResumoDto? ObterResumoPorId(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            if (usuario == null)
                return null;

            return new UsuarioResumoDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Status = usuario.Status,
                DataCriacao = usuario.DataCriacao,
                DataAlteracao = usuario.DataAlteracao
            };
        }

        public UsuarioDto? ObterPorId(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            if (usuario == null)
                return null;

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Status = usuario.Status,
                DataCriacao = usuario.DataCriacao,
                DataAlteracao = usuario.DataAlteracao,
                BibliotecaJogos = usuario.BibliotecaJogos?.Select(b => new BibliotecaJogoResumoDto
                {
                    Id = b.Id,
                    UsuarioId = b.UsuarioId,
                    JogoId = b.JogoId,
                    DataAquisicao = b.DataAquisicao,
                    DataCriacao = b.DataCriacao,
                    DataAlteracao = b.DataAlteracao,
                    Jogo = b.Jogo == null ? null : new JogoResumoDto
                    {
                        Id = b.Jogo.Id,
                        Nome = b.Jogo.Nome,
                        Categoria = b.Jogo.Categoria,
                        Preco = b.Jogo.Preco,
                        PrecoPromocional = b.Jogo.PrecoPromocional,
                        DataCriacao = b.Jogo.DataCriacao,
                        DataAlteracao = b.Jogo.DataAlteracao
                    }
                }).ToList() ?? new List<BibliotecaJogoResumoDto>()
            };
        }

        public void Criar(UsuarioInput input)
        {
            var existe = _usuarioRepository.ObterPorEmail(input.Email);

            if (existe != null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(input.Senha);

            var usuario = Usuario.Criar(
                input.Nome,
                input.Email,
                senhaHash,
                input.Perfil
            );

            _usuarioRepository.Cadastrar(usuario);
        }

        public bool Atualizar(UsuarioUpdateInput input)
        {
            var usuario = _usuarioRepository.ObterPorId(input.Id);

            if (usuario == null)
                return false;

            var existe = _usuarioRepository.ObterPorEmail(input.Email);

            if (existe != null && existe.Id != input.Id)
                throw new InvalidOperationException("E-mail já está em uso.");

            usuario.AtualizarDados(input.Nome, input.Email);

            _usuarioRepository.Alterar(usuario);
            return true;
        }

        public bool AlterarPerfil(UsuarioPerfilUpdateInput input)
        {
            var usuario = _usuarioRepository.ObterPorId(input.UsuarioId);

            if (usuario == null)
                return false;

            usuario.AlterarPerfil(input.Perfil);

            _usuarioRepository.Alterar(usuario);
            return true;
        }

        public bool AlterarSenha(UsuarioSenhaUpdateInput input)
        {
            var usuario = _usuarioRepository.ObterPorId(input.Id);

            if (usuario == null)
                return false;

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(input.Senha);

            usuario.AlterarSenha(senhaHash);

            _usuarioRepository.Alterar(usuario);
            return true;
        }

        public bool Inativar(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            if (usuario == null)
                return false;

            usuario.Inativar();

            _usuarioRepository.Alterar(usuario);
            return true;
        }

        public bool Ativar(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            if (usuario == null)
                return false;

            usuario.Ativar();

            _usuarioRepository.Alterar(usuario);
            return true;
        }
    }
}