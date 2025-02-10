using System.Text.Json;
using System.Text.Json.Serialization;

namespace airtext_api.Dtos;

public class CountryDto
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string DDI { get; set; }
}
