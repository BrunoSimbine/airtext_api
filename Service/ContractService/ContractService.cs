using airtext_api.Models;
using airtext_api.Exceptions;
using airtext_api.Data;
using airtext_api.Dtos;
using airtext_api.Repository.ContractRepository;
using airtext_api.Repository.UserRepository;
using airtext_api.Repository.RoleRepository;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace airtext_api.Service.ContractService;

public class ContractService : IContractService
{

    private readonly IContractRepository _contractRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public ContractService(IContractRepository contractRepository, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _contractRepository = contractRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;

    }

    public async Task<Contract> AddAsync(ContractDto contractDto)
    {
        var user = await _userRepository.GetAsync(contractDto.UserId);
        var role = await _roleRepository.GetAsync(contractDto.RoleId);
        var contract = new Contract
        {
            User = user,
            Role = role
        };

        await _contractRepository.AddAsync(contract);
        return contract;
    }

    public async Task<List<Contract>> GetAllAsync()
    {
        return await _contractRepository.GetAllAsync();
    }
   
}