namespace api.Domain.Interfaces;

public interface IUpdatebleRepository<TEntity> : ICommitableRepository
{
    public Task<TEntity?> AtualizarAsync(TEntity pesquisaDto);
}