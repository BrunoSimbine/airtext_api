using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using airtext_api.Models;
using airtext_api.Dtos;
using airtext_api.Exceptions;
using airtext_api.Filters;
using airtext_api.Service.ContractService;


namespace airtext_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ContractController : ControllerBase
{

    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<Contract>>> GetAllAsync()
    {
        return Ok(await _contractService.GetAllAsync());
    }

    [HttpPost("register")]
    public async Task<ActionResult<Contract>> AddAsync(ContractDto contractDto)
    {
        Console.WriteLine(contractDto.UserId);
        Console.WriteLine(contractDto.RoleId);
        var contract = await _contractService.AddAsync(contractDto);
        return Ok(contract);
    }

}
