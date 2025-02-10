using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using airtext_api.Models;
using airtext_api.Dtos;
using airtext_api.Exceptions;
using airtext_api.Filters;
using airtext_api.Service.CompanyService;


namespace airtext_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class CompanyController : ControllerBase
{

    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<Company>>> GetAllAsync()
    {
        return Ok(await _companyService.GetAllAsync());
    }

    [HttpGet("get/users/{companyId}")]
    public async Task<ActionResult<List<User>>> GetUsersAsync(Guid companyId)
    {
        return Ok(await _companyService.GetUsersAsync(companyId));
    }

    [HttpGet("get/roles/{companyId}")]
    public async Task<ActionResult<List<Role>>> GetRolesAsync(Guid companyId)
    {
        return Ok(await _companyService.GetRolesAsync(companyId));
    }


    [HttpPost("register"), Authorize]
    public async Task<ActionResult<Company>> AddAsync(CompanyDto companyDto)
    {
        try
        {
            var company = await _companyService.AddAsync(companyDto);
            return Ok(company);
        }catch (NameExistsException ex)
        {
            return BadRequest(new Error { Type = "Name already taken.", Solution = "Insert other name."});
        }catch (EmailExistsException ex)
        {
            return BadRequest(new Error { Type = "Email already taken.", Solution = "Insert other email."});

        }
    }

}
