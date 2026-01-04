using Microsoft.EntityFrameworkCore;
using GroupFundTracker.Api.Models;

namespace GroupFundTracker.Api.Data
{
    public class GroupFundDbContext : DbContext
    {
        public GroupFundDbContext(DbContextOptions<GroupFundDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<MonthlyContribution> MonthlyContributions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasMany(m => m.MonthlyContributions)
                .WithOne(c => c.Member)
                .HasForeignKey(c => c.MemberId);

            modelBuilder.Entity<MonthlyContribution>()
                .Property(x => x.Amount)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
