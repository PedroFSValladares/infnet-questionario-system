namespace api.Domain.Interfaces;

public interface IDeletableRepository<in TId>
{
    public bool Delete(TId id);
}