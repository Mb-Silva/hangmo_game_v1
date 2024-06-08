using Hangmo.Repository.Data.Entities;

namespace Hangmo.Server.Services.Interfaces
{
    public interface IAppUserService
    {
        Task<AppUser> GetAppUser(string id);
    }
}
