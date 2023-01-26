namespace Weather_API.Infrastructure.Repositoy
{
    public interface IGenericRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        Task DeleteAsync(string id);
        void DeleteRangeAsync(IEnumerable<T> entities);
        void Update(T item);
    }
}