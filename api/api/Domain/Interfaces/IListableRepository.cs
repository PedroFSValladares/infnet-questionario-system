namespace api.Domain.Interfaces;

public interface IListableRepository<TEntity>
{
    public Task<List<TEntity>> ListarTodosAsync();
}