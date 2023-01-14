using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Contracts;

namespace Albert.BackendChallenge.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task<IReadOnlyList<Product>> GetAllProducts();
        Task<Product> CreatProduct(Product product);
        Task<Product> RemoveItemsFromStock(int id, int items);
        Task<Product> AddItemsToStock(int id, int items);
        //Task<bool> DeleteProduct(int id);

    }
}
