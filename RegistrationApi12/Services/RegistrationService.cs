
using RegistrationApi12.Interfaces;
using RegistrationApi12.Models;
using System.Security.Cryptography;
using System.Text;

namespace RegistrationApi12.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _repository;

        public RegistrationService(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Registration>> GetAllUsersAsync() => await _repository.GetAllAsync();

        public async Task<Registration> GetUserByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<bool> CreateUserAsync(RegistrationRequest req)
        {
            var reg = new Registration
            {
                FullName = req.FullName,
                Email = req.Email,
                Phone = req.Phone,
                Password = HashPassword(req.Password),
                CreatedAt = DateTime.UtcNow
            };
            return await _repository.CreateAsync(reg);
        }

        public async Task<bool> UpdateUserAsync(int id, RegistrationRequest req)
        {
            var reg = new Registration
            {
                FullName = req.FullName,
                Email = req.Email,
                Phone = req.Phone,
                Password = HashPassword(req.Password)
            };
            return await _repository.UpdateAsync(id, reg);
        }

        public async Task<bool> DeleteUserAsync(int id) => await _repository.DeleteAsync(id);

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
