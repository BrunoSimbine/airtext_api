using airtext_api.Models;
using airtext_api.Dtos;

namespace airtext_api.Service.CompanyService;

public interface ICompanyService
{
    Task<Company> AddAsync(CompanyDto companyDto);
    Task<List<User>> GetUsersAsync(Guid companyId);
    Task<List<Role>> GetRolesAsync(Guid companyId);
    Task<List<Company>> GetAllAsync();
}