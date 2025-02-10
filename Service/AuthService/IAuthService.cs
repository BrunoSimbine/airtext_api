using airtext_api.Models;
using airtext_api.Dtos;

namespace airtext_api.Service.AuthService;

public interface IAuthService
{
    Task<List<Auth>> GetByUser();
    Task<Auth> GetLogin(AuthDto authDto);
    Task<Auth> Logout();
}