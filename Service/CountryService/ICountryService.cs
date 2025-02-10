using airtext_api.Models;
using airtext_api.Dtos;

namespace airtext_api.Service.CountryService;

public interface ICountryService
{
    Task<Country> AddAsync(CountryDto countryDto);
    Task<List<Country>> GetAllAsync();
}