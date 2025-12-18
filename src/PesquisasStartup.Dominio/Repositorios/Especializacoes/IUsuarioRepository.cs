using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Repositorios.Base;

namespace PesquisasStartup.Dominio.Repositorios.Especializacoes;

public interface IUsuarioRepository : IIncludableRepository<Pessoa>, IUpdatebleRepository<Pessoa>, IQueryableRepository<Guid, Pessoa>
{
    
}