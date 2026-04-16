using PureDelivery.Achievments.Application.Entities;
using PureDelivery.Achievments.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Mappers
{
    public static class AchievementMapper
    {
        public static UserAchievementDto ToDto(this AchievmentUserProgress progress)
        {
            var definition = progress.Achievment;

            if (progress == null || definition == null) return null!;

            return new UserAchievementDto
            {
                AchievementId = definition.Id,
                Name = definition.Name,
                Description = definition.Description,
                LoyaltyPointsReward = definition.LoyaltyPointsReward,
                IsCompleted = false,
                Progress = new ProgressInfo
                {
                    Current = progress.CurrentValue,
                    Target = definition.TargetValue,
                }
            }; 
        }

        public static UserAchievementDto ToDto(this UserAchievements completed)
        {
            var definition = completed.Achievment;

            if (completed == null || definition == null) return null!;

            return new UserAchievementDto
            {
                Completion = new CompletionInfo
                {
                    UnlockedAt = completed.UpdatedAt,
                    UserAchievementId = completed.Id,
                },
                LoyaltyPointsReward = definition.LoyaltyPointsReward,
                AchievementId = definition.Id,
                Name = definition.Name,
                Description = definition.Description,
                IsCompleted = true,
            };
        }

        public static UserAchievementDto ToDto(this AchievmentDefinition definition)
        {
            if (definition == null) return null!;

            return new UserAchievementDto
            {
                AchievementId = definition.Id,
                Name = definition.Name,
                Description = definition.Description,
                LoyaltyPointsReward = definition.LoyaltyPointsReward,
                IsCompleted = false
            };
        }
    }
}
