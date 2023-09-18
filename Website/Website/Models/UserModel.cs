using System.ComponentModel.DataAnnotations;
namespace Website.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhotoPath { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCompanyRepresentative { get; set; }
        public UserModel() { }

        public UserModel(string name, string surname, string email, string login, string password, string photopath, bool isAdmin, bool isCompanyRepresentative)
        {
            this.PhotoPath = photopath;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Login = login;
            this.Password = password;
            this.IsAdmin = isAdmin;
            this.IsCompanyRepresentative = isCompanyRepresentative;
        }
    }
}
