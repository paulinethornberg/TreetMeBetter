using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models.ViewModels
{
    public class UserCreateVM
    {
        [Required(ErrorMessage = "Enter a user name")]
        [Range(3, 5, ErrorMessage = "Måste vara minst två bokstäver (2-15)")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter an E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You have to enter a password that has at least one capital letter and one alphanumerical sign")]
        [Range(3, 5, ErrorMessage = "Your password must be at least 6 letters (6-20)")]
        public string Password { get; set; }
    }
}
