using LobbyApp.Models;
using LobbyApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LobbyApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
          /*  var lobby = new Lobby()
            {
                Id = 1,
                Name = "Jebus",
                IP = "127.0.0.0.1",
                Port = 8910,
                MaxPlayers = 4,
            };

            var chat1 = new LobbyChat()
            {
                Id = 1,
                Name = "Chat1",
                Messages = new List<Message>()
    {
        new Message() { Text = "hello everyone" },
        new Message() { Text = "are there any admins here?" },
        new Message() { Text = "Hi! i am newbee" },
        new Message() { Text = "Wassup guys" }
    }
            };
            dataContext.AccountChats.Add(chat1);

            var chat2 = new LobbyChat()
            {
                Id = 2,
                Name = "Test Room",
                Messages = new List<Message>()
    {
        new Message() { Text = "hello world!" }
    }
            };
            dataContext.Chats.Add(chat2);

            var account1 = new Account()
            {
                Id = 1,
                Login = "Pikachu",
                Password = "CatsRCool",
                Chat = chat1
            };
            var account2 = new Account()
            {Id = 2,
                Login = "EadorPlayer",
                Password = "password123",
                Chat = chat2
            };

            lobby.Players = new List<Account>() { account1, account2 };
            lobby.Chat = chat2;

            dataContext.Lobbies.Add(lobby);
            dataContext.SaveChanges();*/

        }
    }
}