using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using airtext_api.Models;
using airtext_api.Dtos;
using airtext_api.Exceptions;
using airtext_api.Filters;
using airtext_api.Service.UserService;
using airtext_api.Service.AuthService;


namespace airtext_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class AuthController : ControllerBase
{

    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _authService = authService;
        _userService = userService;
    }
    [HttpGet("get"), Authorize]
    [ServiceFilter(typeof(RequireActiveAuthFilter))]
    public async Task<ActionResult<List<Auth>>> Get()
    {
        return Ok(await _authService.GetByUser());
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(AuthDto authDto)
    {
        try{
            var user = await _authService.GetLogin(authDto);
            return Ok(user);

        } catch (UserOrPassInvalidException ex){
            return BadRequest(new {
                type = "authentication",
                error = "Your credencials are invalid!",
                solution = "Use a correct login and passwrord."
            });
        } 

    }


    [HttpPut("refresh"), Authorize]
    [ServiceFilter(typeof(RequireActiveAuthFilter))]
    public ActionResult<User> Geta()
    {
        return Ok("Bruno");
    }

    [HttpDelete("logout"), Authorize]
    [ServiceFilter(typeof(RequireActiveAuthFilter))]
    public async Task<ActionResult<User>> Logout()
    {
        return Ok(await _authService.Logout());
    }
}
