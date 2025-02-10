using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Models;

namespace airtext_api.Repository.CountryRepository;

public interface ICountryRepository : IBaseRepository<Country>
{
	Task<bool> NameExists(string name);
	Task<bool> CodeExists(string code);
	Task<bool> DDIExists(string ddi);

	Task<bool> AnyAsync(Guid id);

	Task<List<Country>> GetAllAsync();
}