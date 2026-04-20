using Microsoft.AspNetCore.Mvc;
using PureDelivery.Achievments.Application.Services;

namespace PureDelivery.Achievments.API.Controllers
{
    [ApiController]
    [Route("api/v1/achievements/[controller]")]
    public class AchievmentsController(IAchievementService achievementService, ILogger<AchievmentsController> logger) : ControllerBase
    {
        [HttpGet("active/{userId:guid}")]
        public async Task<IActionResult> GetActive(Guid userId)
        {
            logger.LogInformation("Getting active achievements for user {UserId}", userId);
            var result = await achievementService.GetUserActiveAchievementsAsync(userId);
            return Ok(result);
        }

        [HttpGet("finished/{userId:guid}")]
        public async Task<IActionResult> GetFinished(Guid userId)
        {
            logger.LogInformation("Getting finished achievements for user {UserId}", userId);
            var result = await achievementService.GetUserFinishedAchievementsAsync(userId);
            return Ok(result);
        }

        [HttpGet("not-started/{userId:guid}")]
        public async Task<IActionResult> GetNotStarted(Guid userId)
        {
            logger.LogInformation("Getting not-started achievements for user {UserId}", userId);
            var result = await achievementService.GetUserNotStartedAchievementsAsync(userId);
            return Ok(result);
        }
    }
}
