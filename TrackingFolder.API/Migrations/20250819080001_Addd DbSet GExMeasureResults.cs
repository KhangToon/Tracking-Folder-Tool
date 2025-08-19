using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrackingFolder.API.Migrations
{
    /// <inheritdoc />
    public partial class AdddDbSetGExMeasureResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                keyColumn: "Id",
                keyValue: new Guid("041b6cde-53e1-4a8b-ab62-6c82a763aac7"));

            migrationBuilder.DeleteData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                keyColumn: "Id",
                keyValue: new Guid("bc4cc609-7772-45ed-bd82-35559f8916e5"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                schema: "goldexpertapi",
                table: "GoldExpertMachines",
                columns: new[] { "Id", "DeletedBy", "DeletedOn", "FolderPath", "IsDeleted", "Model", "ModifiedBy", "ModifiedOn", "Name", "SerialNumber", "Version" },
                values: new object[,]
                {
                    { new Guid("041b6cde-53e1-4a8b-ab62-6c82a763aac7"), null, null, "/path/to/machine1", false, "ModelX", "system", 1755510579L, "Gold Expert Machine 1", "SN123456", "1.0" },
                    { new Guid("bc4cc609-7772-45ed-bd82-35559f8916e5"), null, null, "/path/to/machine2", false, "ModelY", "system", 1755510579L, "Gold Expert Machine 2", "SN654321", "1.0" }
                });
        }
    }
}
