using Hangmo.Repository.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangmo.Repository.Data.DAO
{
    public class GameDAO : BaseDAO<Game>
    {
        private readonly AppDbContext _context;
        public GameDAO(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Game> GetGameUserActive(string userId)
        {
            var result = await _context.Games.Where(_ => _.AppUserId == userId && _.Status == GameStatus.Started).SingleAsync();

            return result;
        }
    }
}
