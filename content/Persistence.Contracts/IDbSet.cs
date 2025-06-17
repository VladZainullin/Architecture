namespace Persistence.Contracts;

public interface IDbSet<TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity> where TEntity : class
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    void Attach(TEntity entity);

    void AttachRanga(IEnumerable<TEntity> entities);
    
    ILocalView<TEntity> Local { get; }
}