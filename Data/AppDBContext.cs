using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechSolutions.Web.Models;

namespace TechSolutions.Web.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Address> Addresses => Set<Address>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var entries = ChangeTracker.Entries<Customer>();

    foreach (var entry in entries)
    {
        if (entry.State == EntityState.Added)
        {
            entry.Entity.IsActive = true;
        }

        if (entry.State == EntityState.Modified)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
    }

    return await base.SaveChangesAsync(cancellationToken);
}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasQueryFilter(c => c.IsActive);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers", "Data", table =>
                {
                    table.HasTrigger("TR_Customers_Audit");
                });

                entity.HasKey(c => c.ID);
                entity.HasIndex(c => c.Email).IsUnique();
            });

             modelBuilder.Entity<Address>(entity =>
    {
        entity.ToTable("Addresses", "Data");
        entity.HasKey(a => a.ID);

        entity.HasOne(a => a.Customer)
              .WithMany(c => c.Addresses)
              .HasForeignKey(a => a.CustomerID);
    });
        }
    }
}
