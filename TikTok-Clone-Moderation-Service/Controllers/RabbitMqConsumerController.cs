using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TikTok_Clone_Moderation_Service.Services;

namespace TikTok_Clone_Moderation_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqConsumerController : ControllerBase
    {
        private readonly IRabbitMQConsumerService _rabbitMQConsumerService;
        public RabbitMqConsumerController(IRabbitMQConsumerService rabbitMQConsumerService)
        {
            _rabbitMQConsumerService = rabbitMQConsumerService;
            
        }

        [HttpGet("getAllUserMessages")]
        public IActionResult ReadAllUserServiceMessages() 
        {
         var message =  _rabbitMQConsumerService.ReadAllUserServiceMessages("UserPublishQueue");
            return Ok(message);
        
        }
        [HttpGet("getAllVideoMessage")]
        public IActionResult ReadAllVideoServiceMessages()
        {
            var message = _rabbitMQConsumerService.ReadAllVideoServiceMessages("VideoPublishQueue");
            return Ok(message);

        }

    }
}
