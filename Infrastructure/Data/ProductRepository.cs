using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext storeContext) : IProductRepository
    {
        private readonly StoreContext _storeContext = storeContext;

        public void Add(Product product)
        {
            _storeContext.Products.Add(product);
        }

        public void Delete(Product product)
        {
            _storeContext.Products.Remove(product);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _storeContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _storeContext.Products.FindAsync(id);
        }

        public bool IsExists(int id)
        {
            return _storeContext.Products.Any(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _storeContext.SaveChangesAsync() > 0;
        }

        public void Update(Product product)
        {
            _storeContext.Entry(product).State = EntityState.Modified;
        }
    }
}
