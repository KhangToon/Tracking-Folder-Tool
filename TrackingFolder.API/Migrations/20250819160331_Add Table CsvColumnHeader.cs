using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrackingFolder.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCsvColumnHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                keyColumn: "Id",
                keyValue: new Guid("326dda81-124e-4cdc-9be9-2c9bc4edc3ae"));

            migrationBuilder.DeleteData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                keyColumn: "Id",
                keyValue: new Guid("aca252c7-ae91-40d1-af36-3fe3fbaf4a99"));

            migrationBuilder.CreateTable(
                name: "CsvColumnHeader",
                schema: "goldexpertapi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GExSerial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeaderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsvColumnHeader", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "goldexpertapi",
                table: "CsvColumnHeader",
                columns: new[] { "Id", "GExSerial", "HeaderName" },
                values: new object[,]
                {
                    { new Guid("0b1192d9-d8b6-47bd-b90e-98412d59cbd4"), "Default", "Ag" },
                    { new Guid("155a6475-d96f-487e-aac5-46f592f700e3"), "Default", "Zn" },
                    { new Guid("317d16e4-9363-4343-a468-d723bdedc95c"), "Default", "Ag +/-" },
                    { new Guid("34847e7b-4732-42db-9a09-7b84268e1eec"), "Default", "Pass/Fail" },
                    { new Guid("39a14162-e576-40e5-9aee-a3c142a4cfc0"), "Default", "Cu +/-" },
                    { new Guid("3f6ce19f-07f8-47fe-891b-85bb29670d9c"), "Default", "Live Time Total" },
                    { new Guid("41789a29-a452-4223-b2f3-f57849387fec"), "Default", "Ni +/-" },
                    { new Guid("5d5e6fcd-24d6-4d1d-b5dd-4d9915f39488"), "Default", "Cu" },
                    { new Guid("71ff512e-8e3f-409a-99c2-ccf429293637"), "Default", "Time" },
                    { new Guid("8797ddf9-d286-4310-b14b-cd4cfc27e725"), "Default", "Au" },
                    { new Guid("941ee3fe-0cba-4f02-a2b1-2079c01c06cb"), "Default", "Date" },
                    { new Guid("94f035c9-f2c8-475f-af72-0423c727a4ec"), "Default", "Au +/-" },
                    { new Guid("b6a40669-8b0f-4413-b1d7-90ec4ba568ef"), "Default", "Ni" },
                    { new Guid("b954fa2f-fc04-4b53-a2ca-a918188061cd"), "Default", "Elapsed Time Total" },
                    { new Guid("bd1814f6-a010-4420-8076-4e27cbe342b4"), "Default", "Zn +/-" },
                    { new Guid("f018a2af-0a69-47f0-9e92-c436134879e7"), "Default", "Reading" }
                });

            migrationBuilder.InsertData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                columns: new[] { "Id", "DeletedBy", "DeletedOn", "FolderPath", "IsDeleted", "Model", "ModifiedBy", "ModifiedOn", "Name", "SerialNumber", "Version" },
                values: new object[,]
                {
                    { new Guid("59fba43a-08e4-4839-96ad-a96c5f7f47ee"), null, null, "/path/to/machine1", false, "ModelX", "system", 1755619410L, "Gold Expert Machine 1", "SN123456", "1.0" },
                    { new Guid("fb3e6f4a-38e9-4678-a9e7-7aeb569d8398"), null, null, "/path/to/machine2", false, "ModelY", "system", 1755619410L, "Gold Expert Machine 2", "SN654321", "1.0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CsvColumnHeader",
                schema: "goldexpertapi");

            migrationBuilder.DeleteData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                keyColumn: "Id",
                keyValue: new Guid("59fba43a-08e4-4839-96ad-a96c5f7f47ee"));

            migrationBuilder.DeleteData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                keyColumn: "Id",
                keyValue: new Guid("fb3e6f4a-38e9-4678-a9e7-7aeb569d8398"));

            migrationBuilder.InsertData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                columns: new[] { "Id", "DeletedBy", "DeletedOn", "FolderPath", "IsDeleted", "Model", "ModifiedBy", "ModifiedOn", "Name", "SerialNumber", "Version" },
                values: new object[,]
                {
                    { new Guid("326dda81-124e-4cdc-9be9-2c9bc4edc3ae"), null, null, "/path/to/machine1", false, "ModelX", "system", 1755590401L, "Gold Expert Machine 1", "SN123456", "1.0" },
                    { new Guid("aca252c7-ae91-40d1-af36-3fe3fbaf4a99"), null, null, "/path/to/machine2", false, "ModelY", "system", 1755590401L, "Gold Expert Machine 2", "SN654321", "1.0" }
                });
        }
    }
}
