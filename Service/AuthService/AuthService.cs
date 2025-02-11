using airtext_api.Models;
using airtext_api.Exceptions;
using airtext_api.Data;
using airtext_api.Dtos;
using airtext_api.Repository.UserRepository;
using airtext_api.Repository.AuthRepository;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace airtext_api.Service.AuthService;

public class AuthService : IAuthService
{

    private readonly IUserRepository _userRepository;
    private readonly IAuthRepository _authRepository;

    public AuthService(IUserRepository userRepository, IAuthRepository authRepository)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
    }

    public async Task<List<Auth>> GetByUser()
    {
        var userId = _userRepository.GetId();
        var auth = await _authRepository.GetActives(userId);
        return auth;
    }

    public async Task<Auth> GetLogin(AuthDto authDto)
    {
        bool usernameExists = await _userRepository.UsernameExists(authDto.UsernameOrEmail);
        bool emailExists = await _userRepository.EmailExists(authDto.UsernameOrEmail);
        
        if (usernameExists) {

            var user = await _userRepository.GetByUsername(authDto.UsernameOrEmail);
            var device = _authRepository.GetCurrentDevice();
            var auth = new Auth();
            if (_authRepository.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt)){
                var token = _authRepository.CreateToken(user);
                auth.IpAddress = _authRepository.GetIpAddress();
                auth.Device = device;
                auth.Token = token;
                auth.UserId = user.Id;
                await _authRepository.AddAsync(auth);

                return auth;
            } else {
                throw new UserOrPassInvalidException("User or password invalid");
                return auth;
            }
        }else if(emailExists)
        {
            var user = await _userRepository.GetByEmail(authDto.UsernameOrEmail);
            var device = _authRepository.GetCurrentDevice();
            var auth = new Auth();
            if (_authRepository.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt)){
                var token = _authRepository.CreateToken(user);
                auth.IpAddress = _authRepository.GetIpAddress();
                auth.Device = device;
                auth.Token = token;
                auth.UserId = user.Id;
                await _authRepository.AddAsync(auth);

                return auth;
            } else {
                throw new UserOrPassInvalidException("User or password invalid");
                return auth;
            }
        } else {
            throw new UserOrPassInvalidException("User or password invalid");
            return new Auth();
        }
    }


    public async Task<Auth> Logout()
    {
        var token = _authRepository.GetCurrentToken();
        var auth = await _authRepository.GetByToken(token);
        return await _authRepository.Delete(auth);
    }
}