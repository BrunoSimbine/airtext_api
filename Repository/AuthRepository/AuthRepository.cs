using airtext_api.Models;
using airtext_api.Data;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace airtext_api.Repository.AuthRepository;

public class AuthRepository : IAuthRepository
{

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _accessor;

    public AuthRepository(DataContext context, IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _context = context;
    }

    public async Task<Auth> AddAsync(Auth auth)
    {
        _context.Auth.Add(auth);
        await _context.SaveChangesAsync();
        return auth;
    }

    public async Task<Auth> Update(Auth auth)
    {
        _context.Auth.Attach(auth);

        _context.Entry(auth).Property(a => a.Token).IsModified = true;

        await _context.SaveChangesAsync();
        return auth;
    }

    public async Task<Auth> Delete(Auth auth)
    {
        _context.Auth.Attach(auth);

        auth.DateDeleted = DateTime.Now;
        _context.Entry(auth).Property(u => u.DateDeleted).IsModified = true;

        await _context.SaveChangesAsync();
        return auth;
    }

    public async Task<Auth> GetAsync(Guid id)
    {
        var auth = await _context.Auth.FirstOrDefaultAsync(u => u.Id == id);
        return auth;
    }

    public async Task<Auth> GetByToken(string token)
    {
        var auth = await _context.Auth.FirstOrDefaultAsync(u => u.Token == token);
        return auth;
    }

    public async Task<List<Auth>> GetActives(Guid userId)
    {
        var auth = await _context.Auth.Where(a => a.UserId == userId && a.DateDeleted == null).ToListAsync();
        return auth;
    }

    public async Task<List<Auth>> GetAll()
    {
        var auth = await _context.Auth.ToListAsync();
        return auth;
    }

    public async Task<bool> IsDeleted()
    {
        var token = GetCurrentToken();
        var auth = await GetByToken(token);
        if(auth.IsDeleted == true)
        {
            return true; 
        } else {
            return false;
        }
    }


    public string GetCurrentToken()
    {
        var headers = _accessor.HttpContext?.Request.Headers;
        return headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
    }

    public string GetCurrentDevice()
    {
        var headers = _accessor.HttpContext?.Request.Headers;
        return (headers != null) ? headers["User-Agent"].ToString() : "Nothing";
    }

    public string GetIpAddress()
    {

        var forwardedForHeader = _accessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        var clientIp = forwardedForHeader ?? _accessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        return clientIp;
    }


    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA256(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("jwfhgfhjsgdvjhdsg837483hf8743tfg8734gfyegf7634gf38734"));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(claims: claims, expires:DateTime.Now.AddDays(2), signingCredentials:cred);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

}
