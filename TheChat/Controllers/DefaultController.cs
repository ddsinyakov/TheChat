using Microsoft.AspNetCore.Mvc;

namespace TheChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public String Index()
        {
            return "Hello world";
        }
    }

}