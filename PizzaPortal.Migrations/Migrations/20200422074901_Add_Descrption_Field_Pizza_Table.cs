using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaPortal.Migrations.Migrations
{
    public partial class Add_Descrption_Field_Pizza_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Pizzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Pizzas");
        }
    }
}
