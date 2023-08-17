using DapperCRUD.Models;

namespace DapperCRUD.Repository
{
    public interface IBankRepository
    {
        Task<List<Bank>> GetAllAsync();
        Task<Bank> GetByAddres(int id);
        Task Create(Bank _bank);
        Task Update(Bank bank);
        Task Delete(int id);


    }
}
