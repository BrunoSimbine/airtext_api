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
		_context.Countries.Add(country);
		await _context.SaveChangesAsync();
		return country;
	}

	public async Task<Country> GetAsync(Guid id)
	{
		var user = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
		return user;
	}

	public async Task<bool> AnyAsync(Guid id)
	{
		return await _context.Countries.AnyAsync(x => x.Id == id);
	}

	public async Task<List<Country>> GetAllAsync()
	{
		var countries = await _context.Countries.Where(x => x.DateDeleted == null).ToListAsync();
		return countries;
	}

	public async Task<bool> NameExists(string name)
	{
		return await _context.Countries.AnyAsync(x => x.Name == name);
	}

	public async Task<bool> CodeExists(string code)
	{
		return await _context.Countries.AnyAsync(x => x.Code == code);
	}

	public async Task<bool> DDIExists(string ddi)
	{
		return await _context.Countries.AnyAsync(x => x.DDI == ddi);
	}
}