using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Data.SqlServer.Migrations
{
    public partial class Fixreleations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemOptionLocaled_Languages_LanguageId",
                table: "FormItemOptionLocaled");

            migrationBuilder.DropForeignKey(
                name: "FK_FormLocaled_Languages_LanguageId",
                table: "FormLocaled");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultItem_Results_ResultId",
                table: "ResultItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultItemValue_ResultItem_ResultItemId",
                table: "ResultItemValue");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Languages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemOptionLocaled_Languages_LanguageId",
                table: "FormItemOptionLocaled",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormLocaled_Languages_LanguageId",
                table: "FormLocaled",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultItem_Results_ResultId",
                table: "ResultItem",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultItemValue_ResultItem_ResultItemId",
                table: "ResultItemValue",
                column: "ResultItemId",
                principalTable: "ResultItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemOptionLocaled_Languages_LanguageId",
                table: "FormItemOptionLocaled");

            migrationBuilder.DropForeignKey(
                name: "FK_FormLocaled_Languages_LanguageId",
                table: "FormLocaled");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultItem_Results_ResultId",
                table: "ResultItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultItemValue_ResultItem_ResultItemId",
                table: "ResultItemValue");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Languages");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemOptionLocaled_Languages_LanguageId",
                table: "FormItemOptionLocaled",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormLocaled_Languages_LanguageId",
                table: "FormLocaled",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultItem_Results_ResultId",
                table: "ResultItem",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultItemValue_ResultItem_ResultItemId",
                table: "ResultItemValue",
                column: "ResultItemId",
                principalTable: "ResultItem",
                principalColumn: "Id");
        }
    }
}
