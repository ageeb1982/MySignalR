using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ageebSoft.SignlR.Core.Migrations
{
    public partial class A2019_12_14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupsOnline",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date1 = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsOnline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date1 = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersOnline",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date1 = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersOnline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date1 = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    Msg = table.Column<string>(nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_MyUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "MyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersGroupsOnline",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date1 = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    GroupsOnlineId = table.Column<Guid>(nullable: false),
                    MyUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGroupsOnline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersGroupsOnline_GroupsOnline_GroupsOnlineId",
                        column: x => x.GroupsOnlineId,
                        principalTable: "GroupsOnline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersGroupsOnline_MyUsers_MyUserId",
                        column: x => x.MyUserId,
                        principalTable: "MyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroupsOnline_GroupsOnlineId",
                table: "UsersGroupsOnline",
                column: "GroupsOnlineId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersGroupsOnline_MyUserId",
                table: "UsersGroupsOnline",
                column: "MyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UsersGroupsOnline");

            migrationBuilder.DropTable(
                name: "UsersOnline");

            migrationBuilder.DropTable(
                name: "GroupsOnline");

            migrationBuilder.DropTable(
                name: "MyUsers");
        }
    }
}
