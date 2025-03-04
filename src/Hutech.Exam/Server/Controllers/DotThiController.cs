using Hutech.Exam.Server.BUS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hutech.Exam.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DotThiController : Controller
    {
        private readonly DotThiService _dotThiService;

        public DotThiController(DotThiService dotThiService) 
        {
            _dotThiService = dotThiService;
        }

    }
}
