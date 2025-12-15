namespace PesquisasStartup.Dominio.Repositorios.Base;

public interface IDeletableRepository<in TId>
{
    public Task<bool> DeleteAsync(TId id);
}