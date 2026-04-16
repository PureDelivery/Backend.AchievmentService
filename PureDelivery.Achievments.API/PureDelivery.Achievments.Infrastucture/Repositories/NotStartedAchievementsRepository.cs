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
    public class NotStartedAchievementsRepository(AchievmentsDbContext achievmentsDbContext) : INotStartedAchievementsRepository
    {
        public async Task<List<AchievmentDefinition>> GetByUserIdAsync(Guid userId)
        {

            var finishedIds = await achievmentsDbContext.UserAchievements
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AchievmentId)
                .ToListAsync();

            var activeIds = await achievmentsDbContext.AchievmentUserProgresses
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.AchievmentId)
                .ToListAsync();

            var achievementsToStart = await achievmentsDbContext.AchievmentDefinitions
                .Where(a => !finishedIds.Contains(a.Id) && !activeIds.Contains(a.Id))
                .ToListAsync();

            return achievementsToStart;
        }
    }
}
