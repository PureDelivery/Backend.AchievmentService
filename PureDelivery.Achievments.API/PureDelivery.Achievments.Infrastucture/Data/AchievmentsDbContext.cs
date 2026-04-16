using Microsoft.EntityFrameworkCore;
using PureDelivery.Achievments.Application.Entities;

namespace PureDelivery.Achievments.Infrastucture.Data
{
    public class AchievmentsDbContext : DbContext
    {
        public AchievmentsDbContext(DbContextOptions<AchievmentsDbContext> options)
        : base(options)
        {
        }

        public DbSet<AchievmentDefinition> AchievmentDefinitions { get; set; } = null!;
        public DbSet<AchievmentUserProgress> AchievmentUserProgresses { get; set; } = null!;
        public DbSet<UserAchievements> UserAchievements { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AchievmentsDbContext).Assembly);
        }
    }
}
