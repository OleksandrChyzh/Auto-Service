using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixStatusConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity ",
                table: "OrderServices");

            migrationBuilder.DropColumn(
                name: "TotalPrice ",
                table: "OrderServices");

            migrationBuilder.RenameColumn(
                name: "TotalCost ",
                table: "Orders",
                newName: "TotalCost");

            migrationBuilder.RenameColumn(
                name: "Status ",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "OrderDate ",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "OrderID ",
                table: "Orders",
                newName: "OrderID");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewID",
                table: "Reviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Masters ",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Masters ",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Schedules_WeekDay",
                table: "Schedules",
                sql: "\"WeekDay\" IN ('Понеділок', 'Вівторок', 'Середа', 'Четвер', 'П''ятниця', 'Субота', 'Неділя')");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Status_Values",
                table: "Orders",
                sql: "\"Status\" IN ('Створене', 'Прийняте', 'Закінчене', 'Оплачене')");

            migrationBuilder.AddForeignKey(
                name: "Order_FK",
                table: "Reviews",
                column: "ReviewID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Order_FK",
                table: "Reviews");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Schedules_WeekDay",
                table: "Schedules");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Status_Values",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Masters ");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Masters ");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "Orders",
                newName: "TotalCost ");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "Status ");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "OrderDate ");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Orders",
                newName: "OrderID ");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewID",
                table: "Reviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Quantity ",
                table: "OrderServices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice ",
                table: "OrderServices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
