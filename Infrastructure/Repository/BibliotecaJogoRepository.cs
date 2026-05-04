using Core.Entity;
using Core.Repository;

namespace Infrastructure.Repository
{
    public class BibliotecaJogoRepository : EFRepository<BibliotecaJogo>, IBibliotecaJogoRepository
    {
        public BibliotecaJogoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public BibliotecaJogo? ObterPorUsuarioEJogo(int usuarioId, int jogoId)
        {
            return _context.BibliotecaJogo
                .FirstOrDefault(x => x.UsuarioId == usuarioId && x.JogoId == jogoId);
        }
    }
}
