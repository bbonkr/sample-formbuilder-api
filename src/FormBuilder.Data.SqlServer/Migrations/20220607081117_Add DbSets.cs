using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilder.Data.SqlServer.Migrations
{
    public partial class AddDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItem_Forms_FormId",
                table: "FormItem");

            migrationBuilder.DropForeignKey(
                name: "FK_FormItemOption_FormItem_FormItemId",
                table: "FormItemOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormItemOption",
                table: "FormItemOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormItem",
                table: "FormItem");

            migrationBuilder.RenameTable(
                name: "FormItemOption",
                newName: "FormItemOptions");

            migrationBuilder.RenameTable(
                name: "FormItem",
                newName: "FormItems");

            migrationBuilder.RenameIndex(
                name: "IX_FormItemOption_FormItemId",
                table: "FormItemOptions",
                newName: "IX_FormItemOptions_FormItemId");

            migrationBuilder.RenameIndex(
                name: "IX_FormItem_FormId",
                table: "FormItems",
                newName: "IX_FormItems_FormId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormItemOptions",
                table: "FormItemOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormItems",
                table: "FormItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemOptions_FormItems_FormItemId",
                table: "FormItemOptions",
                column: "FormItemId",
                principalTable: "FormItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormItems_Forms_FormId",
                table: "FormItems",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormItemOptions_FormItems_FormItemId",
                table: "FormItemOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_FormItems_Forms_FormId",
                table: "FormItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormItems",
                table: "FormItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormItemOptions",
                table: "FormItemOptions");

            migrationBuilder.RenameTable(
                name: "FormItems",
                newName: "FormItem");

            migrationBuilder.RenameTable(
                name: "FormItemOptions",
                newName: "FormItemOption");

            migrationBuilder.RenameIndex(
                name: "IX_FormItems_FormId",
                table: "FormItem",
                newName: "IX_FormItem_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_FormItemOptions_FormItemId",
                table: "FormItemOption",
                newName: "IX_FormItemOption_FormItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormItem",
                table: "FormItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormItemOption",
                table: "FormItemOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormItem_Forms_FormId",
                table: "FormItem",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormItemOption_FormItem_FormItemId",
                table: "FormItemOption",
                column: "FormItemId",
                principalTable: "FormItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
