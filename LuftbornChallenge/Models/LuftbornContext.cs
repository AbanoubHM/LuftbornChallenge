using Microsoft.EntityFrameworkCore;

namespace LuftbornChallenge.Models {
    public class LuftbornContext : DbContext {
        public LuftbornContext(DbContextOptions<LuftbornContext> options) : base(options) {
        }

        public DbSet<Employee> Employees { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess) {
            UpdateTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) {
            UpdateTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateTimestamps() {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Employee && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries) {
                ((Employee)entityEntry.Entity).UpdatedDate = DateTimeOffset.UtcNow;

                if (entityEntry.State == EntityState.Added) {
                    ((Employee)entityEntry.Entity).CreatedDate = DateTimeOffset.UtcNow;
                }
            }
        }
    }
}