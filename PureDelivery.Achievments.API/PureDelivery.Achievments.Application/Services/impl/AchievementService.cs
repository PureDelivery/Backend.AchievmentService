using MassTransit;
using Microsoft.Extensions.Logging;
using PureDelivery.Achievments.Application.Entities;
using PureDelivery.Achievments.Application.Mappers;
using PureDelivery.Achievments.Application.Models.Response;
using PureDelivery.Achievments.Application.Repositories;
using PureDelivery.Shared.Contracts.Events.Loyalty;
using PureDelivery.Shared.Contracts.Events.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureDelivery.Achievments.Application.Services.impl
{
    public class AchievementService(
        IActiveAchievementsRepository activeAchievementsRepository,
        IFinishedAchievementsRepository finishedAchievementsRepository,
        INotStartedAchievementsRepository notStartedAchievementsRepository,
        IUnitOfWork unitOfWork,
        ILogger<AchievementService> logger,
        IPublishEndpoint publishEndpoint) : IAchievementService
    {
        public async Task CompleteAchievement(Guid userId, Guid achievementId)
        {
            var activeAchievement = await activeAchievementsRepository.GetSingle(userId, achievementId);

            if (activeAchievement == null)
            {
                logger.LogWarning("No active achievement found for user {UserId} and achievement {AchievementId}", userId, achievementId);
                throw new Exception("Active achievement not found for the user.");
            }

            using var transaction = await unitOfWork.BeginTransactionAsync();
            try
            {
                await activeAchievementsRepository.DeleteAsync(activeAchievement);

                var finished = new UserAchievements
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    AchievmentId = achievementId,
                    UpdatedAt = DateTime.UtcNow
                };

                await finishedAchievementsRepository.AddAsync(finished);

                await publishEndpoint.Publish(new AchievementCompletedEvent
                {
                    UserId = userId,
                    AchievementDefinitionId = achievementId,
                    AchievementName = activeAchievement.Achievment.Name,
                    AchievementDescription = activeAchievement.Achievment.Description,
                    LoyaltyPointsBonus = activeAchievement.Achievment.LoyaltyPointsReward
                });


                await unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                logger.LogInformation("User {UserId} unlocked achievement {Id}", userId, achievementId);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Failed to complete achievement for user {UserId}", userId);
                throw;
            }

        }

        public async Task<List<UserAchievementDto>> GetUserNotStartedAchievementsAsync(Guid userId)
        {
            var notStartedAchievements = await notStartedAchievementsRepository.GetByUserIdAsync(userId);

            return notStartedAchievements.Select(a => a.ToDto()).ToList();
        }

        public async Task<List<UserAchievementDto>> GetUserActiveAchievementsAsync(Guid userId)
        {
            var activeAchievements = await activeAchievementsRepository.GetByUserIdAsync(userId);

            return activeAchievements.Select(a => a.ToDto()).ToList();
        }

        public async Task<List<UserAchievementDto>> GetUserFinishedAchievementsAsync(Guid userId)
        {
            var finishedAchievements = await finishedAchievementsRepository.GetByUserIdAsync(userId);

            return finishedAchievements.Select(a => a.ToDto()).ToList();
        }
    }
}
