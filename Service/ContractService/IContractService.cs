using airtext_api.Models;
using airtext_api.Dtos;

namespace airtext_api.Service.ContractService;

public interface IContractService
{
    Task<Contract> AddAsync(ContractDto contractDto);
    Task<List<Contract>> GetAllAsync();
}