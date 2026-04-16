using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Infrastucture.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PureDelivery.Achievments.Application.Entities;

    public class UserAchievementsConfiguration : IEntityTypeConfiguration<UserAchievements>
    {
        public void Configure(EntityTypeBuilder<UserAchievements> builder)
        {
            builder.ToTable("UserAchievements");
            builder.HasKey(x => x.Id);

            // В базе храним как datetime2 для точности
            builder.Property(x => x.UpdatedAt).IsRequired();

            // Связь с дефинишеном
            builder.HasOne(x => x.Achievment)
                   .WithMany()
                   .HasForeignKey(x => x.AchievmentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.UserId);
        }
    }
}
