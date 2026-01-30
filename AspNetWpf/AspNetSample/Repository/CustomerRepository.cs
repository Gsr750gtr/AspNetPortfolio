using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedDTOs.Models;

namespace AspNetSample.Repository
{
    public class CustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<CustomerDto>> GetAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT CustomerCode Code, CustomerName Name, CustomerNameKana NameKana, Prefecture FROM Customers";
            var result = await connection.QueryAsync<CustomerDto>(sql);
            return result.ToList();
        }

        public async Task<CustomerDto?> GetByCodeAsync(string customerCode)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT CustomerCode Code, CustomerName Name, CustomerNameKana NameKana, Prefecture FROM Customers WHERE CustomerCode = @customerCode";
            return await connection.QueryFirstOrDefaultAsync<CustomerDto>(sql, new { customerCode });
        }

        public async Task InsertAsync(CustomerDto customerDto)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = " INSERT INTO Customers(CustomerCode, CustomerName, CustomerNameKana, Prefecture) VALUES(@Code, @Name, @NameKana, @Prefecture)";
            await connection.ExecuteAsync(sql, customerDto);
        }

        public async Task<int> DeleteAsync(string customerCode)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE Customers WHERE CustomerCode = @customerCode";
            return await connection.ExecuteAsync(sql, new { customerCode });
        }
    }
}
