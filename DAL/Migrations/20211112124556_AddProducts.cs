using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<int>(type: "int", nullable: false),
                    TotalRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "Name", "Platform", "TotalRating" },
                values: new object[,]
                {
                    { 1, 2018, "Horizon Zero Dawn", 1, 78 },
                    { 2, 2016, "Counter Strike GO", 0, 85 },
                    { 3, 2017, "Brawl Stars", 2, 100 },
                    { 4, 2020, "Half-Life VR", 4, 70 },
                    { 5, 2008, "TES V Skyrim", 0, 89 },
                    { 6, 2015, "Clash Royale", 2, 80 },
                    { 7, 2017, "Beat Saber", 4, 87 },
                    { 8, 2011, "Terraria", 0, 93 },
                    { 9, 2020, "Genshin Impact", 0, 90 },
                    { 10, 2000, "Snake", 3, 100 },
                    { 11, 2007, "Contra city", 3, 99 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DateCreated_Name_Platform_TotalRating",
                table: "Products",
                columns: new[] { "DateCreated", "Name", "Platform", "TotalRating" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
