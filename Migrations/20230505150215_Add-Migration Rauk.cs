using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationRauk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationAndEvaluations",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrameCaseNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Dnr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Period = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PeriodDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Archived_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Accompanying_SupportStaff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exchange_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weighted_QualityPoints_BudgetView = table.Column<float>(type: "real", nullable: false),
                    Average_TotalPoints_Application = table.Column<float>(type: "real", nullable: false),
                    PointDifference_ApplicationView = table.Column<float>(type: "real", nullable: false),
                    Weighted_AveragePoints = table.Column<float>(type: "real", nullable: false),
                    AverageRating = table.Column<float>(type: "real", nullable: false),
                    PointDifference = table.Column<float>(type: "real", nullable: false),
                    QualityPoints_Report = table.Column<float>(type: "real", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationAndEvaluations", x => x.ApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrganizationEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plus_Bankgiro = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "PreviousApplications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousDnr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousApplications", x => x.ApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EducationLevel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EducationalProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weeks = table.Column<int>(type: "int", nullable: false),
                    PartnerSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerSchool_EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To_Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Applied_Participant_Number = table.Column<int>(type: "int", nullable: false),
                    Granted_Participant_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Participant_Number = table.Column<int>(type: "int", nullable: false),
                    Applied_Staff_Teacher_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Staff_Teacher_Number = table.Column<int>(type: "int", nullable: false),
                    Applied_Student_Number = table.Column<int>(type: "int", nullable: false),
                    Approved_Student_Number = table.Column<int>(type: "int", nullable: false),
                    Granted_Student_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Student_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Women_Student_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Men_Student_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Women_Teacher_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Men_Teacher_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Women_SchoolLeader_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Men_SchoolLeader_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Women_AssociatedStaff_Number = table.Column<int>(type: "int", nullable: false),
                    Reported_Men_AssociatedStaff_Number = table.Column<int>(type: "int", nullable: false),
                    Applied_Staff_Number = table.Column<int>(type: "int", nullable: false),
                    Granted_Staff_Number = table.Column<int>(type: "int", nullable: false),
                    Approved_Staff_Number = table.Column<int>(type: "int", nullable: false),
                    Repported_Staff_Number = table.Column<int>(type: "int", nullable: false),
                    Accompanying_Support_Staff = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                    table.ForeignKey(
                        name: "FK_Participants_ApplicationAndEvaluations_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "ApplicationAndEvaluations",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentAmount = table.Column<float>(type: "real", nullable: false),
                    Total_Applied_Amount = table.Column<float>(type: "real", nullable: false),
                    Total_Granted_Amount = table.Column<float>(type: "real", nullable: false),
                    Total_Reported_Amount = table.Column<float>(type: "real", nullable: false),
                    Total_Approved_Amount = table.Column<float>(type: "real", nullable: false),
                    Applied_Amount_ExtraFunds = table.Column<float>(type: "real", nullable: false),
                    Granted_Amount_ExtraFunds = table.Column<float>(type: "real", nullable: false),
                    Reported_Amount_ExtraFunds = table.Column<float>(type: "real", nullable: false),
                    Approved_Adjusted_Amount_ExtraFunds = table.Column<float>(type: "real", nullable: false),
                    Applied_AuditGrant = table.Column<float>(type: "real", nullable: false),
                    Granted_AuditGrant = table.Column<float>(type: "real", nullable: false),
                    Applied_AdminGrant = table.Column<float>(type: "real", nullable: false),
                    Granted_AdminGrant = table.Column<float>(type: "real", nullable: false),
                    Applied_Amount_Scholarships = table.Column<float>(type: "real", nullable: false),
                    Granted_Amount_Scholarships = table.Column<float>(type: "real", nullable: false),
                    Approved_Amount_Scholarships = table.Column<float>(type: "real", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_ApplicationAndEvaluations_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "ApplicationAndEvaluations",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportAndReclaims",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportStatusDate = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: false),
                    Date_when_ReportStatus_Set = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status_Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reclaim_Paid_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reclaim_Amount = table.Column<float>(type: "real", nullable: false),
                    Reclaim_Paid_Amount = table.Column<float>(type: "real", nullable: false),
                    Reclaim_Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfReportedScholarships = table.Column<int>(type: "int", nullable: false),
                    NumberOfReportedCompletedScholarships = table.Column<int>(type: "int", nullable: false),
                    NumberOfReportedAbortedScholarships = table.Column<int>(type: "int", nullable: false),
                    NumberOfReportedNotAwardedScholarships = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAndReclaims", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_ReportAndReclaims_ApplicationAndEvaluations_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "ApplicationAndEvaluations",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipAndGrants",
                columns: table => new
                {
                    ScholarshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousApplication_Dnr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Project = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProjectYear = table.Column<int>(type: "int", nullable: false),
                    Applied_Year_Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reported_Year_Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Applied_Number_Of_Days = table.Column<int>(type: "int", nullable: false),
                    Reported_Number_Of_Days = table.Column<int>(type: "int", nullable: false),
                    NumberOfAppliedScholarships = table.Column<int>(type: "int", nullable: false),
                    NumberOfGrantedScholarships = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipAndGrants", x => x.ScholarshipId);
                    table.ForeignKey(
                        name: "FK_ScholarshipAndGrants_ApplicationAndEvaluations_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "ApplicationAndEvaluations",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ApplicationId",
                table: "Participants",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAndReclaims_ApplicationId",
                table: "ReportAndReclaims",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipAndGrants_ApplicationId",
                table: "ScholarshipAndGrants",
                column: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PreviousApplications");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "ReportAndReclaims");

            migrationBuilder.DropTable(
                name: "ScholarshipAndGrants");

            migrationBuilder.DropTable(
                name: "ApplicationAndEvaluations");
        }
    }
}
