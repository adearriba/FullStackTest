using FullStack.API.Model;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Data
{
    public class APIDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }

        public APIDbContext(DbContextOptions<APIDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
