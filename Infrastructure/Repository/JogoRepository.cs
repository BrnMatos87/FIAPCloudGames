using Core.Entity;
using Core.Repository;

namespace Infrastructure.Repository
{
    public class JogoRepository : EFRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Jogo? ObterPorNome(string nome)
        {
            return _context.Jogo
                .FirstOrDefault(x => x.Nome == nome);
        }

    }
}
