using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        bool IsExists(int id);
        Task<bool> SaveChangesAsync();
    }
}
