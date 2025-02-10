using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Dtos;

public class ContractDto
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
