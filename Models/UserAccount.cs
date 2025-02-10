using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class UserAccount : BaseEntity
{
    public User User { get; set; }
    public Guid UserId { get; set; }

    public Role Role { get; set; }
    public Guid RoleId { get; set; }
}
