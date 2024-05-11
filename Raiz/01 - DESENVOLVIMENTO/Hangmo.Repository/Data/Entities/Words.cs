using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangmo.Repository.Data.Entities
{
    public class Words
    {
        public int Id { get; set; }
        public byte[] Word { get; set; }
        public DateTime Date { get; set; }
    }
}
