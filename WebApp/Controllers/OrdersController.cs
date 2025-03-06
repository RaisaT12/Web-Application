using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDBContext dbContext;


        public OrdersController(AppDBContext DbContext)
        {
            this.dbContext = DbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var OrderModel = new OrderViewModel();
            var productList = await dbContext.Products.ToListAsync();
            OrderModel.ProductViewModelList = productList.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            return View(OrderModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(OrderViewModel viewModel)
        {
            try
            {
                var product = await dbContext.Products.FindAsync(viewModel.ProductId);
                var order = new Order()
                {
                    Address = viewModel.Address,
                    Name = viewModel.Name,
                    Contact_Number = viewModel.Contact_Number,
                    Product = product,
                    Order_Placed = viewModel.Order_Placed,
                    Quantity = viewModel.Quantity,
                    Bill = product.Price * viewModel.Quantity
                };
                await dbContext.Orders.AddAsync(order);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("List", "Orders");
            }
            catch (Exception ex) {
                throw ex;
            }
            
            return View(viewModel);
 
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var order = await dbContext.Orders.Include(p => p.Product).ToListAsync();
            return View(order);
        }

    }
}
