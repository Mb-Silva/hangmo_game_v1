using Hangmo.Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangmo.Repository.Data.DAO
{
    public class GameDAO : BaseDAO<Game>
    {
        public GameDAO(AppDbContext context) : base(context)
        {
        }
    }
}
