using PureDelivery.Achievments.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Services
{
    public interface IAchievementService
    {
        Task<List<UserAchievementDto>> GetUserActiveAchievementsAsync(Guid userId);
        Task<List<UserAchievementDto>> GetUserFinishedAchievementsAsync(Guid userId);
        Task<List<UserAchievementDto>> GetUserNotStartedAchievementsAsync(Guid userId);
        Task CompleteAchievement(Guid userId, Guid achievementId);
    }
}
