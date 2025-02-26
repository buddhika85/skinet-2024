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

        public async Task<IReadOnlyList<Product>> GetAllAsync(string? brand, string? type, string? sort)
        {
            //return await storeContext.Products.Where(x =>
            //    (brand == null || x.Brand == brand) &&
            //    (type == null || x.Type == type)).ToListAsync();

            var query = storeContext.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(x => x.Brand == brand);
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(x => x.Type == type);
            }
            switch (sort)
            {
                case "priceAsc":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                case "nameAsc":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "nameDesc":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                default:
                    query = query.OrderBy(x => x.Name);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return await storeContext.Products.Select(x => x.Brand).Distinct().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await storeContext.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return await storeContext.Products.Select(x => x.Type).Distinct().ToListAsync();
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
