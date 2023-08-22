namespace LobbyApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public LobbyChat LobbyChat { get; set; }
        public AccountChat AccountChat { get; set; }
    }
}
