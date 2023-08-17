using Dapper;
using DapperCRUD.Data;
using DapperCRUD.Models;
using System.Data;
using static Dapper.SqlMapper;

namespace DapperCRUD.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly IDapperContext _context;
        public BankRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<List<Bank>> GetAllAsync()
        {

            var query = "EXEC GetAllBank";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<Bank>(query);
            return result.ToList();

        }
        public async Task Create(Bank _bank)
        {
            var query = "INSERT INTO " + typeof(Bank).Name + " (Name, Tel,Address,Logo) VALUES (@Name, @Tel,@Address,@Logo)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", _bank.Name, DbType.String);
            parameters.Add("Tel", _bank.Tel, DbType.String);
            parameters.Add("Address", _bank.Address, DbType.String);
            parameters.Add("Logo", _bank.Logo, DbType.String);


            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(query, parameters);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Bank> GetByAddres(int id)
        {
            var query = "select *  from Bank  WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Bank>(query, new { id });
                return result;
            }
        }

        public async Task Update(Bank bank)
        {
            var query = "UPDATE Bank SET Name = @Name, Tel =@Tel ,Address =@Address ,Logo =@Logo    WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Name", bank.Name, DbType.String);
            parameters.Add("Tel", bank.Tel, DbType.String);
            parameters.Add("Address", bank.Address, DbType.String);
            parameters.Add("Logo", bank.Logo, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

    }
}
