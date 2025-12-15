namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IListableRepository<TEntity>
{
    public Task<List<TEntity>> ListarTodosAsync();
}