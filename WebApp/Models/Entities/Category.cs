using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Entities
{
    public class Category
    {
        
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
