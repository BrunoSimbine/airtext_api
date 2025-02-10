using airtext_api.Models;
using airtext_api.Exceptions;
using airtext_api.Data;
using airtext_api.Dtos;
using airtext_api.Repository.CompanyRepository;
using airtext_api.Repository.UserRepository;
using airtext_api.Repository.RoleRepository;
using airtext_api.Repository.ContractRepository;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace airtext_api.Service.CompanyService;

public class CompanyService : ICompanyService
{

    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IContractRepository _contractRepository;

    public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository, IRoleRepository roleRepository, IContractRepository contractRepository)
    {
        _companyRepository = companyRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _contractRepository = contractRepository;

    }

    public async Task<Company> AddAsync(CompanyDto companyDto)
    {
        if(await _companyRepository.NameExists(companyDto.Name))
        {
            throw new NameExistsException("Company name exists");
            return new Company();

        }else if(await _companyRepository.EmailExists(companyDto.Email))
        {
            throw new EmailExistsException("Company email exists.");
            return new Company();
        }else{

            var user = await _userRepository.GetAsync();
            var company = new Company
            {
                Name = companyDto.Name,
                Email = companyDto.Email,
                VAT = companyDto.VAT,
                Address = companyDto.Address
            };
            await _companyRepository.AddAsync(company);


            var ownerRole = new Role
            {
                CompanyId = company.Id,
                Name = "owner",
                CanSpendBalance = true,
                CanAddBalance = true,
                CanAddUser = true,
                CanRemoveUser = true,
                CanChangeRole = true
            };

            var defaultRole = new Role
            {
                CompanyId = company.Id,
                Name = "default",
                CanSpendBalance = true,
                CanAddBalance = false,
                CanAddUser = false,
                CanRemoveUser = false,
                CanChangeRole = false
            };
            await _roleRepository.AddAsync(ownerRole);
            await _roleRepository.AddAsync(defaultRole);


            var contract = new Contract
            {
                User = user,
                Role = ownerRole
            };
            await _contractRepository.AddAsync(contract);

            return company;
        }

    }

    public async Task<List<User>> GetUsersAsync(Guid companyId)
    {
        var roles = await _roleRepository.GetByCompanyAsync(companyId);
        var users = new List<User>();
        foreach (var role in roles)
        {
            var contracts = await _contractRepository.GetByRoleAsync(role.Id);
            foreach (var contract in contracts)
            {
                users.Add(await _userRepository.GetAsync(contract.UserId));
            }
        }

        return users;
    }

    public async Task<List<Role>> GetRolesAsync(Guid companyId)
    {
        var roles = await _roleRepository.GetByCompanyAsync(companyId);
        var myRoles = new List<Role>();
        foreach (var role in roles)
        {
            if(role.Name != "owner")
            {
                myRoles.Add(role);
            }
            
        }
        return myRoles;
    }


    public async Task<List<Company>> GetAllAsync()
    {
        return await _companyRepository.GetAllAsync();
    }
   
}