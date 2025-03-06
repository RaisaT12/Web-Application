using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.Entities;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDBContext dbContext;

        public CategoriesController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel viewModel)
        {
            var category = new Category()
            {

                Name = viewModel.Name,
                Description = viewModel.Description
            };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            TempData["AlertMessage"] = "Category Added Successfully";
            return RedirectToAction("List", "Categories");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var categoryList = await dbContext.Categories.ToListAsync();
            return View(categoryList);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category viewModel)
        {
            var category = await dbContext.Categories.FindAsync(viewModel.ID);
            if (category is not null)
            {
                category.Name = viewModel.Name;
                category.Description = viewModel.Description;
                await dbContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Category Updated Successfully";
            }
            return RedirectToAction("List", "Categories");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category viewModel)
        {
            var category = await dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == viewModel.ID);
            if (category is not null)
            {
                dbContext.Categories.Remove(viewModel);
                await dbContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Category Deleted Succesfully";
            }
            return RedirectToAction("List", "Categories");
        }
    }
}
