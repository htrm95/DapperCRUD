using Dapper;
using DapperCRUD.Data;
using DapperCRUD.Models;
using System.Data;
using static Dapper.SqlMapper;

namespace DapperCRUD.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDapperContext _context;
        public CustomerRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {

            var query = "select * from customer";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<CustomerDto>(query);
            return result.ToList();

        }
        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            //var query = "SELECT * FROM " + typeof(BranchDto).Name + " WHERE Id = @Id";
            var query = "select CU.* bankname from Customer as CU  WHERE CU.Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id",id, DbType.Int64);
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<CustomerDto>(query, new { id });
                return result;
            }
        }
        public async Task Create(Customer _Customer)
        {
            var query = "INSERT INTO " + typeof(Customer).Name + " (Name, Family,Phone,Mobile,NationalCode) VALUES (@Name, @Family,@Phone,@Mobile,@NationalCode)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", _Customer.Name, DbType.String);
            parameters.Add("Family", _Customer.Family, DbType.String);
            parameters.Add("Phone", _Customer.Phone, DbType.String);
            parameters.Add("Mobile", _Customer.Mobile, DbType.String);
            parameters.Add("NationalCode", _Customer.NationalCode, DbType.String);

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(query, parameters);

        }
        public async Task Update(Customer _Customer)
        {
            var query = "UPDATE Branch SET Name = @Name, Tel =@Tel    WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", _Customer.Id, DbType.Int64);
            parameters.Add("Name", _Customer.Name, DbType.String);
            parameters.Add("Tel", _Customer.NationalCode, DbType.String);
            parameters.Add("Tel", _Customer.Phone, DbType.String);
            parameters.Add("Tel", _Customer.Mobile, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task Delete(int id)
        {
            var query = "DELETE FROM " + typeof(Customer).Name + " WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

    }
}
