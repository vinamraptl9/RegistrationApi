using RegistrationApi12.Models;

namespace RegistrationApi12.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<IEnumerable<Registration>> GetAllAsync();
        Task<Registration> GetByIdAsync(int id);
        Task<bool> CreateAsync(Registration registration);
        Task<bool> UpdateAsync(int id, Registration registration);
        Task<bool> DeleteAsync(int id);
    }
}
