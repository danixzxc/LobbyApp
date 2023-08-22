using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace LobbyApp.Models
{
    public class Lobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public int MaxPlayers { get; set; }
        public ICollection<Account> Players { get; set; }
        public LobbyChat LobbyChat { get; set; }
    }
}
