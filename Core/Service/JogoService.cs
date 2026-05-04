using Core.Dto;
using Core.Entity;
using Core.Enum;
using Core.Input;
using Core.Repository;

namespace Core.Service
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public IList<JogoResumoDto> ListarResumo()
        {
            var jogos = _jogoRepository.ObterTodos();

            return jogos.Select(jogo => new JogoResumoDto
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Categoria = jogo.Categoria,
                Preco = jogo.Preco,
                PrecoPromocional = jogo.PrecoPromocional,
                Status = jogo.Status,
                DataCriacao = jogo.DataCriacao,
                DataAlteracao = jogo.DataAlteracao
            }).ToList();
        }

        public IList<JogoDto> Listar()
        {
            var jogos = _jogoRepository.ObterTodos();

            return jogos.Select(jogo => new JogoDto
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Categoria = jogo.Categoria,
                Preco = jogo.Preco,
                PrecoPromocional = jogo.PrecoPromocional,
                Status = jogo.Status,
                DataCriacao = jogo.DataCriacao,
                DataAlteracao = jogo.DataAlteracao
            }).ToList();
        }

        public JogoResumoDto? ObterResumoPorId(int id)
        {
            var jogo = _jogoRepository.ObterPorId(id);

            if (jogo == null)
                return null;

            return new JogoResumoDto
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Categoria = jogo.Categoria,
                Preco = jogo.Preco,
                PrecoPromocional = jogo.PrecoPromocional,
                Status = jogo.Status,
                DataCriacao = jogo.DataCriacao,
                DataAlteracao = jogo.DataAlteracao
            };
        }

        public JogoDto? ObterPorId(int id)
        {
            var jogo = _jogoRepository.ObterPorId(id);

            if (jogo == null)
                return null;

            return new JogoDto
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Categoria = jogo.Categoria,
                Preco = jogo.Preco,
                PrecoPromocional = jogo.PrecoPromocional,
                Status = jogo.Status,
                DataCriacao = jogo.DataCriacao,
                DataAlteracao = jogo.DataAlteracao
            };
        }

        public void Criar(JogoInput input)
        {
            var existe = _jogoRepository.ObterPorNome(input.Nome);

            if (existe != null)
                throw new InvalidOperationException("Já existe um jogo com esse nome.");

            var jogo = Jogo.Criar(
                input.Nome,
                input.Categoria,
                input.Preco
            );

            _jogoRepository.Cadastrar(jogo);
        }

        public bool Atualizar(JogoUpdateInput input)
        {
            var jogo = _jogoRepository.ObterPorId(input.Id);

            if (jogo == null)
                return false;

            var existe = _jogoRepository.ObterPorNome(input.Nome);

            if (existe != null && existe.Id != input.Id)
                throw new InvalidOperationException("Já existe um jogo com esse nome.");

            jogo.AtualizarDados(
                input.Nome,
                input.Categoria,
                input.Preco
            );

            _jogoRepository.Alterar(jogo);
            return true;
        }

        public bool AplicarPromocao(JogoPromocaoInput input)
        {
            var jogo = _jogoRepository.ObterPorId(input.JogoId);

            if (jogo == null)
                return false;

            jogo.AplicarPromocao(input.PrecoPromocional);

            _jogoRepository.Alterar(jogo);
            return true;
        }

        public bool RemoverPromocao(int jogoId)
        {
            var jogo = _jogoRepository.ObterPorId(jogoId);

            if (jogo == null)
                return false;

            jogo.RemoverPromocao();

            _jogoRepository.Alterar(jogo);
            return true;
        }

        public bool Inativar(int id)
        {
            var jogo = _jogoRepository.ObterPorId(id);

            if (jogo == null)
                return false;

            jogo.Inativar();

            _jogoRepository.Alterar(jogo);
            return true;
        }

        public bool Ativar(int id)
        {
            var jogo = _jogoRepository.ObterPorId(id);

            if (jogo == null)
                return false;

            jogo.Ativar();

            _jogoRepository.Alterar(jogo);
            return true;
        }
    }
}