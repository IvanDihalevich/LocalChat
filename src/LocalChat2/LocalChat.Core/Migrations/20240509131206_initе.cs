using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LocalChat.Core.Migrations
{
    /// <inheritdoc />
    public partial class initе : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_messedgeUsers_MessedgeUsersId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messedgeUsers_AspNetUsers_ReceiverId",
                table: "messedgeUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messedgeUsers",
                table: "messedgeUsers");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("df70e1c0-ea77-4504-ad89-86471a198fa1"), new Guid("07f96f6d-44d6-4896-bc72-2f4bf26c35bb") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1c884f19-5f5a-45ce-acbb-c0ec76ff0969"), new Guid("80feeb17-aa63-477e-ac0c-177377466515") });

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: new Guid("3fa61063-e910-4d7d-acb3-257d39e00bc3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1c884f19-5f5a-45ce-acbb-c0ec76ff0969"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("df70e1c0-ea77-4504-ad89-86471a198fa1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("07f96f6d-44d6-4896-bc72-2f4bf26c35bb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80feeb17-aa63-477e-ac0c-177377466515"));

            migrationBuilder.RenameTable(
                name: "messedgeUsers",
                newName: "MessedgeUsers");

            migrationBuilder.RenameIndex(
                name: "IX_messedgeUsers_ReceiverId",
                table: "MessedgeUsers",
                newName: "IX_MessedgeUsers_ReceiverId");

            migrationBuilder.AddColumn<Guid>(
                name: "MessageID",
                table: "ChatRooms",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessedgeUsers",
                table: "MessedgeUsers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8aeafa0a-5cf0-4967-a53a-e4b628f65ffa"), "8aeafa0a-5cf0-4967-a53a-e4b628f65ffa", "Admin", "ADMIN" },
                    { new Guid("a3f87644-3bea-45f7-a3b2-8e3fe856a258"), "a3f87644-3bea-45f7-a3b2-8e3fe856a258", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("7d247d66-8f68-4cda-95ab-4a1319f30129"), 0, "2615429c-f545-4b7d-af54-cbdacb7dd671", "admin@localchat.example", true, "Admin User", false, null, "ADMIN@LOCALCHAT.EXAMPLE", "ADMIN@LOCALCHAT.EXAMPLE", "AQAAAAIAAYagAAAAEIm8k6qSEldHKuiK0pHhG7aJeGy3S4IQm6qZH2FftKlfbrUgh9CeR6gSPb/hC1SO1w==", null, false, "0cb52d15-840a-4b23-8a9b-8764ca2af8cc", false, "admin@localchat.example" },
                    { new Guid("b97abd37-1d2c-474a-bf7a-9178cc1ae9f9"), 0, "676c3c84-f9ac-4710-a7b8-35f10527e841", "user@localchat.example", true, "Regular User", false, null, "USER@LOCALCHAT.EXAMPLE", "USER@LOCALCHAT.EXAMPLE", "AQAAAAIAAYagAAAAEER/QmFKFozwSjoBdCtxsUCfejgyDEaMHhZQEmHOXmJmSEAFyBW9RiBa7ELPhqJjww==", null, false, "7727e003-6538-46b5-b87c-25c02ccc9ce6", false, "user@localchat.example" }
                });

            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "MessageID", "Name" },
                values: new object[] { new Guid("78faa82a-f038-4794-aac0-706bcec072a4"), null, "General" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("8aeafa0a-5cf0-4967-a53a-e4b628f65ffa"), new Guid("7d247d66-8f68-4cda-95ab-4a1319f30129") },
                    { new Guid("a3f87644-3bea-45f7-a3b2-8e3fe856a258"), new Guid("b97abd37-1d2c-474a-bf7a-9178cc1ae9f9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessedgeUsers_MessedgeUsersId",
                table: "Messages",
                column: "MessedgeUsersId",
                principalTable: "MessedgeUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessedgeUsers_AspNetUsers_ReceiverId",
                table: "MessedgeUsers",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessedgeUsers_MessedgeUsersId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessedgeUsers_AspNetUsers_ReceiverId",
                table: "MessedgeUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessedgeUsers",
                table: "MessedgeUsers");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8aeafa0a-5cf0-4967-a53a-e4b628f65ffa"), new Guid("7d247d66-8f68-4cda-95ab-4a1319f30129") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a3f87644-3bea-45f7-a3b2-8e3fe856a258"), new Guid("b97abd37-1d2c-474a-bf7a-9178cc1ae9f9") });

            migrationBuilder.DeleteData(
                table: "ChatRooms",
                keyColumn: "Id",
                keyValue: new Guid("78faa82a-f038-4794-aac0-706bcec072a4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8aeafa0a-5cf0-4967-a53a-e4b628f65ffa"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a3f87644-3bea-45f7-a3b2-8e3fe856a258"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d247d66-8f68-4cda-95ab-4a1319f30129"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b97abd37-1d2c-474a-bf7a-9178cc1ae9f9"));

            migrationBuilder.DropColumn(
                name: "MessageID",
                table: "ChatRooms");

            migrationBuilder.RenameTable(
                name: "MessedgeUsers",
                newName: "messedgeUsers");

            migrationBuilder.RenameIndex(
                name: "IX_MessedgeUsers_ReceiverId",
                table: "messedgeUsers",
                newName: "IX_messedgeUsers_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messedgeUsers",
                table: "messedgeUsers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1c884f19-5f5a-45ce-acbb-c0ec76ff0969"), "1c884f19-5f5a-45ce-acbb-c0ec76ff0969", "User", "USER" },
                    { new Guid("df70e1c0-ea77-4504-ad89-86471a198fa1"), "df70e1c0-ea77-4504-ad89-86471a198fa1", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("07f96f6d-44d6-4896-bc72-2f4bf26c35bb"), 0, "f8446f8d-799f-4b82-afe4-6b728e42624e", "admin@localchat.example", true, "Admin User", false, null, "ADMIN@LOCALCHAT.EXAMPLE", "ADMIN@LOCALCHAT.EXAMPLE", "AQAAAAIAAYagAAAAEETuZ6V/svAoxc3n699y32oxFnHZ+6tup78IHi4k261v8FLWtn0iKv473O97VOIR+Q==", null, false, "06ea6100-be74-4cbe-81f1-48b67a3d060e", false, "admin@localchat.example" },
                    { new Guid("80feeb17-aa63-477e-ac0c-177377466515"), 0, "13025f90-d0d8-460c-90f3-aa36338d5a1d", "user@localchat.example", true, "Regular User", false, null, "USER@LOCALCHAT.EXAMPLE", "USER@LOCALCHAT.EXAMPLE", "AQAAAAIAAYagAAAAEJSJyPtT1xlerxizcD0l4jXLgg5vISJ8pDTihgWDQqNZzFkUQShxLyTbv+VcyQvwEQ==", null, false, "2fe3b0d5-34c7-45a0-986f-003fcb547795", false, "user@localchat.example" }
                });

            migrationBuilder.InsertData(
                table: "ChatRooms",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("3fa61063-e910-4d7d-acb3-257d39e00bc3"), "General" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("df70e1c0-ea77-4504-ad89-86471a198fa1"), new Guid("07f96f6d-44d6-4896-bc72-2f4bf26c35bb") },
                    { new Guid("1c884f19-5f5a-45ce-acbb-c0ec76ff0969"), new Guid("80feeb17-aa63-477e-ac0c-177377466515") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_messedgeUsers_MessedgeUsersId",
                table: "Messages",
                column: "MessedgeUsersId",
                principalTable: "messedgeUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_messedgeUsers_AspNetUsers_ReceiverId",
                table: "messedgeUsers",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
