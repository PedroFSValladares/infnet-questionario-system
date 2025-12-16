using PesquisasStartup.Dominio.Repositorios.Base;

namespace PesquisasStartup.Dominio.Repositorios.Pesquisa;

public interface IPesquisaRepository : 
    IIncludableRepository<Entidades.Pesquisas.Pesquisa>, IDeletableRepository<Guid>,
    IQueryableRepository<Guid, Entidades.Pesquisas.Pesquisa>, IUpdatebleRepository<Entidades.Pesquisas.Pesquisa>,
    IListableRepository<Entidades.Pesquisas.Pesquisa>
{
    
}