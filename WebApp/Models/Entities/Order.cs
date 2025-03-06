using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        public string Contact_Number { get; set; }
        [Required]
        public Product Product { get; set; }
        public DateTime Order_Placed { get; set; }
        public int Quantity { get; set; }
        public decimal Bill { get; set; }
    }
}
