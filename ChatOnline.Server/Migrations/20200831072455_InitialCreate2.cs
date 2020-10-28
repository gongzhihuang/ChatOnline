using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatOnline.Server.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "iMUser");

            migrationBuilder.DropColumn(
                name: "FriendIMNumber",
                table: "friendsRelation");

            migrationBuilder.DropColumn(
                name: "UserIMNumber",
                table: "friendsRelation");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "friendsRelation",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "FriendId",
                table: "friendsRelation",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "friendsRelation",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "chatOnlineUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActualName = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    ConnectionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatOnlineUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chatRecord",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    FriendId = table.Column<long>(nullable: false),
                    TimeAt = table.Column<DateTime>(nullable: false),
                    MessageContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatRecord", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chatOnlineUser");

            migrationBuilder.DropTable(
                name: "chatRecord");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "friendsRelation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "friendsRelation");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "friendsRelation",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "FriendIMNumber",
                table: "friendsRelation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserIMNumber",
                table: "friendsRelation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "iMUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ConnectionId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    IMNumber = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iMUser", x => x.Id);
                });
        }
    }
}
