using Hangmo.Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangmo.Repository.Data.DAO
{
    public class AppUserDAO : BaseDAO<AppUser>
    {
        public AppUserDAO(AppDbContext context) : base(context)
        {
        }
    }
}
