using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaPortal.Migrations.Migrations
{
    public partial class Add_IsPreferredPizza_Field_Pizza_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreferredPizza",
                table: "Pizzas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreferredPizza",
                table: "Pizzas");
        }
    }
}
