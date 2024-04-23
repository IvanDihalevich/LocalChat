﻿// <auto-generated />
using System;
using LocalChat.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LocalChat.Core.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    partial class ChatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LocalChat.Core.Entities.ChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatRooms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a2de186-e394-4d1a-ac2d-dc04ffa07582"),
                            Name = "General"
                        },
                        new
                        {
                            Id = new Guid("9be87dbd-97d1-4cd3-83f2-4da9ceb129e4"),
                            Name = "Random"
                        });
                });

            modelBuilder.Entity("LocalChat.Core.Entities.ChatRoomUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("chatRoomIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("userId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("chatRoomIdId");

                    b.HasIndex("userId");

                    b.ToTable("ChatRoomUsers");
                });

            modelBuilder.Entity("LocalChat.Core.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MessedgeUsersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SendTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SenderIdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("MessedgeUsersId");

                    b.HasIndex("SenderIdId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("LocalChat.Core.Entities.MessedgeUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.ToTable("messedgeUsers");
                });

            modelBuilder.Entity("LocalChat.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f3c796fa-2e94-40ac-afce-77b32f853b02"),
                            Email = "user1@example.com",
                            Name = "user1@example.com",
                            Password = "USER1@EXAMPLE.COM"
                        },
                        new
                        {
                            Id = new Guid("fb63dfde-1a61-4a52-85f9-bea776b4a9b5"),
                            Email = "user2@example.com",
                            Name = "user2@example.com",
                            Password = "USER2@EXAMPLE.COM"
                        });
                });

            modelBuilder.Entity("LocalChat.Core.Entities.ChatRoomUsers", b =>
                {
                    b.HasOne("LocalChat.Core.Entities.ChatRoom", "chatRoomId")
                        .WithMany()
                        .HasForeignKey("chatRoomIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LocalChat.Core.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("chatRoomId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("LocalChat.Core.Entities.Message", b =>
                {
                    b.HasOne("LocalChat.Core.Entities.ChatRoom", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId");

                    b.HasOne("LocalChat.Core.Entities.MessedgeUsers", null)
                        .WithMany("MessageId")
                        .HasForeignKey("MessedgeUsersId");

                    b.HasOne("LocalChat.Core.Entities.User", "SenderId")
                        .WithMany()
                        .HasForeignKey("SenderIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SenderId");
                });

            modelBuilder.Entity("LocalChat.Core.Entities.MessedgeUsers", b =>
                {
                    b.HasOne("LocalChat.Core.Entities.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("LocalChat.Core.Entities.ChatRoom", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("LocalChat.Core.Entities.MessedgeUsers", b =>
                {
                    b.Navigation("MessageId");
                });
#pragma warning restore 612, 618
        }
    }
}
