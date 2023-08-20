
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Models
{
	public class RegisterModel : PageModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Hash the password
                string salt = Guid.NewGuid().ToString();
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: Password,
                    salt: System.Text.Encoding.UTF8.GetBytes(salt),
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                // Here you can save the user data to the database
                // For example, using Entity Framework or another technology

                // Redirect to the registration success page
                return RedirectToPage("RegistrationSuccess");
            }
            else
            {
                // If the model contains validation errors, return the registration page
                return Page();
            }
        }
    }
}
