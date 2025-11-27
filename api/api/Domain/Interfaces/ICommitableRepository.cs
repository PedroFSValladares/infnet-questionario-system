namespace api.Domain.Interfaces;

public interface ICommitableRepository
{
    public Task CommitChanges();
}