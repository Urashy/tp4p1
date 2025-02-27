using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace tp4p1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "t_e_film_flm",
                schema: "public",
                columns: table => new
                {
                    flm_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flm_titre = table.Column<string>(type: "varchar(100)", nullable: false),
                    flm_resume = table.Column<string>(type: "text", nullable: true),
                    flm_datesortie = table.Column<DateTime>(type: "date", nullable: false),
                    flm_duree = table.Column<decimal>(type: "numeric(3,0)", nullable: false),
                    flm_genre = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_film", x => x.flm_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_utilisateur_utl",
                schema: "public",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    utl_nom = table.Column<string>(type: "varchar(50)", nullable: true),
                    utl_prenom = table.Column<string>(type: "varchar(50)", nullable: true),
                    utl_mobile = table.Column<string>(type: "char(10)", nullable: false),
                    utl_mail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    utl_pwd = table.Column<string>(type: "varchar(64)", nullable: true),
                    utl_rue = table.Column<string>(type: "varchar(200)", nullable: true),
                    utl_cp = table.Column<string>(type: "char(5)", nullable: true),
                    utl_ville = table.Column<string>(type: "varchar(50)", nullable: true),
                    utl_pays = table.Column<string>(type: "varchar(50)", nullable: true, defaultValue: "France"),
                    utl_latitude = table.Column<float>(type: "real", nullable: true),
                    utl_longitude = table.Column<float>(type: "real", nullable: true),
                    utl_datecreation = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_utilisateur", x => x.utl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_notation_not",
                schema: "public",
                columns: table => new
                {
                    utl_id = table.Column<int>(type: "integer", nullable: false),
                    flm_id = table.Column<int>(type: "integer", nullable: false),
                    not_note = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notation", x => new { x.utl_id, x.flm_id });
                    table.ForeignKey(
                        name: "fk_notation_film",
                        column: x => x.flm_id,
                        principalSchema: "public",
                        principalTable: "t_e_film_flm",
                        principalColumn: "flm_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_notation_utilisateur",
                        column: x => x.utl_id,
                        principalSchema: "public",
                        principalTable: "t_e_utilisateur_utl",
                        principalColumn: "utl_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "flm_titre",
                schema: "public",
                table: "t_e_film_flm",
                column: "flm_titre");

            migrationBuilder.CreateIndex(
                name: "utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl",
                column: "utl_mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_j_notation_not_flm_id",
                schema: "public",
                table: "t_j_notation_not",
                column: "flm_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_j_notation_not",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_film_flm",
                schema: "public");

            migrationBuilder.DropTable(
                name: "t_e_utilisateur_utl",
                schema: "public");
        }
    }
}
