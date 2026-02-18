using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyWeb.Migrations
{
    /// <inheritdoc />
    public partial class CriandoBancoDeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USUARIO = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SENHA = table.Column<string>(type: "char(60)", fixedLength: true, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categoria",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TIPO = table.Column<string>(type: "char(7)", fixedLength: true, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    COR = table.Column<string>(type: "char(7)", fixedLength: true, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    USUARIO_ID = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria", x => x.ID);
                    table.ForeignKey(
                        name: "fk_categoria_usuario",
                        column: x => x.USUARIO_ID,
                        principalTable: "usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "conta",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SALDO = table.Column<decimal>(type: "decimal(9,2)", nullable: false, defaultValueSql: "0.00"),
                    USUARIO_ID = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conta", x => x.ID);
                    table.ForeignKey(
                        name: "fk_conta_usuario",
                        column: x => x.USUARIO_ID,
                        principalTable: "usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cartao",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "varchar(80)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DATA_FECHAMENTO = table.Column<DateOnly>(type: "date", nullable: false),
                    DATA_VENCIMENTO = table.Column<DateOnly>(type: "date", nullable: false),
                    LIMITE = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    LIMITE_DISPONIVEL = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    VALOR_PARCELADO = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    CONTA_ID = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartao", x => x.ID);
                    table.ForeignKey(
                        name: "fk_cartao_conta",
                        column: x => x.CONTA_ID,
                        principalTable: "conta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lancamento",
                columns: table => new
                {
                    ID = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TIPO = table.Column<string>(type: "char(7)", fixedLength: true, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VALOR = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DATA = table.Column<DateOnly>(type: "date", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "varchar(1000)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FIXO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PRE_LANCAMENTO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CATEGORIA_ID = table.Column<uint>(type: "int unsigned", nullable: false),
                    CONTA_ID = table.Column<uint>(type: "int unsigned", nullable: false),
                    USUARIO_ID = table.Column<uint>(type: "int unsigned", nullable: false),
                    CARTAO_ID = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lancamento", x => x.ID);
                    table.ForeignKey(
                        name: "fk_lancamento_cartao",
                        column: x => x.CARTAO_ID,
                        principalTable: "cartao",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_lancamento_categoria",
                        column: x => x.CATEGORIA_ID,
                        principalTable: "categoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lancamento_conta",
                        column: x => x.CONTA_ID,
                        principalTable: "conta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lancamento_usuario",
                        column: x => x.USUARIO_ID,
                        principalTable: "usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "idx_fk_cartao_conta",
                table: "cartao",
                column: "CONTA_ID");

            migrationBuilder.CreateIndex(
                name: "idx_fk_categoria_usuario",
                table: "categoria",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "idx_fk_conta_usuario",
                table: "conta",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "idx_fk_lancamento_cartao",
                table: "lancamento",
                column: "CARTAO_ID");

            migrationBuilder.CreateIndex(
                name: "idx_fk_lancamento_categoria",
                table: "lancamento",
                column: "CATEGORIA_ID");

            migrationBuilder.CreateIndex(
                name: "idx_fk_lancamento_conta",
                table: "lancamento",
                column: "CONTA_ID");

            migrationBuilder.CreateIndex(
                name: "idx_fk_lancamento_usuario",
                table: "lancamento",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "usuario_unique",
                table: "usuario",
                column: "USUARIO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lancamento");

            migrationBuilder.DropTable(
                name: "cartao");

            migrationBuilder.DropTable(
                name: "categoria");

            migrationBuilder.DropTable(
                name: "conta");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
