namespace api.Domain.Interfaces;

public interface IDeletableRepository<in TId>
{
    public Task<bool> DeleteAsync(TId id);
}