using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models.Entities;

namespace WebApp.Models
{
    public class ProductViewModel : Product
    {
        public List<SelectListItem> CategoryViewModelList { get; set; }
        public int CategoryId { get; set; }
    }
}
