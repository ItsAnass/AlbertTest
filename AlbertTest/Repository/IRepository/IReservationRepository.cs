using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Contracts;

namespace Albert.BackendChallenge.Repository.IRepository
{
    public interface IReservationRepository
    {
        Task<Reservation> GetReservationById(int id);
        Task<IReadOnlyList<Reservation>> GetAllReservations();
        Task<Reservation> CreatReservation(Reservation product);
        //Task<bool> Delete(int id);
        Task<bool> CheckQuantity(Product produc ,int amount);

    }
}
