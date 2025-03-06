using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models.Entities;

namespace WebApp.Models
{
    public class OrderViewModel:Order
    {
        public List<SelectListItem> ProductViewModelList { get; set; }
        public Guid ProductId { get; set; }
    }
}
