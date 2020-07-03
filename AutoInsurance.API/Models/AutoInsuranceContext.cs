using Microsoft.EntityFrameworkCore;

namespace AutoInsurance.API.Models
{
    public class AutoInsuranceContext : DbContext
    {
        public AutoInsuranceContext(DbContextOptions<AutoInsuranceContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PolicyCoverage>().HasKey(x => new { x.CoverageId, x.PolicyId });
            modelBuilder.Entity<VehicleCoverage>().HasKey(x => new { x.CoverageId, x.VehicleId });

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PolicyCoverage> PolicyCoverage { get; set; }
        public DbSet<VehicleCoverage> VehicleCoverage { get; set; }

    }
}