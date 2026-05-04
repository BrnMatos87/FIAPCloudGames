using Core.Dto;
using Core.Input;

namespace Core.Service
{
    public interface IJogoService
    {
        IList<JogoResumoDto> ListarResumo();
        IList<JogoDto> Listar();

        JogoResumoDto? ObterResumoPorId(int id);
        JogoDto? ObterPorId(int id);

        void Criar(JogoInput input);
        bool Atualizar(JogoUpdateInput input);

        bool AplicarPromocao(JogoPromocaoInput input);
        bool RemoverPromocao(int jogoId);

        bool Inativar(int id);
        bool Ativar(int id);
    }
}