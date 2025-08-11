using Templify.Domain.Entities;

namespace Templify.Application.Interfaces;

public interface IAppUserRepository
{
    Task<AppUser?> GetByIdAsync(int id);
    Task<AppUser?> GetByIdentityIdAsync(string identityId);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser?> GetByUserNameAsync(string userName);
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<AppUser> AddAsync(AppUser user);
    Task UpdateAsync(AppUser user);
    Task DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}
