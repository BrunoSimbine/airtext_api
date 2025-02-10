using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class User : BaseEntity
{
	public string Username { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Country { get; set; }
	public string DDI { get; set; }
	public string Role { get; set; } = "user";
	public int Status { get; set; }

	[JsonIgnore]
    public byte[] PasswordHash { get; set; }

    [JsonIgnore]
    public byte[] PasswordSalt { get; set; }
}
