using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.Entities;
using Hangmo.Server.Services.Interfaces;
using Hangmo.Services.Interfaces;

namespace Hangmo.Server.Services
{
    public class AppUserService
    {

        private IBaseDAO<AppUser> _appUser;
        private IGameService _gameService;
        
        public AppUserService(IBaseDAO<AppUser> appUser, IGameService gameService) 
        { 
            _appUser = appUser;
            _gameService = gameService;
        }

        
    }
}
