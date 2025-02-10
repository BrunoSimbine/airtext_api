using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Models;

namespace airtext_api.Repository.RoleRepository;

public interface IRoleRepository : IBaseRepository<Role>
{
	Task<bool> NameExists(string name, Guid companyId);
	Task<List<Role>> GetByCompanyAsync(Guid companyId);
	Task<List<Role>> GetAllAsync();
}