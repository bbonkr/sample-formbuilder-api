using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Data.SqlServer.Migrations
{
    public partial class AddResultrelatedentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ResultId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    FormItemId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultItem_FormItems_FormItemId",
                        column: x => x.FormItemId,
                        principalTable: "FormItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResultItem_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResultItemValue",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ResultItemId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultItemValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResultItemValue_ResultItem_ResultItemId",
                        column: x => x.ResultItemId,
                        principalTable: "ResultItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultItem_FormItemId",
                table: "ResultItem",
                column: "FormItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultItem_ResultId",
                table: "ResultItem",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultItemValue_ResultItemId",
                table: "ResultItemValue",
                column: "ResultItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultItemValue");

            migrationBuilder.DropTable(
                name: "ResultItem");
        }
    }
}
