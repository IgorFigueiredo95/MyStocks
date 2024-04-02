using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyStocks.Api.Controllers;

[ApiController]
[Route("[Controller")]
public class AuthController : ControllerBase
{

    [AllowAnonymous]
    [HttpPost]
    [Route("v1/login")]
    public IActionResult UserLogin()
    {

    }

}
