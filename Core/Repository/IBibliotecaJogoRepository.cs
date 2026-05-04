using Core.Entity;

namespace Core.Repository
{
    public interface IBibliotecaJogoRepository : IRepository<BibliotecaJogo>
    {
        BibliotecaJogo? ObterPorUsuarioEJogo(int usuarioId, int jogoId);
    }
}
