using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Models;

namespace airtext_api.Repository.UserRepository;

public interface IUserRepository : IBaseRepository<User>
{
	Guid GetId();
	Task<User> GetAsync();
	Task<bool> UserExists(Guid id);
	Task<bool> IsActivated(Guid Id);
	Task<bool> PhoneExists(string phone);
	Task<bool> EmailExists(string email);
	Task<bool> NameExists(string name);
	Task<bool> UsernameExists(string username);

	Task<User> ActivateAsync(Guid Token);
	Task<List<User>> GetAllAsync();
	
	Task<User> GetByPhone(string phone);
	Task<User> GetByEmail(string email);
	Task<User> GetByUsername(string email);


}