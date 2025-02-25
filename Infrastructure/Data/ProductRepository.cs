using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext storeContext) : IProductRepository
    {
        public void Add(Product product)
        {
            storeContext.Products.Add(product);
        }

        public void Delete(Product product)
        {
            storeContext.Products.Remove(product);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await storeContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await storeContext.Products.FindAsync(id);
        }

        public bool IsExists(int id)
        {
            return storeContext.Products.Any(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await storeContext.SaveChangesAsync() > 0;
        }

        public void Update(Product product)
        {
            storeContext.Entry(product).State = EntityState.Modified;
        }
    }
}
