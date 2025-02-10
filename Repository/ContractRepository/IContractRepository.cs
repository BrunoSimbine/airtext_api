using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Models;

namespace airtext_api.Repository.ContractRepository;

public interface IContractRepository : IBaseRepository<Contract>
{
	Task<List<Contract>> GetAllAsync();
	Task<List<Contract>> GetByRoleAsync(Guid roleId);
	Task<List<Contract>> GetByUserAsync(Guid userId);
}