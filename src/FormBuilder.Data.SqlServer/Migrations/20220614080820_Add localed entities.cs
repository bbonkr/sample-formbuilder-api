using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Data.SqlServer.Migrations
{
    public partial class Addlocaledentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormItemLocaled",
                columns: table => new
                {
                    FormItemId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Placeholder = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemLocaled", x => new { x.FormItemId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_FormItemLocaled_FormItems_FormItemId",
                        column: x => x.FormItemId,
                        principalTable: "FormItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormItemLocaled_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormItemOptionLocaled",
                columns: table => new
                {
                    FormItemOptionId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemOptionLocaled", x => new { x.FormItemOptionId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_FormItemOptionLocaled_FormItemOptions_FormItemOptionId",
                        column: x => x.FormItemOptionId,
                        principalTable: "FormItemOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormItemOptionLocaled_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormLocaled",
                columns: table => new
                {
                    FormId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    LanguageId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormLocaled", x => new { x.FormId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_FormLocaled_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormLocaled_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormItemLocaled_LanguageId",
                table: "FormItemLocaled",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemOptionLocaled_LanguageId",
                table: "FormItemOptionLocaled",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_FormLocaled_LanguageId",
                table: "FormLocaled",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormItemLocaled");

            migrationBuilder.DropTable(
                name: "FormItemOptionLocaled");

            migrationBuilder.DropTable(
                name: "FormLocaled");
        }
    }
}
