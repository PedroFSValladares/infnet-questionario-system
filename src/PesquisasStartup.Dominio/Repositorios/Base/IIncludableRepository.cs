namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IIncludableRepository<TEntity>
{
    public Task SalvarAsync(TEntity entity);
}