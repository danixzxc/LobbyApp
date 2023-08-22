using Microsoft.EntityFrameworkCore;
using LobbyApp.Models;
using System.Diagnostics.Metrics;

namespace LobbyApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<LobbyChat> LobbyChats { get; set; }
        public DbSet<AccountChat> AccountChats { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasOne(a => a.AccountChat)
            .WithOne(a => a.Account)
            .HasForeignKey<AccountChat>(c => c.Id);

            modelBuilder.Entity<Lobby>()
           .HasOne(a => a.LobbyChat)
           .WithOne(a => a.Lobby)
           .HasForeignKey<LobbyChat>(c => c.Id);
        }
    }
}