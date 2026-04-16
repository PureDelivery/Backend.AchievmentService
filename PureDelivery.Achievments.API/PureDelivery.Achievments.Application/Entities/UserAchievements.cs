using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Entities
{
    public class UserAchievements
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AchievmentId { get; set; }
        public AchievmentDefinition Achievment { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
