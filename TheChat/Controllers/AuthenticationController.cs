using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

using TheChat.Models.DTO;
using TheChat.Models.Entities;
using TheChat.Services.Database.RoleDao;
using TheChat.Services.DataBase.UserDAO;
using TheChat.Services.Authentication;
using TheChat.Services.Hash;
using Microsoft.AspNetCore.Authorization;

namespace TheChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private IUserDao _userDao { get; init; }
        private IRoleDao _roleDao { get; init; }
        private IHashService _hashService { get; init; }
        private IAuthenticationService _authenticationService { get; init; }

        public AuthenticationController(
            IUserDao userDao,
            IRoleDao roleDao,
            IHashService hashService,
            IAuthenticationService authenticationService)
        {
            _userDao = userDao;
            _roleDao = roleDao;
            _hashService = hashService;
            _authenticationService = authenticationService;
        }

        #region Registration

        [HttpPost("Register")]
        public async Task<IActionResult> Register(
                [FromBody] UserRegisterDTO toRegister,
                [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            // Check the validation of request parameters
            if (!ModelState.IsValid)
                return BadRequest();

            Boolean exist;

            // Used insted of BadRequest() to properly generate the responce headers when Error occured 
            var factory = apiBehaviorOptions.Value.InvalidModelStateResponseFactory;

            // Check if there is no users in Database with the same login
            exist = await CheckIfExist(
                predicate: user => user.Login == toRegister.Login,
                key: nameof(toRegister.Login),
                errorMessage: "Login is used"
            );

            if (exist)
                return factory(ControllerContext);

            // Check if there is no users in Database with the same email
            exist = await CheckIfExist(
                predicate: user => user.Email == toRegister.Email,
                key: nameof(toRegister.Email),
                errorMessage: "Email is used"
            );

            if (exist)
                return factory(ControllerContext);

            // Generate new user and add it to database
            User newUser = await AddNewUser(toRegister);

            _authenticationService.Authenticate(newUser);

            return Ok();

        }

        [NonAction] // Used inside class to figure out if there is user that correspondes given predicate already in database
        private async Task<Boolean> CheckIfExist(Expression<Func<User, Boolean>> predicate, String key, String errorMessage)
        {
            User? checkIfExist = await _userDao.GetUserAsync(predicate);

            if (checkIfExist is null) return false;

            ModelState.AddModelError(key, errorMessage); // Adds model error to turn back in responce
            return true;
        }

        [NonAction] // Used inside class to create user entity and add it database
        private async Task<User> AddNewUser(UserRegisterDTO toAdd)
        {
            String salt = _hashService.Hash(_hashService.GenerateSalt());
            String hashedPassword = _hashService.Hash(toAdd.Password + salt);

            Role role = await _roleDao.GetCommonUserRole();

            User newUser = new User()
            {
                Login = toAdd.Login,
                FirstName = toAdd.FirstName,
                SecondName = toAdd.SecondName,
                Email = toAdd.Email,
                Password = hashedPassword,
                Salt = salt,
                CreatedDate = DateTime.Now,
                LastLoginDate = DateTime.Now,
                Role = role
            };

            await _userDao.AddUserAsync(newUser);

            return newUser;
        }

        #endregion

        [Authorize]
        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            // Logout User
            _authenticationService.LogOut();

            return Ok();
        }
    }
}
