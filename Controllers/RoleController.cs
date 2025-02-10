using Microsoft.AspNetCore.Mvc;
using airtext_api.Models;
using airtext_api.Dtos;
using airtext_api.Exceptions;
using airtext_api.Service.RoleService;


namespace airtext_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
    	_roleService = roleService;
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<Role>>> GetAllAsync()
    {
        return Ok(await _roleService.GetAllAsync());
    }

    [HttpPost("register")]
    public async Task<ActionResult<Role>> AddAsync(RoleDto roleDto)
    {
        try
        {
            var role = await _roleService.AddAsync(roleDto);
            return Ok(role);
        }catch (NameExistsException ex)
        {
            return BadRequest(new Error { Type = "Name already taken.", Solution = "Insert other name."});
        }

    }
}
