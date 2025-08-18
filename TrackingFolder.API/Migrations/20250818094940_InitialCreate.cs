using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrackingFolder.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "goldexpertapi");

            migrationBuilder.CreateTable(
                name: "GoldExpertMachines",
                schema: "goldexpertapi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FolderPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldExpertMachines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GExMeasureResult",
                schema: "goldexpertapi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GExMachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Reading = table.Column<double>(type: "float", nullable: false),
                    ElapsedTimeTotal = table.Column<TimeSpan>(type: "time", nullable: false),
                    Ni = table.Column<double>(type: "float", nullable: false),
                    NiPlusMinus = table.Column<double>(type: "float", nullable: false),
                    Cu = table.Column<double>(type: "float", nullable: false),
                    CuPlusMinus = table.Column<double>(type: "float", nullable: false),
                    Zn = table.Column<double>(type: "float", nullable: false),
                    ZnPlusMinus = table.Column<double>(type: "float", nullable: false),
                    Ag = table.Column<double>(type: "float", nullable: false),
                    AgPlusMinus = table.Column<double>(type: "float", nullable: false),
                    Au = table.Column<double>(type: "float", nullable: false),
                    AuPlusMinus = table.Column<double>(type: "float", nullable: false),
                    PassFail = table.Column<bool>(type: "bit", nullable: false),
                    LiveTimeTotal = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<long>(type: "bigint", nullable: true),
                    GoldExpertMachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GExMeasureResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GExMeasureResult_GoldExpertMachines_GoldExpertMachineId",
                        column: x => x.GoldExpertMachineId,
                        principalSchema: "goldexpertapi",
                        principalTable: "GoldExpertMachines",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                columns: new[] { "Id", "DeletedBy", "DeletedOn", "FolderPath", "IsDeleted", "Model", "ModifiedBy", "ModifiedOn", "Name", "SerialNumber", "Version" },
                values: new object[,]
                {
                    { new Guid("041b6cde-53e1-4a8b-ab62-6c82a763aac7"), null, null, "/path/to/machine1", false, "ModelX", "system", 1755510579L, "Gold Expert Machine 1", "SN123456", "1.0" },
                    { new Guid("bc4cc609-7772-45ed-bd82-35559f8916e5"), null, null, "/path/to/machine2", false, "ModelY", "system", 1755510579L, "Gold Expert Machine 2", "SN654321", "1.0" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GExMeasureResult_GoldExpertMachineId",
                schema: "goldexpertapi",
                table: "GExMeasureResult",
                column: "GoldExpertMachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GExMeasureResult",
                schema: "goldexpertapi");

            migrationBuilder.DropTable(
                name: "GoldExpertMachines",
                schema: "goldexpertapi");
        }
    }
}
