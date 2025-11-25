namespace api.Domain.Interfaces;

public interface IListableRepository<TEntity>
{
    public List<TEntity> ListarTodos();
}