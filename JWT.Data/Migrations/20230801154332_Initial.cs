using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENTERPRISE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ENTERPRISE_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENTERPRISE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FOUNDATION_DATE = table.Column<long>(type: "bigint", nullable: true),
                    CREATED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    MODIFIED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTERPRISE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ROLE_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ROLE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    MODIFIED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_USER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ROLE_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CREATED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    MODIFIED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_USER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_INFO",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USER_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SECURITY_KEY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HASHED_PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FULLNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IS_EMAIL_CONFIRMED = table.Column<bool>(type: "bit", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_OF_BIRTH = table.Column<long>(type: "bigint", nullable: true),
                    ENTERPRISE_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CREATED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    MODIFIED_TIME = table.Column<long>(type: "bigint", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: true),
                    IS_DELETE = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_INFO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_INFO_ENTERPRISE_ENTERPRISE_ID",
                        column: x => x.ENTERPRISE_ID,
                        principalTable: "ENTERPRISE",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_INFO_ENTERPRISE_ID",
                table: "USER_INFO",
                column: "ENTERPRISE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "ROLE_USER");

            migrationBuilder.DropTable(
                name: "USER_INFO");

            migrationBuilder.DropTable(
                name: "ENTERPRISE");
        }
    }
}
