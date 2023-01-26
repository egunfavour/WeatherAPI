using Weather_API.Infrastructure.Repositoy;

public interface IUnitOfWork
{
    IAppUserRepository UserRepository { get; }

    Task Commit();
    Task CreateTransaction();
    void Dispose();
    Task Rollback();
    Task Save();
}
