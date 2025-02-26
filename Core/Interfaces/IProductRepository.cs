using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllAsync(string? brand, string? type);
        Task<Product?> GetByIdAsync(int id);
        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        bool IsExists(int id);
        Task<bool> SaveChangesAsync();
    }
}
