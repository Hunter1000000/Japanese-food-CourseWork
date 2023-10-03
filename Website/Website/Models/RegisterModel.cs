
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Укажите логин")]
        [MaxLength(20, ErrorMessage = "Логин должен быть не больше 20 символов")]
        [MinLength(4, ErrorMessage = "Логин должен быть не меньше 4 символов")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль должен быть не меньше чем 6 символов")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
