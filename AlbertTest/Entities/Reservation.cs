using System;

namespace Albert.BackendChallenge.Entities
{
    public class Reservation : IWithId
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Amount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
