using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Dtos;

public class UserDto
{
	public string Name { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string Phone { get; set; }
	public string Password { get; set; }

	public Guid CountryId { get; set; }

}
