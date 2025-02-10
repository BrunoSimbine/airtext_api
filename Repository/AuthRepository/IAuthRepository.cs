using airtext_api.Repository;
using airtext_api.Models;

namespace airtext_api.Repository.AuthRepository;

public interface IAuthRepository : IBaseRepository<Auth>
{
	Task<List<Auth>> GetActives(Guid userId);
	Task<bool> IsDeleted();
	Task<List<Auth>> GetAll();
	Task<Auth> GetByToken(string token);
	string GetCurrentToken();
	string GetCurrentDevice();
	string GetIpAddress();
	void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
	bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
	string CreateToken(User user);
	Task<Auth> Delete(Auth auth);
}
