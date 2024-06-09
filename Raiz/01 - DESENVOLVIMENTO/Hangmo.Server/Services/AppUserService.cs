using Hangmo.Repository.Data.DAO;
using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.Entities;
using Hangmo.Repository.Services;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;

namespace Hangmo.Server.Services
{
    public class AppUserService : BaseService<AppUser>, IAppUserService
    {
        private AppUserDAO _userDAO;

        public AppUserService(IBaseDAO<AppUser> baseDao, AppUserDAO userDAO) : base(baseDao)
        {
            _userDAO = userDAO;
        }

        public async Task<AppUser> GetAppUser(string id)
        {
            var user = await _userDAO.GetByIdStringAsync(id);

            return user;
        }

        public async Task<bool> UpdateAppUser(AppUser userModel)
        {
            try
            {
                await _userDAO.UpdateAsync(userModel);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
