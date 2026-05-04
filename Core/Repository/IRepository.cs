using Core.Entity;

namespace Core.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        IList<T> ObterTodos();

        T ObterPorId(int id);

        void Cadastrar(T entidade);
        void CadastrarEmMassa(IEnumerable<T> entidades);

        void Alterar(T entidade);

        void Deletar(int id);
    }
}
