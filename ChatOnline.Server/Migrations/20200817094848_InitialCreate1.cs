using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatOnline.Server.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "friendsRelation",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserIMNumber = table.Column<long>(nullable: false),
                    FriendIMNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendsRelation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "iMUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IMNumber = table.Column<long>(nullable: false),
                    ConnectionId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iMUser", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendsRelation");

            migrationBuilder.DropTable(
                name: "iMUser");
        }
    }
}
