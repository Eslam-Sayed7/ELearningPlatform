using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;


namespace Infrastructure.Base;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : class;

    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

}
