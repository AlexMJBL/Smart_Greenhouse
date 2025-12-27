using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenhouse_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoilHumidityCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MinHumidityPct = table.Column<float>(type: "REAL", nullable: true),
                    MaxHumidityPct = table.Column<float>(type: "REAL", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilHumidityCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoneCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HumidityMinPct = table.Column<float>(type: "REAL", nullable: false),
                    HumidityMaxPct = table.Column<float>(type: "REAL", nullable: false),
                    LuminosityMinLux = table.Column<float>(type: "REAL", nullable: false),
                    LuminosityMaxLux = table.Column<float>(type: "REAL", nullable: false),
                    TemperatureMinC = table.Column<float>(type: "REAL", nullable: false),
                    TemperatureMaxC = table.Column<float>(type: "REAL", nullable: false),
                    PressureMinPa = table.Column<float>(type: "REAL", nullable: false),
                    PressureMaxPa = table.Column<float>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specimens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SoilHumidityCatId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specimens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specimens_SoilHumidityCategories_SoilHumidityCatId",
                        column: x => x.SoilHumidityCatId,
                        principalTable: "SoilHumidityCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ZoneCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_ZoneCategories_ZoneCategoryId",
                        column: x => x.ZoneCategoryId,
                        principalTable: "ZoneCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AcquiredDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    SpecimenId = table.Column<int>(type: "INTEGER", nullable: false),
                    ZoneId = table.Column<int>(type: "INTEGER", nullable: false),
                    MomId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_Plants_MomId",
                        column: x => x.MomId,
                        principalTable: "Plants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plants_Specimens_SpecimenId",
                        column: x => x.SpecimenId,
                        principalTable: "Specimens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plants_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SensorCode = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    LastSeen = table.Column<bool>(type: "INTEGER", nullable: false),
                    ZoneId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fertilizers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    PlantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fertilizers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fertilizers_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observations_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlantId = table.Column<int>(type: "INTEGER", nullable: false),
                    Reason = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorType = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantAlerts_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantHumidityRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecordPct = table.Column<float>(type: "REAL", nullable: false),
                    InRange = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantHumidityRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantHumidityRecords_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Waterings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HumPctBefore = table.Column<float>(type: "REAL", nullable: false),
                    HumPctAfter = table.Column<float>(type: "REAL", nullable: false),
                    WaterQuantityMl = table.Column<int>(type: "INTEGER", nullable: false),
                    PlantId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waterings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waterings_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZoneAlerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Reason = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorType = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ZoneId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneAlerts_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZoneAlerts_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ZonePressureRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecordedHPa = table.Column<float>(type: "REAL", nullable: false),
                    ZoneId = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonePressureRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZonePressureRecords_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZonePressureRecords_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZoneRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Record = table.Column<float>(type: "REAL", nullable: false),
                    InRange = table.Column<bool>(type: "INTEGER", nullable: false),
                    ZoneId = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneRecords_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZoneRecords_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fertilizers_PlantId",
                table: "Fertilizers",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_PlantId",
                table: "Observations",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantAlerts_PlantId",
                table: "PlantAlerts",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantHumidityRecords_PlantId",
                table: "PlantHumidityRecords",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_MomId",
                table: "Plants",
                column: "MomId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SpecimenId",
                table: "Plants",
                column: "SpecimenId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_ZoneId",
                table: "Plants",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_ZoneId",
                table: "Sensors",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Specimens_SoilHumidityCatId",
                table: "Specimens",
                column: "SoilHumidityCatId");

            migrationBuilder.CreateIndex(
                name: "IX_Waterings_PlantId",
                table: "Waterings",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAlerts_SensorId",
                table: "ZoneAlerts",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneAlerts_ZoneId",
                table: "ZoneAlerts",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ZonePressureRecords_SensorId",
                table: "ZonePressureRecords",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_ZonePressureRecords_ZoneId",
                table: "ZonePressureRecords",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneRecords_SensorId",
                table: "ZoneRecords",
                column: "SensorId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneRecords_ZoneId",
                table: "ZoneRecords",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_ZoneCategoryId",
                table: "Zones",
                column: "ZoneCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fertilizers");

            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "PlantAlerts");

            migrationBuilder.DropTable(
                name: "PlantHumidityRecords");

            migrationBuilder.DropTable(
                name: "Waterings");

            migrationBuilder.DropTable(
                name: "ZoneAlerts");

            migrationBuilder.DropTable(
                name: "ZonePressureRecords");

            migrationBuilder.DropTable(
                name: "ZoneRecords");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Specimens");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "SoilHumidityCategories");

            migrationBuilder.DropTable(
                name: "ZoneCategories");
        }
    }
}
