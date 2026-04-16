using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Models.Response
{
    public class UserAchievementDto
    {
        public Guid AchievementId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public double LoyaltyPointsReward { get; set; }
        public ProgressInfo? Progress { get; set; }

        public CompletionInfo? Completion { get; set; }
    }

    public class ProgressInfo
    {
        public double Current { get; set; }
        public double Target { get; set; }
        public double Percentage => Current / Target * 100;
    }

    public class CompletionInfo
    {
        public Guid UserAchievementId { get; set; }
        public DateTime UnlockedAt { get; set; }
    }
}
