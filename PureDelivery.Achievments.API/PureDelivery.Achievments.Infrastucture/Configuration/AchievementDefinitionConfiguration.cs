namespace PureDelivery.Achievments.Infrastucture.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PureDelivery.Achievments.Application.Entities;

    public class AchievementDefinitionConfiguration : IEntityTypeConfiguration<AchievmentDefinition>
    {
        public void Configure(EntityTypeBuilder<AchievmentDefinition> builder)
        {
            builder.ToTable("AchievementDefinitions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.EventCriteria).IsRequired().HasMaxLength(100);

            builder.Property(x => x.IconUrl).HasMaxLength(255);
            builder.Property(x => x.CalculationType).IsRequired().HasMaxLength(50);
            // Индекс по критерию события, так как мы будем постоянно по нему фильтровать
            builder.HasIndex(x => x.EventCriteria);
        }
    }
}
