using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Data;
using airtext_api.Models;

namespace airtext_api.Repository.CompanyRepository;

public class CompanyRepository : ICompanyRepository
{
	private readonly DataContext _context;

	public CompanyRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<Company> AddAsync(Company company)
	{
		_context.Company.Add(company);
		await _context.SaveChangesAsync();
		return company;
	}


	public async Task<Company> GetAsync(Guid id)
	{
		var company = await _context.Company.FirstOrDefaultAsync(x => x.Id == id);
		return company;
	}

	public async Task<List<Company>> GetAllAsync()
	{
		var company = await _context.Company.Where(x => x.DateDeleted == null).ToListAsync();
		return company;
	}

	public async Task<bool> NameExists(string name)
	{
		return await _context.Company.AnyAsync(x => x.Name == name);
	}

	public async Task<bool> EmailExists(string email)
	{
		return await _context.Company.AnyAsync(x => x.Email == email);
	}


}
