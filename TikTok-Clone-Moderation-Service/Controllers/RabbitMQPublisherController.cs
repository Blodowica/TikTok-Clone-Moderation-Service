using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TikTok_Clone_Moderation_Service.Services;

namespace TikTok_Clone_Moderation_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQPublisherController : ControllerBase
    {
        private readonly IRabbitMQPublisherService rabbitMQPublisherService;
        public RabbitMQPublisherController(IRabbitMQPublisherService rabbitMQPublisherService)
        {
            this.rabbitMQPublisherService = rabbitMQPublisherService;
            
        }


        [HttpPost]
        public IActionResult SendVideoMessage(string videoMessage)
        {
            rabbitMQPublisherService.sendVideoMessage(videoMessage);
            return Ok();
        }

    }
}
