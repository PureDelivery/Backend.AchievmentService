using PureDelivery.Achievments.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Repositories
{
    public interface IFinishedAchievementsRepository
    {
        Task<List<UserAchievements>> GetByUserIdAsync(Guid userId);
        Task AddAsync(UserAchievements achievement);
        Task<bool> ExistsAsync(Guid userId, Guid achievementId);
    }
}
