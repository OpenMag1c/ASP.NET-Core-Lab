using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background1.jpg", 50, "Action/RPG", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Horizon_Zero_Dawn_mpnuy7.jpg", 24.989999999999998, 16 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background3.jpg", 100, "Shooter", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Counter_Strike_tkkgm4.jpg", 4.9900000000000002, 18 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background5.jpg", 500, "MOBA", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Brawl_Stars_jwhuv1.jpg", 0.98999999999999999, 7 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg", 25, "Shooter", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Half_Life_t0lcqj.jpg", 29.989999999999998, 18 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background1.jpg", 90, "RPG", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Skyrim_b3rdpm.jpg", 19.989999999999998, 16 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background3.jpg", 200, "Strategy", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Clash_Royale_oipsjp.jpg", 0.98999999999999999, 7 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg", 75, "Music game", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Beat_Saber_ubvkuu.jpg", 5.9900000000000002, 3 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background5.jpg", 40, "RPG", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Terraria_uzjoxt.jpg", 2.9900000000000002, 12 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg", 700, "Action/RPG", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Genshin_Impact_x0fd6d.jpg", 5.9900000000000002, 7 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg", 999, "Puzzle", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Snake_toiezu.jpg", 18 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Background", "Count", "Genre", "Logo", "Price", "Rating" },
                values: new object[] { "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background4.jpg", 120, "Shooter", "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Contra_City_r3iefw.jpg", 4.9900000000000002, 16 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");
        }
    }
}
