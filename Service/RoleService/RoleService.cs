using airtext_api.Models;
using airtext_api.Exceptions;
using airtext_api.Data;
using airtext_api.Dtos;
using airtext_api.Repository.RoleRepository;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace airtext_api.Service.RoleService;

public class RoleService : IRoleService
{

    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> AddAsync(RoleDto roleDto)
    {
        if(await _roleRepository.NameExists(roleDto.Name, roleDto.CompanyId))
        {
            throw new NameExistsException("Role name exists");
            return new Role();

        }else{

            var role = new Role
            {
                CompanyId = roleDto.CompanyId,
                Name = roleDto.Name,
                CanSpendBalance = roleDto.CanSpendBalance,
                CanAddBalance = roleDto.CanAddBalance,
                CanAddUser = roleDto.CanAddUser,
                CanRemoveUser = roleDto.CanRemoveUser,
                CanChangeRole = roleDto.CanChangeRole
            };

            await _roleRepository.AddAsync(role);
            return role;
        }

    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _roleRepository.GetAllAsync();
    }
   
}