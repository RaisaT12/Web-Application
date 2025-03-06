using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Entities;


namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDBContext dbContext;
        public ProductsController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var ProductModel = new ProductViewModel();
            var categoryList = await dbContext.Categories.ToListAsync();

            ProductModel.CategoryViewModelList = categoryList.Select(a => new SelectListItem()
            {
                Value = a.ID.ToString(),
                Text = a.Name
            }).ToList();

            return View(ProductModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel viewModel)
        {
            var category = await dbContext.Categories.FindAsync(viewModel.CategoryId);
            var product = new Product()
            {

                Name = viewModel.Name,
                Description = viewModel.Description,
                CreationDate = viewModel.CreationDate,
                Quantity = viewModel.Quantity,
                Price = viewModel.Price,
                Category = category
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            TempData["AlertMessage"] = "Product Added Successfully";

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var product = await dbContext.Products
                .Include(p => p.Category)
                .ToListAsync();
            return View(product);

            //Lazy Loading
            //Eager Loading
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product=await dbContext.Products.FindAsync(id);
            var categoryList = await dbContext.Categories.ToListAsync();

            var productVM = new ProductViewModel();
            productVM.Id = product.Id;
            productVM.Name = product.Name;
            productVM.Description = product.Description;
            productVM.Price = product.Price;
            productVM.Quantity = product.Quantity;
            productVM.CategoryId = product.Category.ID;
            productVM.CategoryViewModelList = categoryList.Select(a => new SelectListItem()
            {
                Value = a.ID.ToString(),
                Text = a.Name,
                Selected = a.ID == product.Category.ID
            }).ToList();

            return View(productVM);

        }
        [HttpPost]
        public async Task<IActionResult>Edit(ProductViewModel viewModel)
        {
            var product = await dbContext.Products.FindAsync(viewModel.Id);
            var categoryItem = await dbContext.Categories.FindAsync(viewModel.CategoryId);

            if (product is not null)
            {
                product.Name= viewModel.Name;
                product.Description= viewModel.Description;
                product.CreationDate= viewModel.CreationDate;
                product.Quantity= viewModel.Quantity;
                product.Price= viewModel.Price;
                product.Category = categoryItem;
                
                await dbContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Product Updated Succesfully";
            }
            return RedirectToAction("List", "Products");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel viewModel)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (product is not null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Product Deleted Successfully";
            }
            else
            {
                TempData["AlertMessage"] = "Product not found!";
            }
            return RedirectToAction("List", "Products");
        }
   


    }
}
