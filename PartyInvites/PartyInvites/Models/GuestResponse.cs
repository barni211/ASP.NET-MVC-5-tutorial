using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage = "Proszę podać swoje imie i nazwisko.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę podać adres e-mail.")]
        [RegularExpression(".+\\@.+\\..+",
            ErrorMessage = "Proszę podać prawidłowy adres e-mail.")]
        public string email { get; set; }
        [Required(ErrorMessage = "Proszę podać numer telefonu.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Proszę określić, czy weźmiesz udział w wydarzeniu.")]
        public bool? WillAttend { get; set; }
    }
}