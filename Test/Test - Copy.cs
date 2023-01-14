using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Entities.ApplicationDbContext;
using Albert.BackendChallenge.Repository;
using Albert.BackendChallenge.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test
{
    //public class Tests
    //{
    //    private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
    //             .UseInMemoryDatabase(databaseName: "ProductTest")
    //             .Options;

    //    ApplicationDbContext context;

    //    ProductRepository productRepository;


    //    //[OneTimeSetUp]
    //    public void SetUp()
    //    {
    //        context = new ApplicationDbContext(dbContextOptions);
    //        context.Database.EnsureCreated();

    //        SeedDatabase();
    //    }

    //    //[OneTimeTearDown]
    //    public void CleanUp()
    //    {
    //        context.Database.EnsureDeleted();
    //    }

    //    private void SeedDatabase()
    //    {
    //        var product = new Product()
    //        {
    //            Id = 1,
    //            Stock = 10,
    //            Name = "T-shirts"
    //        };
    //        context.Product.Add(product);
    //        context.SaveChanges();
    //    }

    //    [Fact]
    //    public async void Test1()
    //    {
    //        var check = productRepository.RemoveItemsFromStock(2, 1);


    //        Assert.Equal("9", check.ToString());


    //    }
    //}
}