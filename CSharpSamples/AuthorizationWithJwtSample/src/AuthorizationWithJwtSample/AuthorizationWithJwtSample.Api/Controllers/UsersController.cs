using AuthorizationWithJwtSample.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationWithJwtSample.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet]
    [Authorize]
    public IActionResult GetAllUsers()
    {
        return Ok(_userRepository.GetAll());
    }
}
