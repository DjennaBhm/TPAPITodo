using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTodo.Migrations
{
    /// <inheritdoc />
    public partial class AddAgenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgendaId",
                table: "Api_Todo",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Agendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Api_Todo_AgendaId",
                table: "Api_Todo",
                column: "AgendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Api_Todo_Agendas_AgendaId",
                table: "Api_Todo",
                column: "AgendaId",
                principalTable: "Agendas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Api_Todo_Agendas_AgendaId",
                table: "Api_Todo");

            migrationBuilder.DropTable(
                name: "Agendas");

            migrationBuilder.DropIndex(
                name: "IX_Api_Todo_AgendaId",
                table: "Api_Todo");

            migrationBuilder.DropColumn(
                name: "AgendaId",
                table: "Api_Todo");
        }
    }
}
