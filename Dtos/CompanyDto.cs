using System.Text.Json;
using System.Text.Json.Serialization;
namespace airtext_api.Dtos;

public class CompanyDto
{
    public string Name { get; set; }
    public string VAT { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
}
