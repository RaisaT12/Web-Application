namespace WebApp.Models.Entities
{
    public class Product
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Order> Orders { get; set; }


    }
}
