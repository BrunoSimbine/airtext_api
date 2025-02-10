using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class Contract : BaseEntity
{
    [JsonIgnore]
    public User User { get; set; }
    public Guid UserId { get; set; }

    [JsonIgnore]
    public Role Role { get; set; }
    public Guid RoleId { get; set; }
}
