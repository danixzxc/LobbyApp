using LobbyApp.Data;
using LobbyApp.Interfaces;
using LobbyApp.Models;

namespace LobbyApp.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private DataContext _context;
        public MessageRepository(DataContext context)
        {
            _context = context;
        }
        public bool MessageExists(int id)
        {
            return _context.Messages.Any(c => c.Id == id);
        }

        public bool CreateMessage(Message message)
        {
            _context.Add(message);
            return Save();
        }

        public bool DeleteMessage(Message message)
        {
            _context.Remove(message);
            return Save();
        }


        public LobbyChat GetLobbyChat(int id)
        {
            return _context.LobbyChats.Where(e => e.Id == id).FirstOrDefault();
        }

        public AccountChat GetAccountChat(int id)
        {
            return _context.AccountChats.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMessage(Message message)
        {
            _context.Update(message);
            return Save();
        }

        public Message GetMessage(int id)
        {
            return _context.Messages.Where(e => e.Id == id).FirstOrDefault();
        }


        public List<Message> GetMessages()
        {
            return _context.Messages.ToList();
        }
    }
}
