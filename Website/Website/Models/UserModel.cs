namespace Website.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Admin { get; set; }
        public bool Manufacturer { get; set; }

        public string EmailGet()
        {
            return Email;
        }

        public bool PasswordRemove(string password1, string password2)
        {
            if (password1 == password2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
