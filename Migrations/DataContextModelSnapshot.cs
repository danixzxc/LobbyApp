﻿// <auto-generated />
using System;
using LobbyApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LobbyApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LobbyApp.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("LobbyId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LobbyId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LobbyApp.Models.AccountChat", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountChats");
                });

            modelBuilder.Entity("LobbyApp.Models.Lobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lobbies");
                });

            modelBuilder.Entity("LobbyApp.Models.LobbyChat", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LobbyChats");
                });

            modelBuilder.Entity("LobbyApp.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountChatId")
                        .HasColumnType("int");

                    b.Property<int>("LobbyChatId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountChatId");

                    b.HasIndex("LobbyChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("LobbyApp.Models.Account", b =>
                {
                    b.HasOne("LobbyApp.Models.Lobby", null)
                        .WithMany("Players")
                        .HasForeignKey("LobbyId");
                });

            modelBuilder.Entity("LobbyApp.Models.AccountChat", b =>
                {
                    b.HasOne("LobbyApp.Models.Account", "Account")
                        .WithOne("AccountChat")
                        .HasForeignKey("LobbyApp.Models.AccountChat", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("LobbyApp.Models.LobbyChat", b =>
                {
                    b.HasOne("LobbyApp.Models.Lobby", "Lobby")
                        .WithOne("LobbyChat")
                        .HasForeignKey("LobbyApp.Models.LobbyChat", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lobby");
                });

            modelBuilder.Entity("LobbyApp.Models.Message", b =>
                {
                    b.HasOne("LobbyApp.Models.AccountChat", "AccountChat")
                        .WithMany("Messages")
                        .HasForeignKey("AccountChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LobbyApp.Models.LobbyChat", "LobbyChat")
                        .WithMany("Messages")
                        .HasForeignKey("LobbyChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountChat");

                    b.Navigation("LobbyChat");
                });

            modelBuilder.Entity("LobbyApp.Models.Account", b =>
                {
                    b.Navigation("AccountChat")
                        .IsRequired();
                });

            modelBuilder.Entity("LobbyApp.Models.AccountChat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("LobbyApp.Models.Lobby", b =>
                {
                    b.Navigation("LobbyChat")
                        .IsRequired();

                    b.Navigation("Players");
                });

            modelBuilder.Entity("LobbyApp.Models.LobbyChat", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
