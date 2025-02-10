using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Data;
using airtext_api.Models;

namespace airtext_api.Repository.UserRepository;

public class UserRepository : IUserRepository
{
	private readonly DataContext _context;
	private readonly IHttpContextAccessor _accessor;

	public UserRepository(DataContext context, IHttpContextAccessor accessor)
	{
		_accessor = accessor;
		_context = context;
	}

	 public Guid GetId()
    {
        var id = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);
        return Guid.Parse(id);
    }

    public async Task<User> GetAsync()
    {
    	var id = GetId();
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

	public async Task<User> AddAsync(User user) 
	{
		_context.Users.Add(user);
		await _context.SaveChangesAsync();
		return user;
	}

	public async Task<User> GetAsync(Guid Id) 
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
		return user;
	}

	public async Task<bool> UserExists(Guid id)
	{
		return await _context.Users.AnyAsync(x => x.Id == id);
	}

	public async Task<bool> NameExists(string username) 
	{
		return await _context.Users.AnyAsync(x => x.Username == username);
	}

	public async Task<bool> EmailExists(string email) 
	{
		return await _context.Users.AnyAsync(x => x.Email == email);
	}

	public async Task<bool> PhoneExists(string phone) 
	{
		return await _context.Users.AnyAsync(x => x.Phone == phone);
	}

	public async Task<bool> IsActivated(Guid Id) 
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
		if (user.Status >= 1)
		{
			return true;
		}else{
			return false;
		}
	}

	public async Task<User> ActivateAsync(Guid Token) 
	{
		var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Token);
		user.Status = 1;
		user.DateUpdated = DateTime.Now;
		await _context.SaveChangesAsync();
		return user;
	}

	public async Task<List<User>> GetAllAsync()
    {
        var users = await _context.Users.Where(a => a.DateDeleted == null).ToListAsync();
        return users;
    }


    public async Task<User> GetByPhone(string phone)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == phone);
        return user;
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<List<User>> GetAll()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<bool> ExistsAnyEmail(string email)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == email);
        return exists;
    }


}
