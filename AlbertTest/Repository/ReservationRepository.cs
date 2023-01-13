using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Entities.ApplicationDbContext;
using Albert.BackendChallenge.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Albert.BackendChallenge.Repository
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly ApplicationDbContext _db;
        private IQueryable<Reservation> _dbSet;

        public ReservationRepository(ApplicationDbContext db) 
        {
            _db = db;
            _dbSet = db.Reservation.AsQueryable()
                .Include(x => x.Product);

            
        }

        public async Task<bool> CheckQuantity(Product product ,int amount)
        {
            if (amount > product.Stock)
            {
                Reservation reservation = new Reservation()
                {
                    Amount= amount,
                    CreatedAt= DateTime.Now,
                    ProductId = product.Id
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
