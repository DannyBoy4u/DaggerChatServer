using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLayer.Migrations
{
    /// <inheritdoc />
    public partial class DDL_CreateWorkTaskAndTypeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tasking");

            migrationBuilder.CreateTable(
                name: "worktasktype",
                schema: "tasking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worktasktype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "worktask",
                schema: "tasking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeName = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    EpochCreateDate = table.Column<long>(type: "bigint", nullable: false),
                    EpochUpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UrlLink = table.Column<string>(type: "text", nullable: true),
                    SiteSource = table.Column<string>(type: "text", nullable: false),
                    EpochDueDate = table.Column<long>(type: "bigint", nullable: false),
                    EpochStartDate = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    TaskTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worktask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_worktask_worktasktype_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalSchema: "tasking",
                        principalTable: "worktasktype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_worktask_TaskTypeId",
                schema: "tasking",
                table: "worktask",
                column: "TaskTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "worktask",
                schema: "tasking");

            migrationBuilder.DropTable(
                name: "worktasktype",
                schema: "tasking");
        }
    }
}
