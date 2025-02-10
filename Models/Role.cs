using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class Role : BaseEntity
{
	public Account Account { get; set; }
	public Guid AccountId { get; set; }

	public bool CanRead { get; set; }
	public bool CanAddUser { get; set; }
	public bool CanDeleteUser { get; set; }
}
