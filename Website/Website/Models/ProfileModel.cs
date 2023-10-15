namespace Website.Models
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }

        public ProfileModel(string name, string surname, string login, string email, string photoPath)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Email = email;
            PhotoPath = photoPath;
        }
    }
}
