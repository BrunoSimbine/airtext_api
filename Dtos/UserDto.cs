using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Dtos;

public class UserDto
{
	public string Username { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Country { get; set; }
	public string DDI { get; set; }
	public string Password { get; set; }

}
