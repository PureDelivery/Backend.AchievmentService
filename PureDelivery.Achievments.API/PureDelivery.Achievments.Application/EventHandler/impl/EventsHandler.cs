using Microsoft.EntityFrameworkCore;
using PureDelivery.Achievments.Application.Entities;
using PureDelivery.Achievments.Application.Repositories;
using PureDelivery.Achievments.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.EventHandler.impl
{
    public class EventsHandler(
        IEventRepository eventRepository,
        IFinishedAchievementsRepository finishedRepository,
        IActiveAchievementsRepository activeRepository,
        IAchievementService achievementService,
        IUnitOfWork unitOfWork) : IEventHandler
    {
        public async Task HandleEventAsync(Guid userId, string eventCriteria, double value)
        {
            var definitions = await eventRepository.GetDefinitionsByCriteriaAsync(eventCriteria);

            foreach (var def in definitions)
            {
                if (await finishedRepository.ExistsAsync(userId, def.Id)) continue;

                var progress = await activeRepository.GetSingle(userId, def.Id);
                if (progress == null)
                {
                    progress = new AchievmentUserProgress { UserId = userId, AchievmentId = def.Id, CurrentValue = 0 };
                    await activeRepository.AddAsync(progress);
                }

                if (def.CalculationType == "Increment")
                {
                    progress.CurrentValue += 1;
                }
                else if (def.CalculationType == "AddValue")
                {
                    progress.CurrentValue += value;
                }

                if (progress.CurrentValue >= def.TargetValue)
                {
                    await achievementService.CompleteAchievement(userId, def.Id);
                }
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}
