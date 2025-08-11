using Templify.Domain.Entities;

namespace Templify.Application.Interfaces.Repositories;

public interface IAppUserRepository : IGenericRepository<AppUser>
{
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser?> GetByUserNameAsync(string userName);
    Task<AppUser?> GetByIdentityIdAsync(string identityId);
}
