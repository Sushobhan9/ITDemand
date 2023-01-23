using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItDemand.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessDrivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProcessL1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProcessL1s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComplianceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DCUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItPlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItPlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItSegments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItSegments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bcc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Sent = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityRole = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersImpacted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersImpacted", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProcessL2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessProcessL1Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProcessL2s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProcessL2s_BusinessProcessL1s_BusinessProcessL1Id",
                        column: x => x.BusinessProcessL1Id,
                        principalTable: "BusinessProcessL1s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItHeadId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessUnits_Users_ItHeadId",
                        column: x => x.ItHeadId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    WorkflowItemType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    SequenceNumber = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowItems_WorkflowItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "WorkflowItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkflowItems_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProcessL3s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessProcessL2Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProcessL3s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProcessL3s_BusinessProcessL2s_BusinessProcessL2Id",
                        column: x => x.BusinessProcessL2Id,
                        principalTable: "BusinessProcessL2s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowItemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GateReviewDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplates_WorkflowItems_WorkflowItemId",
                        column: x => x.WorkflowItemId,
                        principalTable: "WorkflowItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProblemStatement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: true),
                    ProcessAreaId = table.Column<int>(type: "int", nullable: true),
                    UsersImpactedId = table.Column<int>(type: "int", nullable: true),
                    BusinessDriverId = table.Column<int>(type: "int", nullable: true),
                    BenefitsPerYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BenefitDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BenefitStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    BusinessBenefit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsequenceNotImplemented = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeSummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestOwnerId = table.Column<int>(type: "int", nullable: true),
                    RequestSponsorId = table.Column<int>(type: "int", nullable: true),
                    ProjectManagerId = table.Column<int>(type: "int", nullable: true),
                    EstimatedInternalEffort = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EstimatedTotalCost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationTypeId = table.Column<int>(type: "int", nullable: true),
                    ApplicationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRedCab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalScope = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterfaceChanges = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityAssessment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalValidationReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItHeadId = table.Column<int>(type: "int", nullable: true),
                    SubmittedForReview = table.Column<bool>(type: "bit", nullable: false),
                    SubmittedForReviewDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    SubmittedById = table.Column<int>(type: "int", nullable: true),
                    DemandState = table.Column<int>(type: "int", nullable: false),
                    ExecutionType = table.Column<int>(type: "int", nullable: true),
                    RequestCorrections = table.Column<bool>(type: "bit", nullable: false),
                    RequestCorrectionsComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestCorrectionsDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RequestCorrectionsById = table.Column<int>(type: "int", nullable: true),
                    PmoReviewById = table.Column<int>(type: "int", nullable: true),
                    PmoReviewedOnDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PmoReviewComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewApplication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProposedPlatformId = table.Column<int>(type: "int", nullable: true),
                    DecommissionRequired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedSmeId = table.Column<int>(type: "int", nullable: true),
                    ItSegmentId = table.Column<int>(type: "int", nullable: true),
                    ArchitectureRelevant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PowerSteeringId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DcuId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    KeyProject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FTEsAssigned = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutiveShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessProcessL1Id = table.Column<int>(type: "int", nullable: true),
                    BusinessProcessL2Id = table.Column<int>(type: "int", nullable: true),
                    BusinessProcessL3Id = table.Column<int>(type: "int", nullable: true),
                    Methodology = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Digital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Replicated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectRepositoryLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectPhase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverallStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaselineCapex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaselineOpex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostItPerYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualCapex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualOpex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EacCapex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EacOpex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualCapexCurrentYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualOpexCurrentYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EacCapexCurrentYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EacOpexCurrentYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyMessages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopIssues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopRisks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccomplishedLastPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlannedNextPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyMilestones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MilestonePlanDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    MilestoneActualDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CancelledById = table.Column<int>(type: "int", nullable: true),
                    CancelledOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedById = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ComplianceItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandRequests_ApplicationTypes_ApplicationTypeId",
                        column: x => x.ApplicationTypeId,
                        principalTable: "ApplicationTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_BusinessDrivers_BusinessDriverId",
                        column: x => x.BusinessDriverId,
                        principalTable: "BusinessDrivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_BusinessProcessL1s_BusinessProcessL1Id",
                        column: x => x.BusinessProcessL1Id,
                        principalTable: "BusinessProcessL1s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_BusinessProcessL2s_BusinessProcessL2Id",
                        column: x => x.BusinessProcessL2Id,
                        principalTable: "BusinessProcessL2s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_BusinessProcessL3s_BusinessProcessL3Id",
                        column: x => x.BusinessProcessL3Id,
                        principalTable: "BusinessProcessL3s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_BusinessUnits_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_ComplianceItems_ComplianceItemId",
                        column: x => x.ComplianceItemId,
                        principalTable: "ComplianceItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_DCUs_DcuId",
                        column: x => x.DcuId,
                        principalTable: "DCUs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_ItPlatforms_ProposedPlatformId",
                        column: x => x.ProposedPlatformId,
                        principalTable: "ItPlatforms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_ItSegments_ItSegmentId",
                        column: x => x.ItSegmentId,
                        principalTable: "ItSegments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_ProcessAreas_ProcessAreaId",
                        column: x => x.ProcessAreaId,
                        principalTable: "ProcessAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_AssignedSmeId",
                        column: x => x.AssignedSmeId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_CancelledById",
                        column: x => x.CancelledById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_ItHeadId",
                        column: x => x.ItHeadId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_PmoReviewById",
                        column: x => x.PmoReviewById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_RequestCorrectionsById",
                        column: x => x.RequestCorrectionsById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_RequestOwnerId",
                        column: x => x.RequestOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_RequestSponsorId",
                        column: x => x.RequestSponsorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Users_SubmittedById",
                        column: x => x.SubmittedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_UsersImpacted_UsersImpactedId",
                        column: x => x.UsersImpactedId,
                        principalTable: "UsersImpacted",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemandRequests_Workflows_ExecutionType",
                        column: x => x.ExecutionType,
                        principalTable: "Workflows",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplateApprovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChecklistTemplateId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplateApprovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplateApprovers_ChecklistTemplates_ChecklistTemplateId",
                        column: x => x.ChecklistTemplateId,
                        principalTable: "ChecklistTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistTemplateQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChecklistTemplateId = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HelpText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcceptedAnswers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomChoices = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistTemplateQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistTemplateQuestions_ChecklistTemplates_ChecklistTemplateId",
                        column: x => x.ChecklistTemplateId,
                        principalTable: "ChecklistTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandRequestId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contents = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_DemandRequests_DemandRequestId",
                        column: x => x.DemandRequestId,
                        principalTable: "DemandRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemandRequestId = table.Column<int>(type: "int", nullable: false),
                    ChecklistTemplateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SequenceNumber = table.Column<double>(type: "float", nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkflowItemId = table.Column<int>(type: "int", nullable: false),
                    AdditionalComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GateReviewDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewComments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklists_ChecklistTemplates_ChecklistTemplateId",
                        column: x => x.ChecklistTemplateId,
                        principalTable: "ChecklistTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checklists_DemandRequests_DemandRequestId",
                        column: x => x.DemandRequestId,
                        principalTable: "DemandRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checklists_WorkflowItems_WorkflowItemId",
                        column: x => x.WorkflowItemId,
                        principalTable: "WorkflowItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandRequestBusinessUnits",
                columns: table => new
                {
                    DemandRequestId = table.Column<int>(type: "int", nullable: false),
                    BusinessUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandRequestBusinessUnits", x => new { x.DemandRequestId, x.BusinessUnitId });
                    table.ForeignKey(
                        name: "FK_DemandRequestBusinessUnits_BusinessUnits_BusinessUnitId",
                        column: x => x.BusinessUnitId,
                        principalTable: "BusinessUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandRequestBusinessUnits_DemandRequests_DemandRequestId",
                        column: x => x.DemandRequestId,
                        principalTable: "DemandRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandRequestComplianceItems",
                columns: table => new
                {
                    DemandRequestId = table.Column<int>(type: "int", nullable: false),
                    ComplianceItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandRequestComplianceItems", x => new { x.DemandRequestId, x.ComplianceItemId });
                    table.ForeignKey(
                        name: "FK_DemandRequestComplianceItems_ComplianceItems_ComplianceItemId",
                        column: x => x.ComplianceItemId,
                        principalTable: "ComplianceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandRequestComplianceItems_DemandRequests_DemandRequestId",
                        column: x => x.DemandRequestId,
                        principalTable: "DemandRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistApprovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChecklistId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: true),
                    ApproverId = table.Column<int>(type: "int", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistApprovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistApprovers_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistApprovers_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChecklistQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChecklistId = table.Column<int>(type: "int", nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HelpText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcceptedAnswers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomChoices = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistQuestions_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CreatedById",
                table: "Attachments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_DemandRequestId",
                table: "Attachments",
                column: "DemandRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProcessL2s_BusinessProcessL1Id",
                table: "BusinessProcessL2s",
                column: "BusinessProcessL1Id");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProcessL3s_BusinessProcessL2Id",
                table: "BusinessProcessL3s",
                column: "BusinessProcessL2Id");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnits_ItHeadId",
                table: "BusinessUnits",
                column: "ItHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistApprovers_ApproverId",
                table: "ChecklistApprovers",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistApprovers_ChecklistId",
                table: "ChecklistApprovers",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistQuestions_ChecklistId",
                table: "ChecklistQuestions",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_ChecklistTemplateId",
                table: "Checklists",
                column: "ChecklistTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_DemandRequestId",
                table: "Checklists",
                column: "DemandRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_WorkflowItemId",
                table: "Checklists",
                column: "WorkflowItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplateApprovers_ChecklistTemplateId",
                table: "ChecklistTemplateApprovers",
                column: "ChecklistTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplateQuestions_ChecklistTemplateId",
                table: "ChecklistTemplateQuestions",
                column: "ChecklistTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistTemplates_WorkflowItemId",
                table: "ChecklistTemplates",
                column: "WorkflowItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequestBusinessUnits_BusinessUnitId",
                table: "DemandRequestBusinessUnits",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequestComplianceItems_ComplianceItemId",
                table: "DemandRequestComplianceItems",
                column: "ComplianceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ApplicationTypeId",
                table: "DemandRequests",
                column: "ApplicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_AssignedSmeId",
                table: "DemandRequests",
                column: "AssignedSmeId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_BusinessDriverId",
                table: "DemandRequests",
                column: "BusinessDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_BusinessProcessL1Id",
                table: "DemandRequests",
                column: "BusinessProcessL1Id");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_BusinessProcessL2Id",
                table: "DemandRequests",
                column: "BusinessProcessL2Id");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_BusinessProcessL3Id",
                table: "DemandRequests",
                column: "BusinessProcessL3Id");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_BusinessUnitId",
                table: "DemandRequests",
                column: "BusinessUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_CancelledById",
                table: "DemandRequests",
                column: "CancelledById");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ComplianceItemId",
                table: "DemandRequests",
                column: "ComplianceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_CountryId",
                table: "DemandRequests",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_CreatedById",
                table: "DemandRequests",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_DcuId",
                table: "DemandRequests",
                column: "DcuId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ExecutionType",
                table: "DemandRequests",
                column: "ExecutionType");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ItHeadId",
                table: "DemandRequests",
                column: "ItHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ItSegmentId",
                table: "DemandRequests",
                column: "ItSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ModifiedById",
                table: "DemandRequests",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_PmoReviewById",
                table: "DemandRequests",
                column: "PmoReviewById");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ProcessAreaId",
                table: "DemandRequests",
                column: "ProcessAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ProjectManagerId",
                table: "DemandRequests",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_ProposedPlatformId",
                table: "DemandRequests",
                column: "ProposedPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_RequestCorrectionsById",
                table: "DemandRequests",
                column: "RequestCorrectionsById");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_RequestOwnerId",
                table: "DemandRequests",
                column: "RequestOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_RequestSponsorId",
                table: "DemandRequests",
                column: "RequestSponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_SubmittedById",
                table: "DemandRequests",
                column: "SubmittedById");

            migrationBuilder.CreateIndex(
                name: "IX_DemandRequests_UsersImpactedId",
                table: "DemandRequests",
                column: "UsersImpactedId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItems_ParentId",
                table: "WorkflowItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowItems_WorkflowId",
                table: "WorkflowItems",
                column: "WorkflowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "ChecklistApprovers");

            migrationBuilder.DropTable(
                name: "ChecklistQuestions");

            migrationBuilder.DropTable(
                name: "ChecklistTemplateApprovers");

            migrationBuilder.DropTable(
                name: "ChecklistTemplateQuestions");

            migrationBuilder.DropTable(
                name: "DemandRequestBusinessUnits");

            migrationBuilder.DropTable(
                name: "DemandRequestComplianceItems");

            migrationBuilder.DropTable(
                name: "MailItems");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "ChecklistTemplates");

            migrationBuilder.DropTable(
                name: "DemandRequests");

            migrationBuilder.DropTable(
                name: "WorkflowItems");

            migrationBuilder.DropTable(
                name: "ApplicationTypes");

            migrationBuilder.DropTable(
                name: "BusinessDrivers");

            migrationBuilder.DropTable(
                name: "BusinessProcessL3s");

            migrationBuilder.DropTable(
                name: "BusinessUnits");

            migrationBuilder.DropTable(
                name: "ComplianceItems");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "DCUs");

            migrationBuilder.DropTable(
                name: "ItPlatforms");

            migrationBuilder.DropTable(
                name: "ItSegments");

            migrationBuilder.DropTable(
                name: "ProcessAreas");

            migrationBuilder.DropTable(
                name: "UsersImpacted");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropTable(
                name: "BusinessProcessL2s");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BusinessProcessL1s");
        }
    }
}
