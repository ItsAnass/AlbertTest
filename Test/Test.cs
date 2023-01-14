using Albert.BackendChallenge.Entities.ApplicationDbContext;
using Albert.BackendChallenge.Repository;
using Albert.BackendChallenge.Repository.IRepository;
using AlbertTest.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test
{
    public class Tests
    {

        [Fact]
        public async  void Subtracting_1_From_RemoveMethod_Test()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName : "Product");
            

            var context = new ApplicationDbContext(optionBuilder.Options);

            var product = new Product()
            {
                Id = 1,
                Stock = 10,
                Name = "T-shirts"
            };

            context.Product.Add(product);
            context.SaveChanges();

            var repo = new ProductRepository(context);

            var check =  repo.RemoveItemsFromStock(1, 1).Result.Stock;

            Assert.Equal(9, check);

        }

        [Fact]
        public async void Subtracting_2_From_RemoveMethod_Test()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Product");


            var context = new ApplicationDbContext(optionBuilder.Options);

            var product = new Product()
            {
                Id = 1,
                Stock = 10,
                Name = "T-shirts"
            };

            context.Product.Add(product);
            context.SaveChanges();

            var repo = new ProductRepository(context);

            var check1 =  repo.RemoveItemsFromStock(1, 1).Result.Stock;
            var check2 = repo.RemoveItemsFromStock(1, 1).Result.Stock;

            Assert.Equal(8, check2);

        }
        
    }
}
