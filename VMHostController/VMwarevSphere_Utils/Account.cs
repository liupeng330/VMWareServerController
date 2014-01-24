
namespace VMwarevSphere_Utils
{
    public class VMAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public VMAccount(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
