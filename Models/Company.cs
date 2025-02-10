using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Models;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public double Balance { get; set; }
    public string VAT { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
}
