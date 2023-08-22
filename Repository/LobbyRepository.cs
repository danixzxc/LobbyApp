using LobbyApp.Data;
using LobbyApp.Dto;
using LobbyApp.Interfaces;
using LobbyApp.Models;

namespace LobbyApp.Repository
{
    public class LobbyRepository : ILobbyRepository
    {
        private readonly DataContext _context;

        public LobbyRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLobby(Lobby lobby)
        {
            _context.Add(lobby);

            return Save();
        }

        public bool DeleteLobby(Lobby lobby)
        {
            _context.Remove(lobby);
            return Save();
        }

        public Lobby GetLobby(int id)
        {
            return _context.Lobbies.Where(l => l.Id == id).FirstOrDefault();
        }

        public Lobby GetLobby(string name)
        {
            return _context.Lobbies.Where(l => l.Name == name).FirstOrDefault();
        }


        public ICollection<Lobby> GetLobbies()
        {
            return _context.Lobbies.OrderBy(p => p.Id).ToList();
        }

        public Lobby GetLobbyTrimToUpper(LobbyDto lobbyCreate)
        {
            return GetLobbies().Where(c => c.Name.Trim().ToUpper() == lobbyCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool LobbyExists(int pokeId)
        {
            return _context.Lobbies.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLobby(Lobby pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }

        public LobbyChat GetChatFromLobby(int lobbyId)
        {
            return _context.Lobbies.Where(l => l.Id == lobbyId).Select(c => c.LobbyChat).FirstOrDefault();
        }
    }
}
