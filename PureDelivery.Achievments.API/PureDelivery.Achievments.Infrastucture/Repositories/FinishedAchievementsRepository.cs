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
    public class FinishedAchievementsRepository(AchievmentsDbContext achievmentsDbContext) : IFinishedAchievementsRepository
    {
        public async Task AddAsync(UserAchievements achievement)
        {
            achievmentsDbContext.Add(achievement);
            await achievmentsDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid achievementId)
        {
            return await achievmentsDbContext.UserAchievements
                .AnyAsync(ua => ua.UserId == userId && ua.AchievmentId == achievementId);
        }

        public async Task<List<UserAchievements>> GetByUserIdAsync(Guid userId)
        {
            return await achievmentsDbContext.UserAchievements
                .Include(ua => ua.Achievment)
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }
    }
}
