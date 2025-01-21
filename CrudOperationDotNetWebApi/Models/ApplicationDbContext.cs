using Microsoft.EntityFrameworkCore;

namespace CrudOperationDotNetWebApi.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {
                
        }
        public DbSet<Brand> Brands { get; set; }
    }
}
