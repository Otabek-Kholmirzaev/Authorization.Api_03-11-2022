using Authorization.Api.Entities;
using Authorization.Api.Filters;
using Authorization.Api.Models;
using Authorization.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authorization.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfigurationsService _configurationsService;
        private readonly UsersService _usersService; 

        public UsersController(IConfigurationsService configurationsService,
            UsersService usersService)
        {
            _configurationsService = configurationsService;
            _usersService = usersService;
        }

        [HttpPost]
        public IActionResult AddUser(CreateUserModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(createUserModel);
            }

            var userEntity = new UserEntity
            {
                Name = createUserModel.Name,
                Email = createUserModel.Email,
                Roles = createUserModel.Roles,
                Token = Guid.NewGuid().ToString()
            };

            _usersService.AddUser(userEntity);

            return Ok(userEntity);
        }

        [HttpGet]
        [Role("admin")]
        public IActionResult GetAllUsers()
        {
            var users = _usersService.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [Role("admin")]
        public IActionResult GetUserById(int id)
        {
            var user = _usersService.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("get-me")]
        [Role("user,admin")]
        public IActionResult GetMe()
        {
            Claim? email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            return Ok("Email: " + email?.Value);
        }

        [HttpGet("get-me-user")]
        [Role("user")]
        public IActionResult GetMeUser()
        {
            Claim? email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            
            return Ok("UserEmail: " + email?.Value);
        }

        [HttpGet("get-me-admin")]
        [Role("admin")]
        public IActionResult GetMeAmin()
        {
            Claim? name = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            Claim? email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            return Ok("UserName: " + name?.Value + "; UserEmail: " + email?.Value);
        }


        /*
        [HttpGet("FilePathsOptions")]
        public IActionResult GetFilePathsOptions()
        {
            var filePathOptions = _configurationsService.GetFilePaths();

            return Ok(filePathOptions);
        }*/
    }
}
