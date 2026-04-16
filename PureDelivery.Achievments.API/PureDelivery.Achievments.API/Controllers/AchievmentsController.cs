using Microsoft.AspNetCore.Mvc;

namespace PureDelivery.Achievments.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AchievmentsController : ControllerBase
    {
        
        private readonly ILogger<AchievmentsController> _logger;

        public AchievmentsController(ILogger<AchievmentsController> logger)
        {
            _logger = logger;
        }

    }
}
