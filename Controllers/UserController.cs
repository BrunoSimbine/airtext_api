using Microsoft.AspNetCore.Mvc;
using airtext_api.Models;
using airtext_api.Dtos;
using airtext_api.Exceptions;
using airtext_api.Service.UserService;


namespace airtext_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
    	_userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> AddAsync(UserDto userDto)
    {
        try
        {
            var user = await _userService.AddAsync(userDto);
            return Ok(user);
        }catch (UsernameExistsException ex)
        {
            return BadRequest(new Error { Type = "Username already taken.", Solution = "Insert other username."});
        }catch (PhoneExistsException ex)
        {
            return BadRequest(new Error { Type = "Phone already taken.", Solution = "Insert other phone."});

        }catch (EmailExistsException ex)
        {
            return BadRequest(new Error { Type = "Email already taken.", Solution = "Insert other email."});
        }

    }

    [HttpGet("validation/confirm")]
    public async Task<ActionResult<User>> AddAsync([FromQuery] Guid Token)
    {
        try
        {
            var user = await _userService.ActivateAsync(Token);
            return Ok(user);
        } catch (UserNotFoundException ex)
        {
            return BadRequest(new Error { Type = "User not found.", Solution = "Insert correct validation token"});
        } catch (UserAlreadyActiveException ex)
        {
            return BadRequest(new Error { Type = "User are active.", Solution = "Don't need to validate this user."});
        }

    }
}
