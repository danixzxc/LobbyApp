namespace LobbyApp.Models
{
    public class LobbyChat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Message> Messages { get; set; }

        public Lobby Lobby { get; set; }
    }
}
