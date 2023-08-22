namespace LobbyApp.Models
{
    public class AccountChat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Account Account { get; set; }
    }
}
