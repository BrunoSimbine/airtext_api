using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class Role : BaseEntity
{
	[JsonIgnore]
	public Company Company { get; set; }
    public Guid CompanyId { get; set; }

	public string Name { get; set; }
	public bool CanSpendBalance { get; set; }
	public bool CanAddBalance { get; set; }
	public bool CanAddUser { get; set; }
	public bool CanRemoveUser { get; set; }
	public bool CanChangeRole { get; set; }
}
