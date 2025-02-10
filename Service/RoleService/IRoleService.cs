using airtext_api.Models;
using airtext_api.Dtos;

namespace airtext_api.Service.RoleService;

public interface IRoleService
{
    Task<Role> AddAsync(RoleDto roleDto);
    Task<List<Role>> GetAllAsync();
}