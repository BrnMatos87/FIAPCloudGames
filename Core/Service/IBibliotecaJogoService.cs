using Core.Dto;
using Core.Input;

namespace Core.Service
{
    public interface IBibliotecaJogoService
    {
        IList<BibliotecaJogoDto> Listar();
        BibliotecaJogoDto? ObterPorId(int id);

        void Criar(BibliotecaJogoInput input);

        bool Inativar(int id);
        bool Ativar(int id);
    }
}