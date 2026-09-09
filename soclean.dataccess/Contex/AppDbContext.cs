using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using soclean.core.Entities;

namespace soclean.dataccess.Contex;

public class AppDbContext :  IdentityDbContext<AppUser>
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }



    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Subscribe> Subscribe { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }


}
