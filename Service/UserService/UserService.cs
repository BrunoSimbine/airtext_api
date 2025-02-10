using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using airtext_api.Data;
using airtext_api.Exceptions;
using airtext_api.Models;
using airtext_api.Dtos;
using airtext_api.Repository.UserRepository;
using airtext_api.Repository.AuthRepository;

namespace airtext_api.Service.UserService;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IAuthRepository _authRepository;

	public UserService(IUserRepository userRpository, IAuthRepository authRepository)
	{
		_authRepository = authRepository;
		_userRepository = userRpository;
	}

	public async Task<User> AddAsync(UserDto userDto) 
	{
		if(await _userRepository.NameExists(userDto.Username))
		{
			throw new UsernameExistsException("Username already taken");
			return new User();

		}else if(await _userRepository.EmailExists(userDto.Email))
		{
			throw new EmailExistsException("Email already taken");
			return new User();
		}else if(await _userRepository.PhoneExists(userDto.Phone))
		{
			throw new PhoneExistsException("Phone already taken");
			return new User();
		}else{

			_authRepository.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
			var user = new User { 
				Username = userDto.Username,
				Email = userDto.Email, 
				Phone = userDto.Phone, 
				Country = userDto.Country, 
				DDI = userDto.DDI, 
				PasswordHash = passwordHash, 
				PasswordSalt = passwordSalt
			};

			return await _userRepository.AddAsync(user);
		}

	}

	public async Task<User> GetAsync(Guid Id) 
	{
		if (!await _userRepository.UserExists(Id))
		{
			throw new UserNotFoundException("User not found.");
			return new User();
		} else {
			var user = await _userRepository.GetAsync(Id);
			return user;
		}
	}

	public async Task<User> ActivateAsync(Guid id) 
	{
		if (!await _userRepository.UserExists(id))
		{
			throw new UserNotFoundException("User not found.");
			return new User();

		}else if(await _userRepository.IsActivated(id)) {
			throw new UserAlreadyActiveException("User already active.");
			return new User();

		} else {			
			var user = await _userRepository.ActivateAsync(id);
			return user;
		}
	}
}
