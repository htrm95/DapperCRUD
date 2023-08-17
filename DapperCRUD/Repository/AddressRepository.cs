using Dapper;
using DapperCRUD.Data;
using DapperCRUD.Models;
using System.Data;

namespace DapperCRUD.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IDapperContext _context;
        public AddressRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task Create(Address _address)
        {
            var query = "INSERT INTO " + typeof(Address).Name + " (AddressName,CoustomerId) VALUES (@AddressName, @CoustomerId)";
            var parameters = new DynamicParameters();
            parameters.Add("AddressName", _address.AddressName, DbType.String);
            parameters.Add("CoustomerId", _address.Customer.Name, DbType.Int64);
          

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(query, parameters);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AddressDto> GetByAddres(int id)
        {
            var query = "select * from Address WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<AddressDto>(query, new { id });
                return result;
            }
        }

        public Task Update(Customer _customer)
        {
            throw new NotImplementedException();
        }
    }
}
