using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenhouse_API.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelWithDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantAlerts_Plants_PlantId",
                table: "PlantAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAlerts_Sensors_SensorId",
                table: "ZoneAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneAlerts_Zones_ZoneId",
                table: "ZoneAlerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneAlerts",
                table: "ZoneAlerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantAlerts",
                table: "PlantAlerts");

            migrationBuilder.RenameTable(
                name: "ZoneAlerts",
                newName: "ZoneSensorAlerts");

            migrationBuilder.RenameTable(
                name: "PlantAlerts",
                newName: "PlantSensorAlerts");

            migrationBuilder.RenameIndex(
                name: "IX_ZoneAlerts_ZoneId",
                table: "ZoneSensorAlerts",
                newName: "IX_ZoneSensorAlerts_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_ZoneAlerts_SensorId",
                table: "ZoneSensorAlerts",
                newName: "IX_ZoneSensorAlerts_SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_PlantAlerts_PlantId",
                table: "PlantSensorAlerts",
                newName: "IX_PlantSensorAlerts_PlantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneSensorAlerts",
                table: "ZoneSensorAlerts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantSensorAlerts",
                table: "PlantSensorAlerts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantSensorAlerts_Plants_PlantId",
                table: "PlantSensorAlerts",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneSensorAlerts_Sensors_SensorId",
                table: "ZoneSensorAlerts",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneSensorAlerts_Zones_ZoneId",
                table: "ZoneSensorAlerts",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantSensorAlerts_Plants_PlantId",
                table: "PlantSensorAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneSensorAlerts_Sensors_SensorId",
                table: "ZoneSensorAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneSensorAlerts_Zones_ZoneId",
                table: "ZoneSensorAlerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneSensorAlerts",
                table: "ZoneSensorAlerts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantSensorAlerts",
                table: "PlantSensorAlerts");

            migrationBuilder.RenameTable(
                name: "ZoneSensorAlerts",
                newName: "ZoneAlerts");

            migrationBuilder.RenameTable(
                name: "PlantSensorAlerts",
                newName: "PlantAlerts");

            migrationBuilder.RenameIndex(
                name: "IX_ZoneSensorAlerts_ZoneId",
                table: "ZoneAlerts",
                newName: "IX_ZoneAlerts_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_ZoneSensorAlerts_SensorId",
                table: "ZoneAlerts",
                newName: "IX_ZoneAlerts_SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_PlantSensorAlerts_PlantId",
                table: "PlantAlerts",
                newName: "IX_PlantAlerts_PlantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneAlerts",
                table: "ZoneAlerts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantAlerts",
                table: "PlantAlerts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantAlerts_Plants_PlantId",
                table: "PlantAlerts",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAlerts_Sensors_SensorId",
                table: "ZoneAlerts",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneAlerts_Zones_ZoneId",
                table: "ZoneAlerts",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");
        }
    }
}
