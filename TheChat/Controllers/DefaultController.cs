using Microsoft.AspNetCore.Mvc;

namespace TheChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : Controller
    {

        [HttpGet]
        public ActionResult<String> Index()
        {
            return Ok("Hello world");
        }


    }

}