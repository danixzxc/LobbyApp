using LobbyApp.Models;

namespace LobbyApp.Interfaces
{
    public interface IMessageRepository
    {
        Message GetMessage(int id);

        List<Message> GetMessages();
        bool CreateMessage(Message message);
        bool UpdateMessage(Message message);
        bool DeleteMessage(Message message);

        bool Save();
    }
}
