using RegistrationApi12.Interfaces;
using RegistrationApi12.Models;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace RegistrationApi12.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly string _connectionString;

        public RegistrationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Registration>> GetAllAsync()
        {
            var list = new List<Registration>();
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("SELECT * FROM get_all_users()", conn);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Registration
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.GetString(3),
                    Password = reader.GetString(4),
                    CreatedAt = reader.GetDateTime(5)
                });
            }
            return list;
        }

        public async Task<Registration> GetByIdAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("SELECT * FROM get_user_by_id(@_id)", conn);
            cmd.Parameters.AddWithValue("_id", id);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Registration
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.GetString(3),
                    Password = reader.GetString(4),
                    CreatedAt = reader.GetDateTime(5)
                };
            }
            return null;
        }

        public async Task<bool> CreateAsync(Registration reg)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("CALL create_user(@_fullname, @_email, @_phone, @_password)", conn);
            cmd.Parameters.AddWithValue("_fullname", reg.FullName);
            cmd.Parameters.AddWithValue("_email", reg.Email);
            cmd.Parameters.AddWithValue("_phone", reg.Phone);
            cmd.Parameters.AddWithValue("_password", reg.Password);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Registration reg)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("CALL update_user(@_id, @_fullname, @_email, @_phone, @_password)", conn);
            cmd.Parameters.AddWithValue("_id", id);
            cmd.Parameters.AddWithValue("_fullname", reg.FullName);
            cmd.Parameters.AddWithValue("_email", reg.Email);
            cmd.Parameters.AddWithValue("_phone", reg.Phone);
            cmd.Parameters.AddWithValue("_password", reg.Password);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("CALL delete_user(@_id)", conn);
            cmd.Parameters.AddWithValue("_id", id);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
