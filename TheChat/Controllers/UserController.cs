using Microsoft.AspNetCore.Mvc;
using TheChat.Models.Entities;
using TheChat.Services.DataBase.UserDAO;

namespace TheChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserDao _userDao;
        public UserController(IUserDao userDao)
        {
            _userDao = userDao;
        }

        [HttpGet("/get")]
        public async Task<ActionResult<User>> GetUserWithLogin (String login)
        {
            User? result = await _userDao.GetUserAsync((user) => user.Login == login);

            if (result is null)
                return NotFound("User not found");

            return Ok(result);
        }

        [HttpGet("/add")]
        public async Task<IActionResult> AddUser()
        {
            await _userDao.AddUserAsync(new User()
            {
                FirstName = "Dima",
                SecondName = "Syniakov",
                Login = "Ddisco",
                Email = "d@gmail.com",
                Password = "1234",
                Salt = "1234",
                CreatedDate = DateTime.Now,
                LastLoginDate = DateTime.Now
            });

            return Ok();
        }
    }
}
