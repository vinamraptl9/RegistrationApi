using RegistrationApi12.Models;

namespace RegistrationApi12.Interfaces
{
    public interface IRegistrationService
    {
        Task<IEnumerable<Registration>> GetAllUsersAsync();
        Task<Registration> GetUserByIdAsync(int id);
        Task<bool> CreateUserAsync(RegistrationRequest request);
        Task<bool> UpdateUserAsync(int id, RegistrationRequest request);
        Task<bool> DeleteUserAsync(int id);
    }
}

