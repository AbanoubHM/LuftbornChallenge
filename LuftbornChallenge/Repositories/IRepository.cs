using LuftbornChallenge.Helpers;

namespace LuftbornChallenge.Repositories {
    public interface IRepository<T> {
        Task<PagedResult<T>> GetAllPagedAsync(int pageNumber, int pageSize);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task<bool> Exists(int id);
    }
}
