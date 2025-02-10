using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Data;
using airtext_api.Models;

namespace airtext_api.Repository.RoleRepository;

public class RoleRepository : IRoleRepository
{
	private readonly DataContext _context;

	public RoleRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<Role> AddAsync(Role role)
	{
		_context.Roles.Add(role);
		await _context.SaveChangesAsync();
		return role;
	}


	public async Task<Role> GetAsync(Guid id)
	{
		var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
		return role;
	}

	public async Task<List<Role>> GetByCompanyAsync(Guid companyId)
	{
		var roles = await _context.Roles.Where(x => x.CompanyId == companyId).ToListAsync();
		return roles;
	}


	public async Task<List<Role>> GetAllAsync()
	{
		var roles = await _context.Roles.Where(x => x.DateDeleted == null).ToListAsync();
		return roles;
	}

	public async Task<bool> NameExists(string name, Guid companyId)
	{
		return await _context.Roles.AnyAsync(x => x.Name == name && x.Id == companyId);
	}

}
