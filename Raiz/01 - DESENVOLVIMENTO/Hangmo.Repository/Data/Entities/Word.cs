using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangmo.Repository.Helpers;


namespace Hangmo.Repository.Data.Entities
{
    public class Word
    {
        public Word() {}
        public Word(string secretWord) {   
            
            SecretWord = CryptHelper.Crypt(secretWord);
            Date = DateTime.UtcNow;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public byte[]? SecretWord { get; set; }
        public DateTime Date { get; set; }
    }

}
