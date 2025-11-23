namespace api.Domain.Interfaces;

public interface IDeletableRepository<in TId>
{
    public void Delete(TId id);
}