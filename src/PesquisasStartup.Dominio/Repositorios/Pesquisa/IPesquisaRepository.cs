using PesquisasStartup.Dominio.Repositorios.Base;

namespace PesquisasStartup.Dominio.Repositorios.Pesquisa;

public interface IPesquisaRepository : 
    IIncludableRepository<api.Domain.Model.Pesquisa>, IDeletableRepository<Guid>,
    IQueryableRepository<Guid, api.Domain.Model.Pesquisa>, IUpdatebleRepository<api.Domain.Model.Pesquisa>,
    IListableRepository<api.Domain.Model.Pesquisa>
{
    
}