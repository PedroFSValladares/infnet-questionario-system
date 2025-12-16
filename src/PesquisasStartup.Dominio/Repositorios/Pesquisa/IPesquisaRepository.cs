using PesquisasStartup.Dominio.Repositorios.Base;

namespace PesquisasStartup.Dominio.Repositorios.Pesquisa;

public interface IPesquisaRepository : 
    IIncludableRepository<Entidades.Pesquisa>, IDeletableRepository<Guid>,
    IQueryableRepository<Guid, Entidades.Pesquisa>, IUpdatebleRepository<Entidades.Pesquisa>,
    IListableRepository<Entidades.Pesquisa>
{
    
}