using AutoMapper;
using LobbyApp.Data;
using LobbyApp.Interfaces;
using LobbyApp.Models;

namespace LobbyApp.Repository
{
    public class AccountChatRepository : IAccountChatRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountChatRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool ChatExtsts(int id)
        {
            return _context.AccountChats.Any(c => c.Id == id);
        }

        public bool CreateChat(AccountChat chat)
        {
            _context.Add(chat);
            return Save();
        }

        public bool DeleteChat(AccountChat chat)
        {
            _context.Remove(chat);
            return Save();
        }

        public ICollection<Message> GetMessages()
        {
            return _context.Messages.ToList();
        }

        public AccountChat GetChat(int id)
        {
            return _context.AccountChats.Where(c => c.Id == id).FirstOrDefault();
        }


        public AccountChat GetChatByAccount(int accountId)
        {
            return _context.Accounts.Where(a => a.Id == accountId).Select(c => c.AccountChat).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateChat(AccountChat country)
        {
            _context.Update(country);
            return Save();
        }

        public List<AccountChat> GetChats()
        {
            return _context.AccountChats.ToList();
        }
    }
}