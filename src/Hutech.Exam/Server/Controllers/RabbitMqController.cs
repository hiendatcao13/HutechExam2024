using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController(RabbitMQService rabbitMqService) : Controller
    {
        private readonly RabbitMQService _rabbitMqService = rabbitMqService;

        //[HttpGet("SendMessage")]
        //public async Task<IActionResult> SendMessage([FromQuery] string message)
        //{
        //    try
        //    {
        //        await _rabbitMqService.PublishMessage(message);
        //        return Ok(new { Success = true, Message = "Message sent successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Success = false, Error = ex.Message });
        //    }
        //}
    }
}
