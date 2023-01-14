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
        public async  void Test()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName : "Product");
            

            var context = new ApplicationDbContext(optionBuilder.Options);

            var repo = new ProductRepository(context);

          var check =  repo.RemoveItemsFromStock(2, 1).Result.Stock;

            Assert.Equal(9, check);

        }
    }
}