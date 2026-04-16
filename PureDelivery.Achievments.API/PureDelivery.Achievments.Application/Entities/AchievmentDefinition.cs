using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Entities
{
    public class AchievmentDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string IconUrl { get; set; } = null!; // Добавили иконку
        public string EventCriteria { get; set; } = null!;
        public string CalculationType { get; set; } = null!; // "Increment" или "AddValue"
        public double TargetValue { get; set; }
        public double LoyaltyPointsReward { get; set; }
    }
}
