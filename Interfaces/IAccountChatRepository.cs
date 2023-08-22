using LobbyApp.Models;

namespace LobbyApp.Interfaces
{
    public interface IAccountChatRepository
    {
        ICollection<Message> GetMessages();
        List<AccountChat> GetChats();
        AccountChat GetChat(int id);
        AccountChat GetChatByAccount(int accountId);
        bool ChatExtsts(int id);
        bool CreateChat(AccountChat chat);
        bool UpdateChat(AccountChat chat);
        bool DeleteChat(AccountChat chat);
        bool Save();
    }
}