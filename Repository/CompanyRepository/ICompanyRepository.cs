using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Models;

namespace airtext_api.Repository.CompanyRepository;

public interface ICompanyRepository : IBaseRepository<Company>
{
	Task<bool> NameExists(string name);
	Task<bool> EmailExists(string email);
	Task<List<Company>> GetAllAsync();
}