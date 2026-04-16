using PureDelivery.Achievments.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Repositories
{
    public interface IActiveAchievementsRepository
    {
        Task<List<AchievmentUserProgress>> GetByUserIdAsync(Guid userId);

        Task<AchievmentUserProgress?> GetSingle(Guid userId, Guid achievementId);

        Task AddAsync(AchievmentUserProgress progress);
        Task UpdateAsync(AchievmentUserProgress progress);
        Task DeleteAsync(AchievmentUserProgress progress);
    }
}
