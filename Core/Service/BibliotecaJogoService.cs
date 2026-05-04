using Core.Dto;
using Core.Entity;
using Core.Enum;
using Core.Input;
using Core.Repository;

namespace Core.Service
{
    public class BibliotecaJogoService : IBibliotecaJogoService
    {
        private readonly IBibliotecaJogoRepository _bibliotecaRepository;

        public BibliotecaJogoService(IBibliotecaJogoRepository bibliotecaRepository)
        {
            _bibliotecaRepository = bibliotecaRepository;
        }

        public IList<BibliotecaJogoDto> Listar()
        {
            var bibliotecas = _bibliotecaRepository.ObterTodos()
                .Where(b => b.Status == StatusType.Ativo);

            return bibliotecas.Select(b => new BibliotecaJogoDto
            {
                Id = b.Id,
                UsuarioId = b.UsuarioId,
                JogoId = b.JogoId,
                DataAquisicao = b.DataAquisicao,
                Status = b.Status,
                DataCriacao = b.DataCriacao,
                DataAlteracao = b.DataAlteracao,
                Jogo = b.Jogo == null ? null : new JogoResumoDto
                {
                    Id = b.Jogo.Id,
                    Nome = b.Jogo.Nome,
                    Categoria = b.Jogo.Categoria,
                    Preco = b.Jogo.Preco,
                    PrecoPromocional = b.Jogo.PrecoPromocional,
                    Status = b.Jogo.Status,
                    DataCriacao = b.Jogo.DataCriacao,
                    DataAlteracao = b.Jogo.DataAlteracao
                },
                Usuario = b.Usuario == null ? null : new UsuarioResumoDto
                {
                    Id = b.Usuario.Id,
                    Nome = b.Usuario.Nome,
                    Email = b.Usuario.Email,
                    Perfil = b.Usuario.Perfil,
                    Status = b.Usuario.Status,
                    DataCriacao = b.Usuario.DataCriacao,
                    DataAlteracao = b.Usuario.DataAlteracao
                }
            }).ToList();
        }

        public BibliotecaJogoDto? ObterPorId(int id)
        {
            var b = _bibliotecaRepository.ObterPorId(id);

            if (b == null || b.Status != StatusType.Ativo)
                return null;

            return new BibliotecaJogoDto
            {
                Id = b.Id,
                UsuarioId = b.UsuarioId,
                JogoId = b.JogoId,
                DataAquisicao = b.DataAquisicao,
                Status = b.Status,
                DataCriacao = b.DataCriacao,
                DataAlteracao = b.DataAlteracao,
                Jogo = b.Jogo == null ? null : new JogoResumoDto
                {
                    Id = b.Jogo.Id,
                    Nome = b.Jogo.Nome,
                    Categoria = b.Jogo.Categoria,
                    Preco = b.Jogo.Preco,
                    PrecoPromocional = b.Jogo.PrecoPromocional,
                    Status = b.Jogo.Status,
                    DataCriacao = b.Jogo.DataCriacao,
                    DataAlteracao = b.Jogo.DataAlteracao
                },
                Usuario = b.Usuario == null ? null : new UsuarioResumoDto
                {
                    Id = b.Usuario.Id,
                    Nome = b.Usuario.Nome,
                    Email = b.Usuario.Email,
                    Perfil = b.Usuario.Perfil,
                    Status = b.Usuario.Status,
                    DataCriacao = b.Usuario.DataCriacao,
                    DataAlteracao = b.Usuario.DataAlteracao
                }
            };
        }

        public void Criar(BibliotecaJogoInput input)
        {
            var existe = _bibliotecaRepository.ObterPorUsuarioEJogo(
                input.UsuarioId,
                input.JogoId
            );

            if (existe != null && existe.Status == StatusType.Ativo)
                throw new InvalidOperationException("Este jogo já está na biblioteca do usuário.");

            var biblioteca = BibliotecaJogo.Criar(
                input.UsuarioId,
                input.JogoId,
                input.DataAquisicao
            );

            _bibliotecaRepository.Cadastrar(biblioteca);
        }

        public bool Inativar(int id)
        {
            var biblioteca = _bibliotecaRepository.ObterPorId(id);

            if (biblioteca == null)
                return false;

            biblioteca.Inativar();

            _bibliotecaRepository.Alterar(biblioteca);
            return true;
        }

        public bool Ativar(int id)
        {
            var biblioteca = _bibliotecaRepository.ObterPorId(id);

            if (biblioteca == null)
                return false;

            biblioteca.Ativar();

            _bibliotecaRepository.Alterar(biblioteca);
            return true;
        }
    }
}