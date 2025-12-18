using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Repositorios.Base;

namespace PesquisasStartup.Dominio.Repositorios.Especializacoes;

public interface IPesquisaRepository : 
    IIncludableRepository<Entidades.Pesquisas.Pesquisa>, IDeletableRepository<Pesquisa>,
    IQueryableRepository<Guid, Entidades.Pesquisas.Pesquisa>, IUpdatebleRepository<Entidades.Pesquisas.Pesquisa>,
    IListableRepository<Entidades.Pesquisas.Pesquisa>
{
    
}