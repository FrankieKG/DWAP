using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    /// <inheritdoc />
    public partial class Rauk : Migration
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
                    FrameCaseNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Period = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PeriodDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archived_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Accompanying_SupportStaff = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exchange_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weighted_QualityPoints_BudgetView = table.Column<float>(type: "real", nullable: true),
                    Average_TotalPoints_Application = table.Column<float>(type: "real", nullable: true),
                    PointDifference_ApplicationView = table.Column<float>(type: "real", nullable: true),
                    Weighted_AveragePoints = table.Column<float>(type: "real", nullable: true),
                    AverageRating = table.Column<float>(type: "real", nullable: true),
                    PointDifference = table.Column<float>(type: "real", nullable: true),
                    QualityPoints_Report = table.Column<int>(type: "int", nullable: true),
                    PreviousApplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    OrganizationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrganizationEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plus_Bankgiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Applied_Participant_Number = table.Column<int>(type: "int", nullable: true),
                    Granted_Participant_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Participant_Number = table.Column<int>(type: "int", nullable: true),
                    Applied_Staff_Teacher_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Staff_Teacher_Number = table.Column<int>(type: "int", nullable: true),
                    Applied_Student_Number = table.Column<int>(type: "int", nullable: true),
                    Approved_Student_Number = table.Column<int>(type: "int", nullable: true),
                    Granted_Student_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Student_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Women_Student_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Men_Student_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Women_Teacher_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Men_Teacher_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Women_SchoolLeader_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Men_SchoolLeader_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Women_AssociatedStaff_Number = table.Column<int>(type: "int", nullable: true),
                    Reported_Men_AssociatedStaff_Number = table.Column<int>(type: "int", nullable: true),
                    Applied_Staff_Number = table.Column<int>(type: "int", nullable: true),
                    Granted_Staff_Number = table.Column<int>(type: "int", nullable: true),
                    Approved_Staff_Number = table.Column<int>(type: "int", nullable: true),
                    Repported_Staff_Number = table.Column<int>(type: "int", nullable: true),
                    Accompanying_Support_Staff = table.Column<int>(type: "int", nullable: true),
                    Dnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentAmount = table.Column<int>(type: "int", nullable: true),
                    Total_Applied_Amount = table.Column<int>(type: "int", nullable: true),
                    Total_Granted_Amount = table.Column<int>(type: "int", nullable: true),
                    Total_Reported_Amount = table.Column<int>(type: "int", nullable: true),
                    Total_Approved_Amount = table.Column<int>(type: "int", nullable: true),
                    Applied_Amount_ExtraFunds = table.Column<int>(type: "int", nullable: true),
                    Granted_Amount_ExtraFunds = table.Column<int>(type: "int", nullable: true),
                    Reported_Amount_ExtraFunds = table.Column<int>(type: "int", nullable: true),
                    Approved_Adjusted_Amount_ExtraFunds = table.Column<int>(type: "int", nullable: true),
                    Applied_AuditGrant = table.Column<int>(type: "int", nullable: true),
                    Granted_AuditGrant = table.Column<int>(type: "int", nullable: true),
                    Applied_AdminGrant = table.Column<int>(type: "int", nullable: true),
                    Granted_AdminGrant = table.Column<int>(type: "int", nullable: true),
                    Applied_Amount_Scholarships = table.Column<int>(type: "int", nullable: true),
                    Granted_Amount_Scholarships = table.Column<int>(type: "int", nullable: true),
                    Approved_Amount_Scholarships = table.Column<int>(type: "int", nullable: true),
                    Dnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "PreviousApplications",
                columns: table => new
                {
                    Dnr = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PreviousDnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousApplications", x => x.Dnr);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EducationalProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weeks = table.Column<int>(type: "int", nullable: true),
                    PartnerSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerSchool_EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Applied_Audit_Contribution = table.Column<int>(type: "int", nullable: true),
                    Granted_Audit_Contribution = table.Column<int>(type: "int", nullable: true),
                    Applied_Administrative_Contribution = table.Column<int>(type: "int", nullable: true),
                    Granted_Administrative_Contribution = table.Column<int>(type: "int", nullable: true),
                    Dnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "ReportAndReclaims",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportStatusDate = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: true),
                    Date_when_ReportStatus_Set = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status_Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reclaim_Paid_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reclaim_Amount = table.Column<int>(type: "int", nullable: true),
                    Reclaim_Paid_Amount = table.Column<int>(type: "int", nullable: true),
                    Reclaim_Created_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberOfReportedScholarships = table.Column<int>(type: "int", nullable: true),
                    NumberOfReportedCompletedScholarships = table.Column<int>(type: "int", nullable: true),
                    NumberOfReportedAbortedScholarships = table.Column<int>(type: "int", nullable: true),
                    NumberOfReportedNotAwardedScholarships = table.Column<int>(type: "int", nullable: true),
                    Dnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAndReclaims", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipAndGrants",
                columns: table => new
                {
                    ScholarshipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousApplication_Dnr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Project = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProjectYear = table.Column<int>(type: "int", nullable: true),
                    Applied_Year_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reported_Year_Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Applied_Number_Of_Days = table.Column<int>(type: "int", nullable: true),
                    Reported_Number_Of_Days = table.Column<int>(type: "int", nullable: true),
                    NumberOfAppliedScholarships = table.Column<int>(type: "int", nullable: true),
                    NumberOfGrantedScholarships = table.Column<int>(type: "int", nullable: true),
                    Applied_Scholarship_Amount = table.Column<int>(type: "int", nullable: true),
                    Approved_Scholarship_Amount = table.Column<int>(type: "int", nullable: true),
                    Granted_Scholarship_Amount = table.Column<int>(type: "int", nullable: true),
                    NumberOfScholarshipsAppliedFor = table.Column<int>(type: "int", nullable: true),
                    NumberOfScholarshipsGranted = table.Column<int>(type: "int", nullable: true),
                    Dnr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipAndGrants", x => x.ScholarshipId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationAndEvaluations");

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
        }
    }
}
