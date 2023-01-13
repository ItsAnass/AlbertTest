using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Entities.ApplicationDbContext;
using Albert.BackendChallenge.Repository.IRepository;
using AlbertTest.Entities.Identity;
using AlbertTest.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Albert.BackendChallenge.Repository
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly ApplicationDbContext _db;
        private IQueryable<Reservation> _dbSet;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserAccessor _userAccessor;

        public ReservationRepository(ApplicationDbContext db, UserManager<AppUser> user, IUserAccessor userAccessor ) 
        {
            _db = db;
            _dbSet = db.Reservation.AsQueryable()
                .Include(x => x.Product);

            _userAccessor = userAccessor;
            _userManager  = user;

            
        }

        public async Task<bool> CheckQuantity(Product product ,int amount)
        {
            if (amount > product.Stock)
            {
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUser());

                Reservation reservation = new Reservation()
                {
                    Amount= amount,
                    CreatedAt= DateTime.Now,
                    ProductId = product.Id,
                    UserId= user.Id
                                      
                };              
                await CreatReservation(reservation);
                return true;
            }

            return false;
        }

        public async Task<Reservation> CreatReservation(Reservation reservation)
        {
           
            _db.Reservation.Add(reservation);
            await _db.SaveChangesAsync();
           
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == reservation.Id);
              
        }

        public async Task<bool> Delete(int id)
        {
            var reservation = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (reservation == null) return false;

            _db.Remove(reservation);

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<Reservation>> GetAllReservations()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id); 
        }

       

       
    }
}
