using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Parola gereklidir")]
        public string Password { get; set; }
    }
}
