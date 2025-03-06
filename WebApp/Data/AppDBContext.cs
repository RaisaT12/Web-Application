using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WebApp.Models.Entities;
namespace WebApp.Data
{
    public class AppDBContext: DbContext
    {
        internal readonly object ProductViewModel;

        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
            
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
