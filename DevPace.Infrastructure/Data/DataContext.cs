using DevPace.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevPace.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Customer>()
                .HasKey(e => e.Id);

            model.Entity<Customer>()
                .HasIndex(nameof(Customer.Name));

            model.Entity<Customer>()
                .HasIndex(nameof(Customer.Email));

            model.Entity<Customer>()
                .HasIndex(nameof(Customer.PhoneNumber));
        }

    }
}
