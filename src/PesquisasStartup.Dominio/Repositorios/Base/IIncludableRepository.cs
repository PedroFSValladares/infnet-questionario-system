namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IIncludableRepository<TEntity>
{
    public Task<TEntity> SalvarAsync(TEntity entity);
}