using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangmo.Repository.Data.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public byte[]? SecretWord { get; set; }
        public DateTime Date { get; set; }
    }
}
