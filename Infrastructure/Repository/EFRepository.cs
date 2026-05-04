using Core.Entity;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EFRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            entidade.DataCriacao = DateTime.Now;
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void CadastrarEmMassa(IEnumerable<T> entidades)
        {
            var lista = entidades.ToList();

            foreach (var entidade in lista)
                entidade.DataCriacao = DateTime.Now;

            _context.BulkInsert(lista);
        }

        public void Deletar(int id)
        {
            var entidade = ObterPorId(id);

            if (entidade == null)
                throw new Exception($"Registro com Id {id} não encontrado.");

            _dbSet.Remove(entidade);
            _context.SaveChanges();
        }

        public T? ObterPorId(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public IList<T> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}
