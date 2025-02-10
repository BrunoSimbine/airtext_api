using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class Error
{
    public string Type { get; set; }
    public string Solution { get; set; }
}
