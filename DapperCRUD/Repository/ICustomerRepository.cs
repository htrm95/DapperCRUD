using DapperCRUD.Models;

namespace DapperCRUD.Repository
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);
        Task Create(Customer _Customer);
        Task Update(Customer _Customer);
        Task Delete(int id);
        Task<List<Address>> GetAddressesAsync(int id);
    }
}
