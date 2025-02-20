using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CheckOutTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TotalTime = table.Column<TimeSpan>(type: "time", nullable: false, computedColumnSql: "DATEDIFF(SECOND, CheckInTime, CheckOutTime)"),
                    OverTime = table.Column<TimeSpan>(type: "time", nullable: false, computedColumnSql: "CASE WHEN DATEDIFF(HOUR, CheckInTime, CheckOutTime) > 9 THEN DATEADD(SECOND, DATEDIFF(SECOND, DATEADD(HOUR, 9, CheckInTime), CheckOutTime), '00:00:00') ELSE '00:00:00' END"),
                    JobDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSheets");
        }
    }
}
