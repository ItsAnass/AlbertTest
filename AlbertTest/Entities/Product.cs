namespace Albert.BackendChallenge.Entities
{
    public class Product : IWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
    }
}
