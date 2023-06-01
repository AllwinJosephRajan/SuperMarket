using Microsoft.EntityFrameworkCore;
using SuperMarket.Models;

namespace SuperMarket.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Created a constructor to Inject necessary dependencies into the ApplicationDbContext class.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<SMarketModel> SMarket { get; set; }    
    }
}
