using System.Text.Json;
using System.Text.Json.Serialization;

namespace airtext_api.Models;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string DDI { get; set; }
    public string FlagUrl { get; set; }

}
