using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class Account : BaseEntity
{
    public string Name { get; set; }
}
