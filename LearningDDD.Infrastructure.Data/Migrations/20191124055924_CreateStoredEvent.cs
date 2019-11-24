using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningDDD.Infrastructure.Data.Migrations
{
    public partial class CreateStoredEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoredEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageType = table.Column<string>(type: "varchar(200)", nullable: true),
                    AggregateId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(3)", nullable: false),
                    Data = table.Column<string>(type: "json", nullable: true),
                    User = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoredEvents");
        }
    }
}
