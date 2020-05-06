using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaPortal.Migrations.Migrations
{
    public partial class Add_PhotoPath_Field_To_Pizza_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Pizzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Pizzas");
        }
    }
}
