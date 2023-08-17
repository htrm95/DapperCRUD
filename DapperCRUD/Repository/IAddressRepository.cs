using DapperCRUD.Models;

namespace DapperCRUD.Repository
{
    public interface IAddressRepository
    {
        Task<AddressDto> GetByAddres(int id);
        Task Create(Address _address);
        Task Update(Customer _customer);
        Task Delete(int id);
    }
}
