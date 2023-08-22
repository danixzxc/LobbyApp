namespace LobbyApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public AccountChat AccountChat { get; set; }
    }
}
