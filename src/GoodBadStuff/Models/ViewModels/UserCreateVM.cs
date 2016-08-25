using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBadStuff.Models.ViewModels
{
    public class UserCreateVM
    {
        [Required(ErrorMessage = "Du måste ange ett användarnamn")]
        [Range(3, 5, ErrorMessage = "Måste vara minst två bokstäver (2-15)")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Du måste ange en E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Du måste ange ett lösenord som har minst ens tor bokstav och ett alfanumeriskt tecken")]
        [Range(3, 5, ErrorMessage = "Måste vara minst sex bokstäver (6-20)")]
        public string Password { get; set; }
    }
}
