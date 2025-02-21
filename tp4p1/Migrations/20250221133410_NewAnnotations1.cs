using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tp4p1.Migrations
{
    /// <inheritdoc />
    public partial class NewAnnotations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "utl_mobile",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "char(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "utl_mobile",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "char(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(10)");

            migrationBuilder.AlterColumn<string>(
                name: "utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);
        }
    }
}
