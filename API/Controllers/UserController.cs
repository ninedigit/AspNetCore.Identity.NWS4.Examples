using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NineDigit.AspNetCore.Identity.NWS4.Examples.API.Controllers;

public class User
{
    public string Name { get; set; }
}

[Authorize]
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return new[]
        {
            new User { Name = "John" }
        };
    }
}