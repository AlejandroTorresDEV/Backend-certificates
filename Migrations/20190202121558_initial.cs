using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GttApiWeb.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jira",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_id = table.Column<long>(nullable: false),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true),
                    proyect = table.Column<string>(nullable: true),
                    componente = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jira", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    email = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    rolUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    alias = table.Column<string>(nullable: true),
                    entidad_emisiora = table.Column<string>(nullable: true),
                    numero_de_serie = table.Column<string>(nullable: true),
                    subject = table.Column<string>(nullable: true),
                    caducidad = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    id_orga = table.Column<string>(nullable: true),
                    nombre_cliente = table.Column<string>(nullable: true),
                    contacto_renovacion = table.Column<string>(nullable: true),
                    repositorio = table.Column<string>(nullable: true),
                    observaciones = table.Column<string>(nullable: true),
                    integration_list = table.Column<string>(nullable: true),
                    user_idid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.id);
                    table.ForeignKey(
                        name: "FK_Certificates_Users_user_idid",
                        column: x => x.user_idid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_user_idid",
                table: "Certificates",
                column: "user_idid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Jira");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
