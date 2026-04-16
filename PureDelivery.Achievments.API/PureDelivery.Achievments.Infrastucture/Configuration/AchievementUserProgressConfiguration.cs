using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PureDelivery.Achievments.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Infrastucture.Configuration
{
    public class AchievementUserProgressConfiguration : IEntityTypeConfiguration<AchievmentUserProgress>
    {
        public void Configure(EntityTypeBuilder<AchievmentUserProgress> builder)
        {
            builder.ToTable("AchievementUserProgress");
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.UserId, x.AchievmentId }).IsUnique();

            builder.HasOne(x => x.Achievment)
                .WithMany()
                .HasForeignKey(x => x.AchievmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
