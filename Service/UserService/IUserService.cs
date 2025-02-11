using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Models;
using airtext_api.Dtos;

namespace airtext_api.Service.UserService;

public interface IUserService
{
	Task<User> AddAsync(UserDto userDto);
	Task<User> GetAsync(Guid Id);
	Task<User> GetAsync();
	Task<User> ActivateAsync(Guid Token);
	Task<List<User>> GetAllAsync();
	Task<List<Company>> GetCompanyAsync();
}