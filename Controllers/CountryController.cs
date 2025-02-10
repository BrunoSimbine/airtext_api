using Microsoft.AspNetCore.Mvc;
using airtext_api.Models;
using airtext_api.Dtos;
using airtext_api.Exceptions;
using airtext_api.Service.CountryService;


namespace airtext_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
    	_countryService = countryService;
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<Country>>> GetAllAsync()
    {
        return Ok(await _countryService.GetAllAsync());
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> AddAsync(CountryDto countryDto)
    {
        try
        {
            var country = await _countryService.AddAsync(countryDto);
            return Ok(country);
        }catch (NameExistsException ex)
        {
            return BadRequest(new Error { Type = "Name already taken.", Solution = "Insert other name."});
        }catch (CodeExistsException ex)
        {
            return BadRequest(new Error { Type = "Code already taken.", Solution = "Insert other code."});

        }catch (DDIExistsException ex)
        {
            return BadRequest(new Error { Type = "DDI already taken.", Solution = "Insert other DDI."});
        }

    }
}
