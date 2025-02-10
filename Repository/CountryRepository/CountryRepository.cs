using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Data;
using airtext_api.Models;

namespace airtext_api.Repository.CountryRepository;

public class CountryRepository : ICountryRepository
{
	private readonly DataContext _context;

	public CountryRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<Country> AddAsync(Country country)
	{
		return new Country();
	}

	public async Task<Country> GetAsync(Guid id)
	{
		return new Country();
	}
}