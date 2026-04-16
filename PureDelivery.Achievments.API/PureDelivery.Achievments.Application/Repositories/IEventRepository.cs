using PureDelivery.Achievments.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Repositories
{
    public interface IEventRepository
    {
        Task<List<AchievmentDefinition>> GetDefinitionsByCriteriaAsync(string eventCriteria);
    }
}
