using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Technic.Web.Data.Base;
using Technic.Web.Data.Entites;

namespace Technic.Web.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null;
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;

        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
