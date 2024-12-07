using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WildlifeSanctuaryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Sanctuaries",
                columns: table => new
                {
                    SanctuaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalArea = table.Column<double>(type: "float", nullable: false),
                    HabitatType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProtectedSpecies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanctuaries", x => x.SanctuaryId);
                    table.ForeignKey(
                        name: "FK_Sanctuaries_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Species = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastCheckupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SanctuaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                    table.ForeignKey(
                        name: "FK_Animals_Sanctuaries_SanctuaryId",
                        column: x => x.SanctuaryId,
                        principalTable: "Sanctuaries",
                        principalColumn: "SanctuaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostManagements",
                columns: table => new
                {
                    CostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanctuaryId = table.Column<int>(type: "int", nullable: false),
                    ExpenseType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsiblePersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostManagements", x => x.CostId);
                    table.ForeignKey(
                        name: "FK_CostManagements_Sanctuaries_SanctuaryId",
                        column: x => x.SanctuaryId,
                        principalTable: "Sanctuaries",
                        principalColumn: "SanctuaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalData",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanctuaryId = table.Column<int>(type: "int", nullable: false),
                    ImpactType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Recommendations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ConductedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalData", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_EnvironmentalData_Sanctuaries_SanctuaryId",
                        column: x => x.SanctuaryId,
                        principalTable: "Sanctuaries",
                        principalColumn: "SanctuaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    IncidentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanctuaryId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResolutionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_Incidents_Sanctuaries_SanctuaryId",
                        column: x => x.SanctuaryId,
                        principalTable: "Sanctuaries",
                        principalColumn: "SanctuaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanctuaryId = table.Column<int>(type: "int", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RangerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Sanctuaries_SanctuaryId",
                        column: x => x.SanctuaryId,
                        principalTable: "Sanctuaries",
                        principalColumn: "SanctuaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StorageLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastRestockedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SanctuaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resources_Sanctuaries_SanctuaryId",
                        column: x => x.SanctuaryId,
                        principalTable: "Sanctuaries",
                        principalColumn: "SanctuaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WildlifeData",
                columns: table => new
                {
                    DataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanctuaryId = table.Column<int>(type: "int", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ObservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BehavioralReport = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PopulationEstimate = table.Column<int>(type: "int", nullable: true),
                    BiologistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WildlifeData", x => x.DataId);
                    table.ForeignKey(
                        name: "FK_WildlifeData_Sanctuaries_SanctuaryId",
                        column: x => x.SanctuaryId,
                        principalTable: "Sanctuaries",
                        principalColumn: "SanctuaryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimalsMedicalRecords",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Treatment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VetId = table.Column<int>(type: "int", nullable: false),
                    NextCheckup = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalsMedicalRecords", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_AnimalsMedicalRecords_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "AnimalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SanctuaryId",
                table: "Animals",
                column: "SanctuaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalsMedicalRecords_AnimalId",
                table: "AnimalsMedicalRecords",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_CostManagements_SanctuaryId",
                table: "CostManagements",
                column: "SanctuaryId");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalData_SanctuaryId",
                table: "EnvironmentalData",
                column: "SanctuaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_SanctuaryId",
                table: "Incidents",
                column: "SanctuaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SanctuaryId",
                table: "Projects",
                column: "SanctuaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_SanctuaryId",
                table: "Resources",
                column: "SanctuaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sanctuaries_ManagerId",
                table: "Sanctuaries",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_WildlifeData_SanctuaryId",
                table: "WildlifeData",
                column: "SanctuaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalsMedicalRecords");

            migrationBuilder.DropTable(
                name: "CostManagements");

            migrationBuilder.DropTable(
                name: "EnvironmentalData");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "WildlifeData");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Sanctuaries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
