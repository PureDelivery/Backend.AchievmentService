using Microsoft.EntityFrameworkCore;
using PureDelivery.Achievments.Application.Entities;
using PureDelivery.Achievments.Application.Repositories;
using PureDelivery.Achievments.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Infrastucture.Repositories
{
    public class ActiveAchievementsRepository(AchievmentsDbContext achievmentsDbContext) : IActiveAchievementsRepository
    {
        public async Task AddAsync(AchievmentUserProgress progress)
        {
            achievmentsDbContext.AchievmentUserProgresses.Add(progress);
            await achievmentsDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AchievmentUserProgress progress)
        {
            achievmentsDbContext.Remove(progress);
            await achievmentsDbContext.SaveChangesAsync();
        }

        public async Task<List<AchievmentUserProgress>> GetByUserIdAsync(Guid userId)
        {
            return await achievmentsDbContext.AchievmentUserProgresses
                .Include(ua => ua.Achievment)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<AchievmentUserProgress?> GetSingle(Guid userId, Guid achievementId)
        {
            return await achievmentsDbContext.AchievmentUserProgresses
                .Include(ua => ua.Achievment)
                .FirstOrDefaultAsync(p => p.UserId == userId && p.AchievmentId == achievementId);
        }

        public async Task UpdateAsync(AchievmentUserProgress progress)
        {
            achievmentsDbContext.AchievmentUserProgresses.Update(progress);
            await achievmentsDbContext.SaveChangesAsync();
        }
    }
}
