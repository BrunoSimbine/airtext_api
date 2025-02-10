using airtext_api.Models;
using airtext_api.Exceptions;
using airtext_api.Data;
using airtext_api.Dtos;
using airtext_api.Repository.CountryRepository;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


namespace airtext_api.Service.CountryService;

public class CountryService : ICountryService
{

    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Country> AddAsync(CountryDto countryDto)
    {
        if( await _countryRepository.NameExists(countryDto.Name))
        {
            throw new NameExistsException("This nama are already taken");
            return new Country();

        }else if (await _countryRepository.CodeExists(countryDto.Code))
        {
            throw new CodeExistsException("This Code are already taken");
            return new Country();
        }else if( await _countryRepository.DDIExists(countryDto.DDI))
        {
            throw new DDIExistsException("This DDI are already taken");
            return new Country();
        }else{
            var country = new Country 
            {
                Name = countryDto.Name,
                Code = countryDto.Code,
                DDI = countryDto.DDI
            };

            await _countryRepository.AddAsync(country);
            return country;
        }

    }

    public async Task<List<Country>> GetAllAsync()
    {
        return await _countryRepository.GetAllAsync();
    }

   
}