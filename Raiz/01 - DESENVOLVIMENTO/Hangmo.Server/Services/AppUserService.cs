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
        private IUserGameService _userGameService;
        public AppUserService(IBaseDAO<AppUser> appUser, IGameService gameService, IUserGameService userGameService) 
        { 
            _appUser = appUser;
            _gameService = gameService;
            _userGameService = userGameService;
        }

        
    }
}
