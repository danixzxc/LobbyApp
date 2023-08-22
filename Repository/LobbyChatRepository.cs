using AutoMapper;
using LobbyApp.Data;
using LobbyApp.Interfaces;
using LobbyApp.Models;

namespace LobbyApp.Repository
{
    public class LobbyChatRepository : ILobbyChatRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LobbyChatRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool ChatExtsts(int id)
        {
            return _context.LobbyChats.Any(c => c.Id == id);
        }

        public bool CreateChat(LobbyChat chat)
        {
            _context.Add(chat);
            return Save();
        }

        public bool DeleteChat(LobbyChat chat)
        {
            _context.Remove(chat);
            return Save();
        }

        public ICollection<Message> GetMessages()
        {
            return _context.Messages.ToList();
        }

        public LobbyChat GetChat(int id)
        {
            return _context.LobbyChats.Where(c => c.Id == id).FirstOrDefault();
        }

        public LobbyChat GetChatByLobby(int lobbyId)
        {
            return _context.Lobbies.Where(l => l.Id == lobbyId).Select(c => c.LobbyChat).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateChat(LobbyChat country)
        {
            _context.Update(country);
            return Save();
        }

        public List<LobbyChat> GetChats()
        {
            return _context.LobbyChats.ToList();
        }
    }
}