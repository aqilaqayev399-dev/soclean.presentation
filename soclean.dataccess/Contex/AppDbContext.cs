using Microsoft.EntityFrameworkCore;
using soclean.core.Entities;

namespace soclean.dataccess.Contex;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options): base(options)
    {
        
    }



    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Slider> Sliders { get; set; }


}
