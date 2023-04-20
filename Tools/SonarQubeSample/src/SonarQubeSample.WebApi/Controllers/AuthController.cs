using Microsoft.AspNetCore.Mvc;

namespace SonarQubeSample.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(string username, string password, string returnUrl)
        {
            if (username == "admin" && password == "admin")
            {
                return  Redirect(returnUrl);
            }
            return BadRequest();
            
        }
    }
}
