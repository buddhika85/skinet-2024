using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity     // T has to be either BaseEntity or sub class of it
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T?> GetEntityWithSpec(ISpecification<T> specification);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);


        // TResult is different T (entity) type
        Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> specification);
        Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification);


        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        bool IsExists(int id);
        Task<bool> SaveAllAsync();
    }
}
