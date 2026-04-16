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
    public class EventRepository(AchievmentsDbContext achievmentsDbContext) : IEventRepository
    {
        public async Task<List<AchievmentDefinition>> GetDefinitionsByCriteriaAsync(string eventCriteria)
        {
            return await achievmentsDbContext.AchievmentDefinitions
                .Where(ad => ad.EventCriteria == eventCriteria)
                .ToListAsync();
        }
    }
}
