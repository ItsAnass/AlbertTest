using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Entities.ApplicationDbContext;
using Albert.BackendChallenge.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Albert.BackendChallenge.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;
        private IQueryable<Product> _dbSet;

        public ProductRepository(ApplicationDbContext db) 
        {
            _db = db;
            
        }

        public async Task<Product> CreatProduct(Product product)
        {
           
            _db.Product.Add(product);
            await _db.SaveChangesAsync();
           
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == product.Id);
              
        }

        public async Task<IReadOnlyList<Product>> GetAllProducts()
        {           
            return await _db.Product.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _db.Product.FindAsync(id);
        }

        public async Task<Product> RemoveItemsFromStock(int id, int items)
        {
            var product = await _db.Product.FindAsync(id);

            if (product == null) return null;

            if (items > product.Stock)
            {
                throw new Exception($"Tried to remove {items}, which is higher than the current stock: {product.Stock}");
            }

            product.Stock -= items;
            
            _db.Update(product);
            _db.SaveChanges();
            return product;
           

        }

        public async Task<Product> AddItemsToStock(int id, int items)
        {
            var product = await _db.Product.FindAsync(id);

            if (product == null) return null;

            if (items < 1)
            {
                throw new Exception($"Error value cannot be lower than 1");
            }

            product.Stock += items;

            _db.Update(product);
            _db.SaveChanges();
            return product;


        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            
            if (product == null) return false;
            
            _db.Remove(product);

            return await _db.SaveChangesAsync() > 0;
        }

        
    }
}
