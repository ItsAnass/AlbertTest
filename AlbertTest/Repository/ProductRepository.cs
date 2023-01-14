using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Entities.ApplicationDbContext;
using Albert.BackendChallenge.Repository.IRepository;
using AlbertTest.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Albert.BackendChallenge.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;
        private IQueryable<Product> _dbSet;
        private readonly IEmailSender _emailSender;       
        //private IQueryable<Reservation> _reservationDbSet;
        private readonly IReservationRepository _reservationRepository;
        private readonly UserManager<AppUser> _userManager;


        public ProductRepository(ApplicationDbContext db , IEmailSender emailSender, IReservationRepository reservationRepository, UserManager<AppUser> userManager) 
        {
            _db = db;           
            _emailSender = emailSender;
            //_reservationDbSet = _db.Reservation.AsQueryable();
            _reservationRepository = reservationRepository;
            _userManager = userManager;


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

            var reservations =  _reservationRepository.GetAllReservations().Result.Where(x => x.ProductId == id).Select(x =>x.UserId).ToList();

            foreach ( var item in reservations)
            {
                var usersEmail = _userManager.Users.Where(x => x.Id == item).Select(x => x.Email).ToList();

                foreach (var email in usersEmail)
                {
                    await _emailSender.SendEmailAsync(email, "Hurry up!", "We have added more products from your previous request login and check them");
                }
            }
           
            if (items < 1)
            {
                throw new Exception($"Error value cannot be lower than 1");
            }

            product.Stock += items;

            _db.Update(product);
            _db.SaveChanges();
            return product;


        }

        //public async Task<bool> DeleteProduct(int id)
        //{
        //    var product = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            
        //    if (product == null) return false;
            
        //    _db.Remove(product);

        //    return await _db.SaveChangesAsync() > 0;
        //}

        
    }
}
