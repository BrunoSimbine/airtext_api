using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Data;
using airtext_api.Models;

namespace airtext_api.Repository.ContractRepository;

public class ContractRepository : IContractRepository
{
	private readonly DataContext _context;

	public ContractRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<Contract> AddAsync(Contract contract)
	{
		_context.Contracts.Add(contract);
		await _context.SaveChangesAsync();
		return contract;
	}


	public async Task<Contract> GetAsync(Guid id)
	{
		var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.Id == id);
		return contract;
	}

	public async Task<List<Contract>> GetByRoleAsync(Guid roleId)
	{
		var contracts = await _context.Contracts.Where(x => x.RoleId == roleId).ToListAsync();
		return contracts;
	}

	public async Task<List<Contract>> GetByUserAsync(Guid userId)
	{
		var contracts = await _context.Contracts.Where(x => x.UserId == userId).ToListAsync();
		return contracts;
	}


	public async Task<List<Contract>> GetAllAsync()
	{
		var contract = await _context.Contracts.Where(x => x.DateDeleted == null).ToListAsync();
		return contract;
	}


}
