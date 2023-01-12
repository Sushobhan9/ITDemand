using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;
using ItDemand.Domain.Properties;
using ItDemand.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System.Resources;

// Notes:
// 1. Since we are basing the data context on an existing database we specifically
// insert the identify (primary key) to match the data that already exists to make
// the migration easier.

namespace ItDemand.Domain.DataContext
{
	public static class ItDemandInitializer
	{
		public static void Seed(this ItDemandContext context)
		{
			if (!context.ApplicationTypes.Any())
			{
				var applicationTypes = new List<ApplicationType>
				{
					new ApplicationType { Id = 1,  Name = "Healthcare @HOME SUITE", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 2,  Name = "Healthcare LMD SUITE", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 3,  Name = "SAP ECC BOCT", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 4,  Name = "SAP ECC IMPRESS", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 5,  Name = "SAP ECC INTOUCH", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 6,  Name = "SAP ECC LT", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 7,  Name = "SAP ECC HR SAPHRON", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 8,  Name = "SAP SRM GLOBAL", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 9,  Name = "SAP NETWEAVER PO", Description = "", Active = false, SortOrder = 0 },
					new ApplicationType { Id = 10, Name = "SAP Analytics SAP BW BOCT", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 11, Name = "SAP Analytics SAP BW CORE", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 12, Name = "SAP Analytics SAP BW GLOBAL", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 13, Name = "SAP Analytics SAP BW IMPRESS", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 14, Name = "SAP Analytics SAP BW INTOUCH", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 15, Name = "SAP Analytics SAP BW LT", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 16, Name = "Bulk GOLD SUITE", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 17, Name = "Bulk LCS SUITE", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 18, Name = "Bulk BSCM OPTIMIZER", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 19, Name = "TRACKABOUT", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 20, Name = "INLABEL", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 21, Name = "LIMA", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 22, Name = "PARAGON", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 23, Name = "SDL TRIDION WCMS", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 24, Name = "JDE ERP", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 25, Name = "HYPERION GLOBAL CONSOLIDATION", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 26, Name = "Other Corporate IT Supported Application", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 27, Name = "Local", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 28, Name = "New Corporate Application", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 29, Name = "SAP Ariba", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 30, Name = "SAP MDG-S", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 31, Name = "SAP PI/ PO", Description = "", Active = true, SortOrder = 0 },
					new ApplicationType { Id = 32, Name = "SiteCore", Description = "", Active = true, SortOrder = 0 },
				};

				applicationTypes.ForEach(s => context.ApplicationTypes.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ApplicationTypes ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ApplicationTypes OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}

				// If we are creating Application Type entries, then the DB has not yet
				// been created, so wire up the Security Roles.
				InitializeSecurityRoles(context);
			}

			if (!context.BusinessDrivers.Any())
			{
				var businessDrivers = new List<BusinessDriver>
				{
					new BusinessDriver { Id = 1, Name = "Cost Avoidance / Risk Mitigation / Run the Business", Description = "", Active = true },
					new BusinessDriver { Id = 2, Name = "Cost Savings / Cost Reduction", Description = "", Active = true },
					new BusinessDriver { Id = 3, Name = "Growth (Price / Volume / Increase Capability)", Description = "", Active = true },
					new BusinessDriver { Id = 4, Name = "Strategic Investment", Description = "", Active = true },
					new BusinessDriver { Id = 5, Name = "TLS", Description = "", Active = true },
					new BusinessDriver { Id = 6, Name = "Not Applicable", Description = "", Active = true }
				};

				businessDrivers.ForEach(s => context.BusinessDrivers.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessDrivers ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessDrivers OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.BusinessProcessL1s.Any())
			{
				var businessProcessL1s = new List<BusinessProcessL1>
				{
					new BusinessProcessL1 { Id = 1, Name = "Human Resources", Description = "", Active = true },
					new BusinessProcessL1 { Id = 2, Name = "Procurement", Description = "", Active = true },
					new BusinessProcessL1 { Id = 3, Name = "Finance & Controlling", Description = "", Active = true },
					new BusinessProcessL1 { Id = 4, Name = "Data to Report", Description = "", Active = true },
					new BusinessProcessL1 { Id = 5, Name = "Consumer Facing", Description = "", Active = true },
					new BusinessProcessL1 { Id = 6, Name = "Concept to Market", Description = "", Active = true },
					new BusinessProcessL1 { Id = 7, Name = "Customer Management", Description = "", Active = true },
					new BusinessProcessL1 { Id = 8, Name = "Order to Cash", Description = "", Active = true },
					new BusinessProcessL1 { Id = 9, Name = "Supply Chain - Generic", Description = "", Active = true },
					new BusinessProcessL1 { Id = 10, Name = "Supply Chain - Cylinder & Hardgoods", Description = "", Active = true },
					new BusinessProcessL1 { Id = 11, Name = "Supply Chain - Bulk", Description = "", Active = true },
					new BusinessProcessL1 { Id = 12, Name = "Business Management", Description = "", Active = true },
					new BusinessProcessL1 { Id = 13, Name = "Security Services", Description = "", Active = true },
					new BusinessProcessL1 { Id = 14, Name = "Client Services", Description = "", Active = true },
					new BusinessProcessL1 { Id = 15, Name = "Connectivity & Networking Services", Description = "", Active = true },
					new BusinessProcessL1 { Id = 16, Name = "Collaboration Services", Description = "", Active = true },
					new BusinessProcessL1 { Id = 17, Name = "Data Center Services", Description = "", Active = true },
					new BusinessProcessL1 { Id = 18, Name = "Information Insight", Description = "", Active = true },
					new BusinessProcessL1 { Id = 19, Name = "Information Management", Description = "", Active = true },
					new BusinessProcessL1 { Id = 20, Name = "Information Integration", Description = "", Active = true },
					new BusinessProcessL1 { Id = 21, Name = "Process Orchestration", Description = "", Active = true },
					new BusinessProcessL1 { Id = 22, Name = "Service Management", Description = "", Active = true },
					new BusinessProcessL1 { Id = 23, Name = "Service Delivery", Description = "", Active = true },
					new BusinessProcessL1 { Id = 24, Name = "Service Operations", Description = "", Active = true }
				};

				businessProcessL1s.ForEach(s => context.BusinessProcessL1s.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessProcessL1s ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessProcessL1s OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.BusinessProcessL2s.Any())
			{
				var businessProcessL2s = new List<BusinessProcessL2>
				{
					new BusinessProcessL2 { Id = 1,   BusinessProcessL1Id = 1,  Name = "Emp & Mgr.Self - service", Description = "", Active = true },
					new BusinessProcessL2 { Id = 2,   BusinessProcessL1Id = 1,  Name = "eLearning", Description = "", Active = true },
					new BusinessProcessL2 { Id = 3,   BusinessProcessL1Id = 1,  Name = "Recruiting & On - boarding", Description = "", Active = true },
					new BusinessProcessL2 { Id = 4,   BusinessProcessL1Id = 1,  Name = "Travel & Expense", Description = "", Active = true },
					new BusinessProcessL2 { Id = 5,   BusinessProcessL1Id = 1,  Name = "Time & Attendance", Description = "", Active = true },
					new BusinessProcessL2 { Id = 6,   BusinessProcessL1Id = 1,  Name = "Payroll", Description = "", Active = true },
					new BusinessProcessL2 { Id = 7,   BusinessProcessL1Id = 1,  Name = "Individual Sales Compensation", Description = "", Active = true },
					new BusinessProcessL2 { Id = 8,   BusinessProcessL1Id = 1,  Name = "Employee & Organisation Data Administration", Description = "", Active = true },
					new BusinessProcessL2 { Id = 9,   BusinessProcessL1Id = 1,  Name = "Employee Benefits &Recognition", Description = "", Active = true },
					new BusinessProcessL2 { Id = 10,  BusinessProcessL1Id = 2,  Name = "Strategic Sourcing", Description = "", Active = true },													  
					new BusinessProcessL2 { Id = 11,  BusinessProcessL1Id = 2,  Name = "Direct Procurement", Description = "", Active = true },
					new BusinessProcessL2 { Id = 12,  BusinessProcessL1Id = 2,  Name = "Supplier Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 13,  BusinessProcessL1Id = 2,  Name = "Supplier Self Service", Description = "", Active = true },
					new BusinessProcessL2 { Id = 14,  BusinessProcessL1Id = 2,  Name = "Indirect Procurement", Description = "", Active = true },
					new BusinessProcessL2 { Id = 15,  BusinessProcessL1Id = 2,  Name = "Material Master", Description = "", Active = true },
					new BusinessProcessL2 { Id = 16,  BusinessProcessL1Id = 2,  Name = "Reporting", Description = "", Active = true },
					new BusinessProcessL2 { Id = 17,  BusinessProcessL1Id = 2,  Name = "Invoice to Pay", Description = "", Active = true },
					new BusinessProcessL2 { Id = 18,  BusinessProcessL1Id = 3,  Name = "Consolidation", Description = "", Active = true },
					new BusinessProcessL2 { Id = 19,  BusinessProcessL1Id = 3,  Name = "Treasury", Description = "", Active = true },
					new BusinessProcessL2 { Id = 20,  BusinessProcessL1Id = 3,  Name = "Reporting(IFRS & US - GAAP, P & L)", Description = "", Active = true },													  
					new BusinessProcessL2 { Id = 21,  BusinessProcessL1Id = 3,  Name = "Tax & Legal Accounting", Description = "", Active = true },
					new BusinessProcessL2 { Id = 22,  BusinessProcessL1Id = 3,  Name = "G / L Accounting & Intercompany", Description = "", Active = true },
					new BusinessProcessL2 { Id = 23,  BusinessProcessL1Id = 3,  Name = "Vendor Invoice Management, Reconciliation", Description = "", Active = true },
					new BusinessProcessL2 { Id = 24,  BusinessProcessL1Id = 3,  Name = "Billing & Invoicing", Description = "", Active = true },
					new BusinessProcessL2 { Id = 25,  BusinessProcessL1Id = 3,  Name = "Credit Collection, Reconciliation", Description = "", Active = true },
					new BusinessProcessL2 { Id = 26,  BusinessProcessL1Id = 3,  Name = "Customer Rating", Description = "", Active = true },
					new BusinessProcessL2 { Id = 27,  BusinessProcessL1Id = 3,  Name = "Asset Accounting", Description = "", Active = true },
					new BusinessProcessL2 { Id = 28,  BusinessProcessL1Id = 3,  Name = "Banking, Reconciliaiton and Clearing", Description = "", Active = true },
					new BusinessProcessL2 { Id = 29,  BusinessProcessL1Id = 3,  Name = "CAPEX Controlling(Budgeting & Planing)", Description = "", Active = true },
					new BusinessProcessL2 { Id = 30,  BusinessProcessL1Id = 3,  Name = "License Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 31,  BusinessProcessL1Id = 3,  Name = "Audit Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 32,  BusinessProcessL1Id = 4,  Name = "Vendor", Description = "", Active = true },
					new BusinessProcessL2 { Id = 33,  BusinessProcessL1Id = 4,  Name = "Materials", Description = "", Active = true },
					new BusinessProcessL2 { Id = 34,  BusinessProcessL1Id = 4,  Name = "Retail", Description = "", Active = true },
					new BusinessProcessL2 { Id = 35,  BusinessProcessL1Id = 4,  Name = "Consumer", Description = "", Active = true },
					new BusinessProcessL2 { Id = 36,  BusinessProcessL1Id = 4,  Name = "Financial", Description = "", Active = true },
					new BusinessProcessL2 { Id = 37,  BusinessProcessL1Id = 5,  Name = "Call Center Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 38,  BusinessProcessL1Id = 5,  Name = "Consumer Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 39,  BusinessProcessL1Id = 5,  Name = "Consumer Intelligence", Description = "", Active = true },
					new BusinessProcessL2 { Id = 40,  BusinessProcessL1Id = 5,  Name = "Consumer Facing -POS and Store Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 41,  BusinessProcessL1Id = 5,  Name = "Store Replenish", Description = "", Active = true },
					new BusinessProcessL2 { Id = 42,  BusinessProcessL1Id = 5,  Name = "Consumer Comm.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 43,  BusinessProcessL1Id = 5,  Name = "Loyalty and Promotions", Description = "", Active = true },
					new BusinessProcessL2 { Id = 44,  BusinessProcessL1Id = 6,  Name = "Ideations", Description = "", Active = true },
					new BusinessProcessL2 { Id = 45,  BusinessProcessL1Id = 6,  Name = "Innovation Cap. Mgmt", Description = "", Active = true },
					new BusinessProcessL2 { Id = 46,  BusinessProcessL1Id = 6,  Name = "Product Life Cycle Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 47,  BusinessProcessL1Id = 6,  Name = "Product Development", Description = "", Active = true },
					new BusinessProcessL2 { Id = 48,  BusinessProcessL1Id = 6,  Name = "Understand Customer Needs", Description = "", Active = true },
					new BusinessProcessL2 { Id = 49,  BusinessProcessL1Id = 6,  Name = "Develop Product Service Offering(PSO)", Description = "", Active = true },
					new BusinessProcessL2 { Id = 50,  BusinessProcessL1Id = 6,  Name = "Place Product Service Offering(PSO)", Description = "", Active = true },
					new BusinessProcessL2 { Id = 51,  BusinessProcessL1Id = 6,  Name = "Promote Product Service Offering(PSO)", Description = "", Active = true },
					new BusinessProcessL2 { Id = 52,  BusinessProcessL1Id = 6,  Name = "Product Launch", Description = "", Active = true },
					new BusinessProcessL2 { Id = 53,  BusinessProcessL1Id = 7,  Name = "Plan Sales Organisation", Description = "", Active = true },
					new BusinessProcessL2 { Id = 54,  BusinessProcessL1Id = 7,  Name = "Acquire New Customers/ Business", Description = "", Active = true },
					new BusinessProcessL2 { Id = 55,  BusinessProcessL1Id = 7,  Name = "Manage Customer Accounts", Description = "", Active = true },
					new BusinessProcessL2 { Id = 56,  BusinessProcessL1Id = 7,  Name = "Assist with Customer Queries", Description = "", Active = true },
					new BusinessProcessL2 { Id = 57,  BusinessProcessL1Id = 7,  Name = "Enable Customer Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 58,  BusinessProcessL1Id = 8,  Name = "Order Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 59,  BusinessProcessL1Id = 8,  Name = "Manage Customer Assets", Description = "", Active = true },
					new BusinessProcessL2 { Id = 60,  BusinessProcessL1Id = 8,  Name = "Bill - to - Cash", Description = "", Active = true },
					new BusinessProcessL2 { Id = 61,  BusinessProcessL1Id = 8,  Name = "Retail Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 62,  BusinessProcessL1Id = 8,  Name = "Customer Self Service", Description = "", Active = true },
					new BusinessProcessL2 { Id = 63,  BusinessProcessL1Id = 8,  Name = "Trade Promotion", Description = "", Active = true },
					new BusinessProcessL2 { Id = 64,  BusinessProcessL1Id = 8,  Name = "B2B", Description = "", Active = true },
					new BusinessProcessL2 { Id = 65,  BusinessProcessL1Id = 8,  Name = "Pricing Strategy", Description = "", Active = true },
					new BusinessProcessL2 { Id = 66,  BusinessProcessL1Id = 9,  Name = "Demand Planning", Description = "", Active = true },
					new BusinessProcessL2 { Id = 67,  BusinessProcessL1Id = 9,  Name = "Supply Chain Planning", Description = "", Active = true },
					new BusinessProcessL2 { Id = 68,  BusinessProcessL1Id = 9,  Name = "Quote Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 69,  BusinessProcessL1Id = 9,  Name = "Inventory Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 70,  BusinessProcessL1Id = 9,  Name = "Product Execution", Description = "", Active = true },
					new BusinessProcessL2 { Id = 71,  BusinessProcessL1Id = 9,  Name = "Quality Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 72,  BusinessProcessL1Id = 9,  Name = "Manufact.Ops Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 73,  BusinessProcessL1Id = 9,  Name = "3rd Party Mfg.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 74,  BusinessProcessL1Id = 9,  Name = "Plant Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 75,  BusinessProcessL1Id = 10,  Name = "Plan Supply Chain", Description = "", Active = true },
					new BusinessProcessL2 { Id = 76,  BusinessProcessL1Id = 10,  Name = "Source Products", Description = "", Active = true },
					new BusinessProcessL2 { Id = 77,  BusinessProcessL1Id = 10,  Name = "Make Products", Description = "", Active = true },
					new BusinessProcessL2 { Id = 78,  BusinessProcessL1Id = 10,  Name = "Deliver Products", Description = "", Active = true },
					new BusinessProcessL2 { Id = 79,  BusinessProcessL1Id = 10,  Name = "Return Defective Products", Description = "", Active = true },
					new BusinessProcessL2 { Id = 80,  BusinessProcessL1Id = 11,  Name = "Plan Supply Chain", Description = "", Active = true },
					new BusinessProcessL2 { Id = 81,  BusinessProcessL1Id = 11,  Name = "Source Supply Chain", Description = "", Active = true },
					new BusinessProcessL2 { Id = 82,  BusinessProcessL1Id = 11,  Name = "Make Products", Description = "", Active = true },
					new BusinessProcessL2 { Id = 83,  BusinessProcessL1Id = 11,  Name = "Deliver Products", Description = "", Active = true },
					new BusinessProcessL2 { Id = 84,  BusinessProcessL1Id = 11,  Name = "Customer Engineering Services", Description = "", Active = true },
					new BusinessProcessL2 { Id = 85,  BusinessProcessL1Id = 12,  Name = "Legal and Compliance", Description = "", Active = true },
					new BusinessProcessL2 { Id = 86,  BusinessProcessL1Id = 12,  Name = "Health and Safety", Description = "", Active = true },
					new BusinessProcessL2 { Id = 87,  BusinessProcessL1Id = 12,  Name = "Facility Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 88,  BusinessProcessL1Id = 12,  Name = "Corporate Communications", Description = "", Active = true },
					new BusinessProcessL2 { Id = 89,  BusinessProcessL1Id = 13,  Name = "Directory", Description = "", Active = true },
					new BusinessProcessL2 { Id = 90,  BusinessProcessL1Id = 13,  Name = "Identity Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 91,  BusinessProcessL1Id = 13,  Name = "Authentication", Description = "", Active = true },
					new BusinessProcessL2 { Id = 92,  BusinessProcessL1Id = 13,  Name = "Access Control", Description = "", Active = true },
					new BusinessProcessL2 { Id = 93,  BusinessProcessL1Id = 13,  Name = "Digital Signatures", Description = "", Active = true },
					new BusinessProcessL2 { Id = 94,  BusinessProcessL1Id = 13,  Name = "Information Rights Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 95,  BusinessProcessL1Id = 13,  Name = "Encryption", Description = "", Active = true },
					new BusinessProcessL2 { Id = 96,  BusinessProcessL1Id = 13,  Name = "Data Loss Prevention", Description = "", Active = true },
					new BusinessProcessL2 { Id = 97,  BusinessProcessL1Id = 13,  Name = "Virus Protection", Description = "", Active = true },
					new BusinessProcessL2 { Id = 98,  BusinessProcessL1Id = 13,  Name = "Compliance Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 99,  BusinessProcessL1Id = 13,  Name = "Intrusion Detection &Protection", Description = "", Active = true },
					new BusinessProcessL2 { Id = 100, BusinessProcessL1Id = 14,  Name = "Desktop & Laptops", Description = "", Active = true },
					new BusinessProcessL2 { Id = 101, BusinessProcessL1Id = 14,  Name = "Mobile & Hand - held", Description = "", Active = true },
					new BusinessProcessL2 { Id = 102, BusinessProcessL1Id = 14,  Name = "PBX / UC Devices", Description = "", Active = true },
					new BusinessProcessL2 { Id = 103, BusinessProcessL1Id = 14,  Name = "  Peripherals", Description = "", Active = true },
					new BusinessProcessL2 { Id = 104, BusinessProcessL1Id = 14,  Name = "Productivity Software", Description = "", Active = true },
					new BusinessProcessL2 { Id = 105, BusinessProcessL1Id = 14,  Name = "Personalization", Description = "", Active = true },
					new BusinessProcessL2 { Id = 106, BusinessProcessL1Id = 15,  Name = "Local Area Network", Description = "", Active = true },
					new BusinessProcessL2 { Id = 107, BusinessProcessL1Id = 15,  Name = "Wide Area Network", Description = "", Active = true },
					new BusinessProcessL2 { Id = 108, BusinessProcessL1Id = 15,  Name = "Wireless Network", Description = "", Active = true },
					new BusinessProcessL2 { Id = 109, BusinessProcessL1Id = 15,  Name = "Remote Access", Description = "", Active = true },
					new BusinessProcessL2 { Id = 110, BusinessProcessL1Id = 15,  Name = "Firewall", Description = "", Active = true },
					new BusinessProcessL2 { Id = 111, BusinessProcessL1Id = 16,  Name = "Conferencing", Description = "", Active = true },
					new BusinessProcessL2 { Id = 112, BusinessProcessL1Id = 16,  Name = "Enterprise Search", Description = "", Active = true },
					new BusinessProcessL2 { Id = 113, BusinessProcessL1Id = 16,  Name = "Portal", Description = "", Active = true },
					new BusinessProcessL2 { Id = 114, BusinessProcessL1Id = 16,  Name = "Social Networking", Description = "", Active = true },
					new BusinessProcessL2 { Id = 115, BusinessProcessL1Id = 16,  Name = "Communication", Description = "", Active = true },
					new BusinessProcessL2 { Id = 116, BusinessProcessL1Id = 17,  Name = "Servers", Description = "", Active = true },
					new BusinessProcessL2 { Id = 117, BusinessProcessL1Id = 17,  Name = "Virtualization", Description = "", Active = true },
					new BusinessProcessL2 { Id = 118, BusinessProcessL1Id = 17,  Name = "Storage", Description = "", Active = true },
					new BusinessProcessL2 { Id = 119, BusinessProcessL1Id = 17,  Name = "Print", Description = "", Active = true },
					new BusinessProcessL2 { Id = 120, BusinessProcessL1Id = 17,  Name = "Web", Description = "", Active = true },
					new BusinessProcessL2 { Id = 121, BusinessProcessL1Id = 17,  Name = "Databases", Description = "", Active = true },
					new BusinessProcessL2 { Id = 122, BusinessProcessL1Id = 17,  Name = "Archiving & Recovery", Description = "", Active = true },
					new BusinessProcessL2 { Id = 123, BusinessProcessL1Id = 17,  Name = "Operating System", Description = "", Active = true },
					new BusinessProcessL2 { Id = 124, BusinessProcessL1Id = 17,  Name = "Application Server", Description = "", Active = true },
					new BusinessProcessL2 { Id = 125, BusinessProcessL1Id = 17,  Name = "File & Data Transfer", Description = "", Active = true },
					new BusinessProcessL2 { Id = 126, BusinessProcessL1Id = 17,  Name = "Messaging", Description = "", Active = true },
					new BusinessProcessL2 { Id = 127, BusinessProcessL1Id = 17,  Name = "Voice Comm", Description = "", Active = true },
					new BusinessProcessL2 { Id = 128, BusinessProcessL1Id = 17,  Name = "Fax", Description = "", Active = true },
					new BusinessProcessL2 { Id = 129, BusinessProcessL1Id = 17,  Name = "Unified Comm", Description = "", Active = true },
					new BusinessProcessL2 { Id = 130, BusinessProcessL1Id = 18,  Name = "Reporting & Dashboards", Description = "", Active = true },
					new BusinessProcessL2 { Id = 131, BusinessProcessL1Id = 18,  Name = "Analytics", Description = "", Active = true },
					new BusinessProcessL2 { Id = 132, BusinessProcessL1Id = 18,  Name = "Data Warehouse", Description = "", Active = true },
					new BusinessProcessL2 { Id = 133, BusinessProcessL1Id = 18,  Name = "Knowledge Discovery", Description = "", Active = true },
					new BusinessProcessL2 { Id = 134, BusinessProcessL1Id = 19,  Name = "Content Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 135, BusinessProcessL1Id = 19,  Name = "Document Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 136, BusinessProcessL1Id = 19,  Name = "Digital Media Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 137, BusinessProcessL1Id = 19,  Name = "Records Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 138, BusinessProcessL1Id = 19,  Name = "Master Data Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 139, BusinessProcessL1Id = 19,  Name = "Knowledge Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 140, BusinessProcessL1Id = 19,  Name = "Info.Life Cycle Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 141, BusinessProcessL1Id = 19,  Name = "Meta Data Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 142, BusinessProcessL1Id = 19,  Name = "Info.Quality Assurance", Description = "", Active = true },
					new BusinessProcessL2 { Id = 143, BusinessProcessL1Id = 20,  Name = "Application Integration", Description = "", Active = true },
					new BusinessProcessL2 { Id = 144, BusinessProcessL1Id = 20,  Name = "Data Integration", Description = "", Active = true },
					new BusinessProcessL2 { Id = 145, BusinessProcessL1Id = 20,  Name = "Partner Integration", Description = "", Active = true },
					new BusinessProcessL2 { Id = 146, BusinessProcessL1Id = 21,  Name = "Process Automation", Description = "", Active = true },
					new BusinessProcessL2 { Id = 147, BusinessProcessL1Id = 21,  Name = "Event Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 148, BusinessProcessL1Id = 21,  Name = "Process Management", Description = "", Active = true },
					new BusinessProcessL2 { Id = 149, BusinessProcessL1Id = 22,  Name = "Strategic Planning", Description = "", Active = true },
					new BusinessProcessL2 { Id = 150, BusinessProcessL1Id = 22,  Name = "Enterprise Architecture Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 151, BusinessProcessL1Id = 22,  Name = "IT Governance", Description = "", Active = true },
					new BusinessProcessL2 { Id = 152, BusinessProcessL1Id = 22,  Name = "Demand Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 153, BusinessProcessL1Id = 22,  Name = "Financial Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 154, BusinessProcessL1Id = 22,  Name = "Work Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 155, BusinessProcessL1Id = 22,  Name = "Service Level Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 156, BusinessProcessL1Id = 23,  Name = "Change Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 157, BusinessProcessL1Id = 23,  Name = "Config Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 158, BusinessProcessL1Id = 23,  Name = "Release Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 159, BusinessProcessL1Id = 23,  Name = "Vendor Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 160, BusinessProcessL1Id = 23,  Name = "Project Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 161, BusinessProcessL1Id = 23,  Name = "Solution Development", Description = "", Active = true },
					new BusinessProcessL2 { Id = 162, BusinessProcessL1Id = 23,  Name = "SOE Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 163, BusinessProcessL1Id = 24,  Name = "Incident & Prob.Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 164, BusinessProcessL1Id = 24,  Name = "Service Desk &Assist.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 165, BusinessProcessL1Id = 24,  Name = "Monitoring", Description = "", Active = true },
					new BusinessProcessL2 { Id = 166, BusinessProcessL1Id = 24,  Name = "Deploy. & Patch Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 167, BusinessProcessL1Id = 24,  Name = "Systems Admin.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 168, BusinessProcessL1Id = 24,  Name = "Capacity Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 169, BusinessProcessL1Id = 24,  Name = "Availibility Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 170, BusinessProcessL1Id = 24,  Name = "Business Cont. Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 171, BusinessProcessL1Id = 24,  Name = "Asset Mgmt.", Description = "", Active = true },
					new BusinessProcessL2 { Id = 172, BusinessProcessL1Id = 24,  Name = "Scheduling", Description = "", Active = true },
					new BusinessProcessL2 { Id = 173, BusinessProcessL1Id = 24,  Name = "Service Validation & Testing", Description = "", Active = true }
				};

				businessProcessL2s.ForEach(s => context.BusinessProcessL2s.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessProcessL2s ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessProcessL2s OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.BusinessProcessL3s.Any())
			{
				var businessProcessL3s = new List<BusinessProcessL3>
				{
					new BusinessProcessL3 { Id = 1,  BusinessProcessL2Id = 48,  Name = "Segmentation of Customers", Description = "", Active = true },
					new BusinessProcessL3 { Id = 2,  BusinessProcessL2Id = 48,  Name = "Customer / Market Research", Description = "", Active = true },
					new BusinessProcessL3 { Id = 3,  BusinessProcessL2Id = 48,  Name = "Define Market Segment Strategy", Description = "", Active = true },
					new BusinessProcessL3 { Id = 4,  BusinessProcessL2Id = 49,  Name = "PSO Definition & Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 5,  BusinessProcessL2Id = 49,  Name = "Product Development", Description = "", Active = true },
					new BusinessProcessL3 { Id = 6,  BusinessProcessL2Id = 50,  Name = "Channel Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 7,  BusinessProcessL2Id = 51,  Name = "Marketing Campaign Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 8,  BusinessProcessL2Id = 53,  Name = "Sales Team Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 9,  BusinessProcessL2Id = 53,  Name = "Plan & Execute Pricing Lockdown", Description = "", Active = true },
					new BusinessProcessL3 { Id = 10, BusinessProcessL2Id = 54,  Name = "Lead & Opportunity Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 11, BusinessProcessL2Id = 54,  Name = "Customer Account Creation", Description = "", Active = true },
					new BusinessProcessL3 { Id = 12, BusinessProcessL2Id = 55,  Name = "Account Lifecycle Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 13, BusinessProcessL2Id = 55,  Name = "Pricing & Contract Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 14, BusinessProcessL2Id = 56,  Name = "Query Management & Customer Interaction", Description = "", Active = true },
					new BusinessProcessL3 { Id = 15, BusinessProcessL2Id = 57,  Name = "EDI & Interfaces", Description = "", Active = true },
					new BusinessProcessL3 { Id = 16, BusinessProcessL2Id = 57,  Name = "Sales Partner Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 17, BusinessProcessL2Id = 58,  Name = "Manage Quotation", Description = "", Active = true },
					new BusinessProcessL3 { Id = 18, BusinessProcessL2Id = 58,  Name = "Manage Orders", Description = "", Active = true },
					new BusinessProcessL3 { Id = 19, BusinessProcessL2Id = 59,  Name = "Manage Container Holdings", Description = "", Active = true },
					new BusinessProcessL3 { Id = 20, BusinessProcessL2Id = 59,  Name = "Manage Private Cylinder Assets", Description = "", Active = true },
					new BusinessProcessL3 { Id = 21, BusinessProcessL2Id = 59,  Name = "Calculate Rent", Description = "", Active = true },
					new BusinessProcessL3 { Id = 22, BusinessProcessL2Id = 60,  Name = "Customer Invoicing &Information", Description = "", Active = true },
					new BusinessProcessL3 { Id = 23, BusinessProcessL2Id = 60,  Name = "Customer Receivables &Risk Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 24, BusinessProcessL2Id = 60,  Name = "Tonnage Customer Management &Invoicing", Description = "", Active = true },
					new BusinessProcessL3 { Id = 25, BusinessProcessL2Id = 61,  Name = "Retail Direct Serve Customer", Description = "", Active = true },
					new BusinessProcessL3 { Id = 26, BusinessProcessL2Id = 61,  Name = "Retail Operations Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 27, BusinessProcessL2Id = 62,  Name = "Online Webshop", Description = "", Active = true },
					new BusinessProcessL3 { Id = 28, BusinessProcessL2Id = 62,  Name = "Customer Information Management &Retrieval", Description = "", Active = true },
					new BusinessProcessL3 { Id = 29, BusinessProcessL2Id = 62,  Name = "Online Payments", Description = "", Active = true },
					new BusinessProcessL3 { Id = 30, BusinessProcessL2Id = 62,  Name = "Customer Cylinder Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 31, BusinessProcessL2Id = 75,  Name = "Central Cylinder Planning", Description = "", Active = true },
					new BusinessProcessL3 { Id = 32, BusinessProcessL2Id = 75,  Name = "Sales & Operations Planning", Description = "", Active = true },
					new BusinessProcessL3 { Id = 33, BusinessProcessL2Id = 75,  Name = "Inventory Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 34, BusinessProcessL2Id = 75,  Name = "Material Handling", Description = "", Active = true },
					new BusinessProcessL3 { Id = 35, BusinessProcessL2Id = 75,  Name = "Product Portfolio Optimisation", Description = "", Active = true },
					new BusinessProcessL3 { Id = 36, BusinessProcessL2Id = 75,  Name = "Manage Supply Networks", Description = "", Active = true },
					new BusinessProcessL3 { Id = 37, BusinessProcessL2Id = 76,  Name = "3rd Party Filling & Maintenance", Description = "", Active = true },
					new BusinessProcessL3 { Id = 38, BusinessProcessL2Id = 76,  Name = "Source Hardgoods", Description = "", Active = true },
					new BusinessProcessL3 { Id = 39, BusinessProcessL2Id = 76,  Name = "Warehouse Management", Description = "", Active = true },
					new BusinessProcessL3 { Id = 40, BusinessProcessL2Id = 77,  Name = "Cylinder Filling", Description = "", Active = true },
					new BusinessProcessL3 { Id = 41, BusinessProcessL2Id = 77,  Name = "Cylinder & Accessory Maintenance", Description = "", Active = true },
					new BusinessProcessL3 { Id = 42, BusinessProcessL2Id = 77,  Name = "Hardgoods Manufacturing", Description = "", Active = true },
					new BusinessProcessL3 { Id = 43, BusinessProcessL2Id = 78,  Name = "Fleet Maintenance", Description = "", Active = true },
					new BusinessProcessL3 { Id = 44, BusinessProcessL2Id = 78,  Name = "Scheduling", Description = "", Active = true },
					new BusinessProcessL3 { Id = 45, BusinessProcessL2Id = 78,  Name = "Picking & Loading", Description = "", Active = true },
					new BusinessProcessL3 { Id = 46, BusinessProcessL2Id = 78,  Name = "Delivery Execution", Description = "", Active = true },
					new BusinessProcessL3 { Id = 47, BusinessProcessL2Id = 79,  Name = "Recall Defective Products", Description = "", Active = true },
					new BusinessProcessL3 { Id = 48, BusinessProcessL2Id = 79,  Name = "Customer Complaints Process(defective product)", Description = "", Active = true },
					new BusinessProcessL3 { Id = 49, BusinessProcessL2Id = 80,  Name = "Execute Operational Optimiser", Description = "", Active = true },
					new BusinessProcessL3 { Id = 50, BusinessProcessL2Id = 80,  Name = "Sales & Operations Planning", Description = "", Active = true },
					new BusinessProcessL3 { Id = 51, BusinessProcessL2Id = 80,  Name = "Plan & Execute Investments", Description = "", Active = true },
					new BusinessProcessL3 { Id = 52, BusinessProcessL2Id = 81,  Name = "Source Stocked Products", Description = "", Active = true },
					new BusinessProcessL3 { Id = 53, BusinessProcessL2Id = 82,  Name = "Operate Process Plants", Description = "", Active = true },
					new BusinessProcessL3 { Id = 54, BusinessProcessL2Id = 82,  Name = "Plant Maintenance", Description = "", Active = true },
					new BusinessProcessL3 { Id = 55, BusinessProcessL2Id = 83,  Name = "Schedule Deliveries", Description = "", Active = true },
					new BusinessProcessL3 { Id = 56, BusinessProcessL2Id = 83,  Name = "Pick & Load Vehicles", Description = "", Active = true },
					new BusinessProcessL3 { Id = 57, BusinessProcessL2Id = 83,  Name = "Deliver Products", Description = "", Active = true },
					new BusinessProcessL3 { Id = 58, BusinessProcessL2Id = 84,  Name = "Install / Remove Project Execution", Description = "", Active = true },
					new BusinessProcessL3 { Id = 59, BusinessProcessL2Id = 84,  Name = "Planned / Unplanned Maintenance", Description = "", Active = true }
				};

				businessProcessL3s.ForEach(s => context.BusinessProcessL3s.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessProcessL3s ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessProcessL3s OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.ComplianceItems.Any())
			{
				var complianceItems = new List<ComplianceItem>
				{
					new ComplianceItem { Id = 1, Name = "Not Applicable", Description = "", Active = true },
					new ComplianceItem { Id = 2, Name = "GDPR", Description = "", Active = true },
					new ComplianceItem { Id = 3, Name = "Medical Validation", Description = "", Active = true },
					new ComplianceItem { Id = 4, Name = "SOX", Description = "", Active = true },
					new ComplianceItem { Id = 5, Name = "Data Protection Relevant", Description = "", Active = true }
				};

				complianceItems.ForEach(s => context.ComplianceItems.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ComplianceItems ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ComplianceItems OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.Countries.Any())
			{
				var countries = new List<Country>
				{
					new Country { Id = 1,  Name = "Algeria", Description = "DZ", Active = true },
					new Country { Id = 2,  Name = "Argentina", Description = "AR", Active = true },
					new Country { Id = 3,  Name = "Aruba", Description = "AW", Active = true },
					new Country { Id = 4,  Name = "Austria", Description = "AT", Active = true },
					new Country { Id = 5,  Name = "Australia", Description = "AU", Active = true },
					new Country { Id = 6,  Name = "Bangladesh", Description = "BD", Active = true },
					new Country { Id = 7,  Name = "Belgium", Description = "BE", Active = true },
					new Country { Id = 8,  Name = "Bulgaria", Description = "BG", Active = true },
					new Country { Id = 9,  Name = "Bosina", Description = "BIH", Active = true },
					new Country { Id = 10, Name = "Brazil", Description = "BR", Active = true },
					new Country { Id = 11, Name = "Canada", Description = "CA", Active = true },
					new Country { Id = 12, Name = "Switzerland", Description = "CH", Active = true },
					new Country { Id = 13, Name = "Chile", Description = "CL", Active = true },
					new Country { Id = 14, Name = "China", Description = "CN", Active = true },
					new Country { Id = 15, Name = "Colombia", Description = "CO", Active = true },
					new Country { Id = 16, Name = "Costa Rica", Description = "CR", Active = true },
					new Country { Id = 17, Name = "Cyprus", Description = "CY", Active = true },
					new Country { Id = 18, Name = "Curacao", Description = "CW", Active = true },
					new Country { Id = 19, Name = "Czech Republic", Description = "CZ", Active = true },
					new Country { Id = 20, Name = "Germany", Description = "DE", Active = true },
					new Country { Id = 21, Name = "Denmark", Description = "DK", Active = true },
					new Country { Id = 22, Name = "Dominican Republic", Description = "DO", Active = true },
					new Country { Id = 23, Name = "Ecuador", Description = "EC", Active = true },
					new Country { Id = 24, Name = "Estonia", Description = "EE", Active = true },
					new Country { Id = 25, Name = "Spain", Description = "ES", Active = true },
					new Country { Id = 26, Name = "Finland", Description = "FI", Active = true },
					new Country { Id = 27, Name = "France", Description = "FR", Active = true },
					new Country { Id = 28, Name = "UK", Description = "GB", Active = true },
					new Country { Id = 29, Name = "Greece", Description = "GR", Active = true },
					new Country { Id = 30, Name = "Global", Description = "Global", Active = true },
					new Country { Id = 31, Name = "Hong Kong", Description = "HK", Active = true },
					new Country { Id = 32, Name = "Croatia", Description = "HR", Active = true },
					new Country { Id = 33, Name = "Hungary", Description = "HU", Active = true },
					new Country { Id = 34, Name = "Indonesia", Description = "ID", Active = true },
					new Country { Id = 35, Name = "Ireland", Description = "IE", Active = true },
					new Country { Id = 36, Name = "India", Description = "IN", Active = true },
					new Country { Id = 37, Name = "Iceland", Description = "IS", Active = true },
					new Country { Id = 38, Name = "Italy", Description = "IT", Active = true },
					new Country { Id = 39, Name = "Kenya", Description = "KE", Active = true },
					new Country { Id = 40, Name = "Korea (South)", Description = "KR", Active = true },
					new Country { Id = 41, Name = "Kazakhstan", Description = "KZ", Active = true },
					new Country { Id = 42, Name = "Sri Lanka", Description = "LK", Active = true },
					new Country { Id = 43, Name = "Lithuania", Description = "LT", Active = true },
					new Country { Id = 44, Name = "Luxembourg", Description = "LU", Active = true },
					new Country { Id = 45, Name = "Latvia", Description = "LV", Active = true },
					new Country { Id = 46, Name = "Malaysia", Description = "MY", Active = true },
					new Country { Id = 47, Name = "Mexico", Description = "MX", Active = true },
					new Country { Id = 48, Name = "Nigeria", Description = "NG", Active = true },
					new Country { Id = 49, Name = "Netherlands", Description = "NL", Active = true },
					new Country { Id = 50, Name = "Norway", Description = "NO", Active = true },
					new Country { Id = 51, Name = "New Zealand", Description = "NZ", Active = true },
					new Country { Id = 52, Name = "Panama", Description = "PA", Active = true },
					new Country { Id = 53, Name = "Peru", Description = "PE", Active = true },
					new Country { Id = 54, Name = "Philippines", Description = "PH", Active = true },
					new Country { Id = 55, Name = "Pakistan", Description = "PK", Active = true },
					new Country { Id = 56, Name = "Poland", Description = "PL", Active = true },
					new Country { Id = 57, Name = "Puerto Rico", Description = "PR", Active = true },
					new Country { Id = 58, Name = "Portugal", Description = "PT", Active = true },
					new Country { Id = 59, Name = "Romania", Description = "RO", Active = true },
					new Country { Id = 60, Name = "Russia", Description = "RU", Active = true },
					new Country { Id = 61, Name = "Saudi Arabia", Description = "SA", Active = true },
					new Country { Id = 62, Name = "Sweden", Description = "SE", Active = true },
					new Country { Id = 63, Name = "Singapore", Description = "SG", Active = true },
					new Country { Id = 64, Name = "Slovakia", Description = "SK", Active = true },
					new Country { Id = 65, Name = "Serbia", Description = "SR", Active = true },
					new Country { Id = 66, Name = "Slovenia", Description = "SI", Active = true },
					new Country { Id = 67, Name = "Thailand", Description = "TH", Active = true },
					new Country { Id = 68, Name = "Turkey", Description = "TR", Active = true },
					new Country { Id = 69, Name = "Ukraine", Description = "UA", Active = true },
					new Country { Id = 70, Name = "Utd.Arab Emirates", Description = "UE", Active = true },
					new Country { Id = 71, Name = "US", Description = "US", Active = true },
					new Country { Id = 72, Name = "Uruguay", Description = "UY", Active = true },
					new Country { Id = 73, Name = "Venezuela", Description = "VE", Active = true },
					new Country { Id = 74, Name = "Vietnam", Description = "VN", Active = true },
					new Country { Id = 75, Name = "South Africa", Description = "ZA", Active = true },
					new Country { Id = 76, Name = "Zimbabwe", Description = "ZW", Active = true }
				};

				countries.ForEach(s => context.Countries.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Countries ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Countries OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.DCUs.Any())
			{
				var dcus = new List<DCU>
				{
					new DCU { Id = 1,  Name = "100010", Description = "100010 - Unternehmenszentrale (Linde AG)", Active = true },
					new DCU { Id = 2,  Name = "111010", Description = "111010 - Geschäftsbereich Linde Engineering (Linde Aktiengesellschaf)", Active = true },
					new DCU { Id = 3,  Name = "206540", Description = "206540 - Geschäftsbereich Linde Gas (Linde AG)", Active = true },
					new DCU { Id = 4,  Name = "206550", Description = "206550 - GS-DEU-HQ", Active = true },
					new DCU { Id = 5,  Name = "206590", Description = "206590 - GS-DEU-EMEA", Active = true },
					new DCU { Id = 6,  Name = "222010", Description = "222010 - Tega-Technische Gase und Gasetechnik Gesellschaft mit beschr", Active = true },
					new DCU { Id = 7,  Name = "222130", Description = "222130 - Linde Electronics GmbH & Co. KG", Active = true },
					new DCU { Id = 8,  Name = "222230", Description = "222230 - Linde Gas Therapeutics GmbH", Active = true },
					new DCU { Id = 9,  Name = "305600", Description = "305600 - Linde Gas Tunisie S.A.", Active = true },
					new DCU { Id = 10, Name = "306580", Description = "306580 - Linde Finance B.V.", Active = true },
					new DCU { Id = 11, Name = "322010", Description = "322010 - Linde Hellas Monoprosopi E.P.E.", Active = true },
					new DCU { Id = 12, Name = "322070", Description = "322070 - Abelló Linde, S.A.U.", Active = true },
					new DCU { Id = 13, Name = "322110", Description = "322110 - LINDE PORTUGAL, LDA", Active = true },
					new DCU { Id = 14, Name = "322120", Description = "322120 - LINDE SAÚDE, LDA", Active = true },
					new DCU { Id = 15, Name = "322130", Description = "322130 - PU_Linde Gas S.A.", Active = true },
					new DCU { Id = 16, Name = "322180", Description = "322180 - LINDE GLOBAL SERVICES PORTUGAL, UNIPESSOAL LDA", Active = true },
					new DCU { Id = 17, Name = "322200", Description = "322200 - Linde Gas a.s.", Active = true },
					new DCU { Id = 18, Name = "322280", Description = "322280 - Linde Gas GmbH", Active = true },
					new DCU { Id = 19, Name = "322300", Description = "322300 - Linde Gáz Magyarország Zrt.", Active = true },
					new DCU { Id = 20, Name = "322350", Description = "322350 - LINDE GAZ POLSKA Spó?ka z o.o.", Active = true },
					new DCU { Id = 21, Name = "322410", Description = "322410 - LINDE MEDICALE Srl", Active = true },
					new DCU { Id = 22, Name = "322400", Description = "322400 - Linde Gas Italia S.r.l.", Active = true },
					new DCU { Id = 23, Name = "322540", Description = "322540 - Linde Homecare Belgium SPRL", Active = true },
					new DCU { Id = 24, Name = "322590", Description = "322590 - Linde Gas Benelux B.V.", Active = true },
					new DCU { Id = 25, Name = "322620", Description = "322620 - Linde Gas Belgium NV", Active = true },
					new DCU { Id = 26, Name = "322720", Description = "322720 - Linde Homecare Benelux B.V.", Active = true },
					new DCU { Id = 27, Name = "322770", Description = "322770 - Linde Gas Cryoservices B.V.", Active = true },
					new DCU { Id = 28, Name = "322800", Description = "322800 - PanGas AG", Active = true },
					new DCU { Id = 29, Name = "322960", Description = "322960 - Linde Gaz Bel FLLC - sold 06/06/2018", Active = true },
					new DCU { Id = 30, Name = "323000", Description = "323000 - TOO Linde Gaz Kazakhstan", Active = true },
					new DCU { Id = 31, Name = "323110", Description = "323110 - Saudi Industrial Gas Company", Active = true },
					new DCU { Id = 32, Name = "323120", Description = "323120 - LINDE GAS MIDDLE EAST LLC", Active = true },
					new DCU { Id = 33, Name = "323150", Description = "323150 - LINDE HEALTHCARE MIDDLE EAST LLC", Active = true },
					new DCU { Id = 34, Name = "323230", Description = "323230 - Linde Jubail Industrial Gases Factory LLC", Active = true },
					new DCU { Id = 35, Name = "325570", Description = "325570 - Linde Global IT Services s. r. o.", Active = true },
					new DCU { Id = 36, Name = "325640", Description = "325640 - Eurogaz-Gdynia Sp. z o.o.", Active = true },
					new DCU { Id = 37, Name = "325780", Description = "325780 - Linde Gas k.s.", Active = true },
					new DCU { Id = 38, Name = "325860", Description = "325860 - LINDE GAZ ROMANIA S.R.L.", Active = true },
					new DCU { Id = 39, Name = "325870", Description = "325870 - Linde Gas Bulgaria EOOD", Active = true },
					new DCU { Id = 40, Name = "326110", Description = "326110 - Linde Gas Therapeutics Benelux B.V.", Active = true },
					new DCU { Id = 41, Name = "326290", Description = "326290 - LINDE GAS SRBIJA Industrija gasova a.d. Be?ej", Active = true },
					new DCU { Id = 42, Name = "326330", Description = "326330 - LINDE HADJIKYRIAKOS GAS LIMITED", Active = true },
					new DCU { Id = 43, Name = "326400", Description = "326400 - Linde Electronics & Specialty Gases (Suzhou) Co Ltd.", Active = true },
					new DCU { Id = 44, Name = "326480", Description = "326480 - Linde Gas Singapore Pte. Ltd.", Active = true },
					new DCU { Id = 45, Name = "326530", Description = "326530 - LINDE GAS BITOLA DOOEL Skopje", Active = true },
					new DCU { Id = 46, Name = "326660", Description = "326660 - Linde Gas Vietnam Limited", Active = true },
					new DCU { Id = 47, Name = "326670", Description = "326670 - Linde Gaz Anonim", Active = true },
					new DCU { Id = 48, Name = "326720", Description = "326720 - Linde Electronics B.V.", Active = true },
					new DCU { Id = 49, Name = "326740", Description = "326740 - Linde Gas Algerie S.p.A.", Active = true },
					new DCU { Id = 50, Name = "401000", Description = "401000 - GS-USA", Active = true },
					new DCU { Id = 51, Name = "610020", Description = "610020 - BOC GROUP LIMITED , THE", Active = true },
					new DCU { Id = 52, Name = "611080", Description = "611080 - BOC Limited - ENG (Gases)", Active = true },
					new DCU { Id = 53, Name = "617030", Description = "617030 - GIST LIMITED", Active = true },
					new DCU { Id = 54, Name = "626670", Description = "626670 - PU_BOC Gases Ireland", Active = true },
					new DCU { Id = 55, Name = "631230", Description = "631230 - Linde Gas North America (old Linde LLC)", Active = true },
					new DCU { Id = 56, Name = "631370", Description = "631370 - Linde North America Inc.", Active = true },
					new DCU { Id = 57, Name = "631410", Description = "631410 - BOC HELEX", Active = true },
					new DCU { Id = 58, Name = "631601", Description = "631601 - East Coast Oxygen Company - sold 01/03/2019", Active = true },
					new DCU { Id = 59, Name = "641000", Description = "641000 - Linde Canada Limited (CAN part) - sold 01/03/2019", Active = true },
					new DCU { Id = 60, Name = "645000", Description = "645000 - BOC GASES ARUBA N.V.", Active = true },
					new DCU { Id = 61, Name = "645520", Description = "645520 - Linde Gas Curaçao N.V.", Active = true },
					new DCU { Id = 62, Name = "646010", Description = "646010 - BOC GASES DE VENEZUELA, C.A.", Active = true },
					new DCU { Id = 63, Name = "646850", Description = "646850 - Compañía de Nitrógeno de Cantarell, S.A. de C.V.", Active = true },
					new DCU { Id = 64, Name = "646940", Description = "646940 - PU_CONSA", Active = true },
					new DCU { Id = 65, Name = "651010", Description = "651010 - PU_Afrox", Active = true },
					new DCU { Id = 66, Name = "651560", Description = "651560 - IGL (PTY) LIMITED", Active = true },
					new DCU { Id = 67, Name = "652030", Description = "652030 - Afrox Moçambique, Limitada", Active = true },
					new DCU { Id = 68, Name = "654000", Description = "654000 - BOC Gases Nigeria Plc", Active = true },
					new DCU { Id = 69, Name = "662000", Description = "662000 - Linde Bangladesh Limited", Active = true },
					new DCU { Id = 70, Name = "663120", Description = "663120 - Linde Global Support Services Private Limited", Active = true },
					new DCU { Id = 71, Name = "663130", Description = "663130 - LINDE INDIA LIMITED", Active = true },
					new DCU { Id = 72, Name = "663200", Description = "663200 - Ceylon Oxygen Ltd.", Active = true },
					new DCU { Id = 73, Name = "665750", Description = "665750 - BOC (China) Holdings Co., Ltd.", Active = true },
					new DCU { Id = 74, Name = "670710", Description = "670710 - BOC Limited (Australia)", Active = true },
					new DCU { Id = 75, Name = "672070", Description = "672070 - BOC LIMITED (New Zealand)", Active = true },
					new DCU { Id = 76, Name = "672760", Description = "672760 - LINDE PHILIPPINES, INC.", Active = true },
					new DCU { Id = 77, Name = "673020", Description = "673020 - Linde Korea Co., Ltd.", Active = true },
					new DCU { Id = 78, Name = "674050", Description = "674050 - LINDE MALAYSIA SDN. BHD.", Active = true },
					new DCU { Id = 79, Name = "674110", Description = "674110 - LINDE ROC SDN. BHD.", Active = true },
					new DCU { Id = 80, Name = "675020", Description = "675020 - Linde Gas Asia Pte Ltd", Active = true },
					new DCU { Id = 81, Name = "675130", Description = "675130 - Linde Gas Asia Pte. Ltd. - Philippines ROHQ", Active = true },
					new DCU { Id = 82, Name = "675510", Description = "675510 - PT. LINDE INDONESIA", Active = true },
					new DCU { Id = 83, Name = "675560", Description = "675560 - P.T. Gresik Gases Indonesia", Active = true },
					new DCU { Id = 84, Name = "675610", Description = "675610 - P.T. Gresik Power Indonesia", Active = true },
					new DCU { Id = 85, Name = "676120", Description = "676120 - Linde HKO Limited", Active = true },
					new DCU { Id = 86, Name = "676640", Description = "676640 - Linde (Thailand) Public Company Limited", Active = true },
					new DCU { Id = 87, Name = "724160", Description = "724160 - LINDE HOMECARE FRANCE SAS", Active = true },
					new DCU { Id = 88, Name = "725200", Description = "725200 - Linde Médica, S.L.", Active = true },
					new DCU { Id = 89, Name = "821000", Description = "821000 - AGA AB Corporate Staffs", Active = true },
					new DCU { Id = 90, Name = "821020", Description = "821020 - AGA AB GAS", Active = true },
					new DCU { Id = 91, Name = "822010", Description = "822010 - AGA A/S", Active = true },
					new DCU { Id = 92, Name = "822130", Description = "822130 - PU_Finland", Active = true },
					new DCU { Id = 93, Name = "822210", Description = "822210 - AGA AS", Active = true },
					new DCU { Id = 94, Name = "822400", Description = "822400 - AS Eesti AGA", Active = true },
					new DCU { Id = 95, Name = "822500", Description = "822500 - ISAGA ehf.", Active = true },
					new DCU { Id = 96, Name = "822600", Description = "822600 - AGA SIA", Active = true },
					new DCU { Id = 97, Name = "822700", Description = "822700 - UAB AGA", Active = true },
					new DCU { Id = 98, Name = "822900", Description = "822900 - AO Linde Gas Rus", Active = true },
					new DCU { Id = 99, Name = "822960", Description = "822960 - AO Linde Uraltechgaz", Active = true },
					new DCU { Id = 100, Name = "823000", Description = "823000 - Private Joint Stock Company - Linde Gas Ukraine", Active = true },
					new DCU { Id = 101, Name = "824630", Description = "824630 - PU_Unterbichler Gase GmbH", Active = true },
					new DCU { Id = 102, Name = "826030", Description = "826030 - LINDE GAS DOMINICANA, S.R.L.", Active = true },
					new DCU { Id = 103, Name = "826100", Description = "826100 - Linde Gas Puerto Rico, Inc. - sold 01/03/2019", Active = true },
					new DCU { Id = 104, Name = "826430", Description = "826430 - Spectra Gases (Shanghai) Trading Co., LTD.", Active = true },
					new DCU { Id = 105, Name = "827010", Description = "827010 - Grupo Linde Gas Argentina S.A.", Active = true },
					new DCU { Id = 106, Name = "827020", Description = "827020 - Linde Gases Ltda. - sold 01/03/2019", Active = true },
					new DCU { Id = 107, Name = "827050", Description = "827050 - Linde Gas Chile S.A.", Active = true },
					new DCU { Id = 108, Name = "827070", Description = "827070 - Linde Colombia S.A. - sold 01/03/2019", Active = true },
					new DCU { Id = 109, Name = "827080", Description = "827080 - Linde Ecuador S.A.", Active = true },
					new DCU { Id = 110, Name = "827130", Description = "827130 - Linde Gas Perú S.A.", Active = true },
					new DCU { Id = 111, Name = "827140", Description = "827140 - AGA S.A.", Active = true },
					new DCU { Id = 112, Name = "827160", Description = "827160 - AGA Gas C.A.", Active = true },
					new DCU { Id = 113, Name = "Existing Project in PPM", Description = "Existing Project in PPM", Active = true },
					new DCU { Id = 114, Name = "Non–Accounting", Description = "Non–Accounting", Active = true }
				};

				dcus.ForEach(s => context.DCUs.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DCUs ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.DCUs OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.ItPlatforms.Any())
			{
				var itPlatforms = new List<ItPlatform>
				{
					new ItPlatform { Id = 1, Name = "D365 Dynamics Platform", Description = "", Active = true },
					new ItPlatform { Id = 2, Name = "Linde governed MS Azure", Description = "", Active = true },
					new ItPlatform { Id = 3, Name = "M365 Power Platform (Power Apps)", Description = "", Active = true },
					new ItPlatform { Id = 4, Name = "M365 SharePoint (SPO)", Description = "", Active = true },
					new ItPlatform { Id = 5, Name = "Salesforce.com (SFDC)", Description = "", Active = true },
					new ItPlatform { Id = 6, Name = "SAP Cloud Platform (SCP)", Description = "", Active = true },
					new ItPlatform { Id = 7, Name = "Other - External service", Description = "", Active = true },
					new ItPlatform { Id = 8, Name = "Other - Internal service (Linde datacentre)", Description = "", Active = true },
					new ItPlatform { Id = 9, Name = "N/A (only if its not a new application)", Description = "", Active = true },
				};

				itPlatforms.ForEach(s => context.ItPlatforms.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ItPlatforms ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ItPlatforms OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.ItSegments.Any())
			{
				var itSegments = new List<ItSegment>
				{
					new ItSegment { Id = 1, Name = "Americas", Description = "", Active = true },
					new ItSegment { Id = 2, Name = "APAC", Description = "", Active = true },
					new ItSegment { Id = 3, Name = "EMEA", Description = "", Active = true },
					new ItSegment { Id = 4, Name = "Global Applications", Description = "", Active = true },
					new ItSegment { Id = 5, Name = "Global Delivery Services", Description = "", Active = true },
					new ItSegment { Id = 6, Name = "Global Infrastructure", Description = "", Active = true },
				};

				itSegments.ForEach(s => context.ItSegments.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ItSegments ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ItSegments OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.ProcessAreas.Any())
			{
				var processAreas = new List<ProcessArea>
				{
					new ProcessArea { Id = 1, Name = "Bulk & On-site Supply Chain", Description = "", Active = true },
					new ProcessArea { Id = 2, Name = "Corporate Communications Incl.web sites.", Description = "", Active = true },
					new ProcessArea { Id = 3, Name = "Customer Engineering Services", Description = "", Active = true },
					new ProcessArea { Id = 4, Name = "Customer Management", Description = "", Active = true },
					new ProcessArea { Id = 5, Name = "Cylinder Supply Chain", Description = "", Active = true },
					new ProcessArea { Id = 6, Name = "Finance & Controlling", Description = "", Active = true },
					new ProcessArea { Id = 7, Name = "Home, Hospital & Healthcare", Description = "", Active = true },
					new ProcessArea { Id = 8, Name = "HR", Description = "", Active = true },
					new ProcessArea { Id = 9, Name = "Internal Audit", Description = "", Active = true },
					new ProcessArea { Id = 10, Name = "IT Internal Demand", Description = "", Active = true },
					new ProcessArea { Id = 11, Name = "Legal Services", Description = "", Active = true },
					new ProcessArea { Id = 12, Name = "Order to Cash", Description = "", Active = true },
					new ProcessArea { Id = 13, Name = "Procurement", Description = "", Active = true },
					new ProcessArea { Id = 14, Name = "SHEQ", Description = "", Active = true },
					new ProcessArea { Id = 15, Name = "Tonnage", Description = "", Active = true },
				};

				processAreas.ForEach(s => context.ProcessAreas.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProcessAreas ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProcessAreas OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.UsersImpacted.Any())
			{
				var usersImpacted = new List<UsersImpacted>
				{
					new UsersImpacted { Id = 1, Name = "< 30", Description = "", Active = true },
					new UsersImpacted { Id = 2, Name = "< 100", Description = "", Active = true },
					new UsersImpacted { Id = 3, Name = "< 500", Description = "", Active = true },
					new UsersImpacted { Id = 4, Name = "< 1000", Description = "", Active = true },
					new UsersImpacted { Id = 5, Name = "< 5000", Description = "", Active = true },
					new UsersImpacted { Id = 6, Name = "< 10000", Description = "", Active = true },
					new UsersImpacted { Id = 7, Name = "< 100000", Description = "", Active = true },
					new UsersImpacted { Id = 8, Name = "Unknown", Description = "", Active = true },
				};

				usersImpacted.ForEach(s => context.UsersImpacted.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.UsersImpacted ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.UsersImpacted OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.Users.Any())
			{
				var users = new List<User>
				{
					new User { Id = 1, DisplayName = "Richard Dillon", UserName = @"MUCLVAD1\rd028u", Email = "Richard.Dillon@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Admin },
					new User { Id = 2, DisplayName = "Darren Williams", UserName = @"LINDE\UK0C0B", Email = "Darren.Williams@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Pmo },
					new User { Id = 3, DisplayName = "Nelson Do Rego", UserName = @"LINDE\UK0C09", Email = "Nelson.DoRego@boc.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Pmo },
					new User { Id = 4, DisplayName = "Lisa Korn", @UserName = @"LINDE\f4gz08", Email = "Lisa.Korn@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User },
					new User { Id = 5, DisplayName = "Geoffrey Munch", @UserName = @"LINDE\f8kx29", Email = "Geoffrey.Munch@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Pmo },
					new User { Id = 6, DisplayName = "Janet Wright", UserName = @"LINDE\UK0CB7", Email = "Janet.Wright@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 7, DisplayName = @"Jason O'Brien", UserName = @"LINDE\SP01B8", Email = @"Jason.O'Brien@boc.com", Created = DateTimeOffset.Now },
					new User { Id = 8, DisplayName = "Tracy Zhang", UserName = @"LINDE\CN02FD", Email = "Tracy.Zhang@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 9, DisplayName = "Youngsam Yu", UserName = @"LINDE\e6uz78", Email = "Youngsam.Yu@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 10, DisplayName = "Sujoy Sen", UserName = @"LINDE\IN0909", Email = "Sujoy.Sen@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 11, DisplayName = "Danny Perri", UserName = @"LINDE\DE1CB4", Email = "Danny.Perri@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 12, DisplayName = "Sandy Morris", UserName = @"LINDE\ZA0431", Email = "Sandy.Morris@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 13, DisplayName = "Oliver Breitbach", UserName = @"LINDE\DE02C3", Email = "Oliver.Breitbach@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 14, DisplayName = "Craig Batey", UserName = @"LINDE\UK0A9F", Email = "Craig.Batey@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 15, DisplayName = "Jean-Luc Darre", UserName = @"LINDE\FR0011", Email = "Jean-Luc.Darre@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 16, DisplayName = "Dulce Borjas", UserName = @"LINDE\b9dd40", Email = "Dulce.Borjas@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 17, DisplayName = "George Pun", UserName = @"LINDE\d4yk49", Email = "george.pun@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 18, DisplayName = "Claudia Silveira", UserName = @"LINDE\c6ol39", Email = "claudia.silveira@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 19, DisplayName = "Martin Vega", UserName = @"LINDE\d4pg03", Email = "Martin.Vega@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 20, DisplayName = "Michael Kurtz", UserName = @"LINDE\DE2007", Email = "michael.kurtz@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 21, DisplayName = "Martin Mossakowski", UserName = @"LINDE\a4iz37", Email = "Martin.Mossakowski@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 22, DisplayName = "Sebastian Mahler", UserName = @"LINDE\DEC138", Email = "Sebastian.Mahler@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 23, DisplayName = "Brenda Silvestro", UserName = @"LINDE\a0it96", Email = "Brenda.Silvestro@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 24, DisplayName = "Richard Wilkie", UserName = @"LINDE\f8ml00", Email = "Richard.Wilkie@nuco2.com", Created = DateTimeOffset.Now },
					new User { Id = 25, DisplayName = "Dena Stirn", UserName = @"LINDE\b0vy55", Email = "Dena.Stirn@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 26, DisplayName = "Pranay Shah", UserName = @"LINDE\a0nc97", Email = "Pranay.Shah@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 27, DisplayName = "Peter Casazza", UserName = @"LINDE\e2cx99", Email = "Peter.Casazza@linde.com", Created = DateTimeOffset.Now },
					new User { Id = 28, DisplayName = "Neil Kitchen", UserName = @"LINDE\UK02BC", Email = "Neil.Kitchen@linde.com", Created = DateTimeOffset.Now },
			        new User { Id = 29, DisplayName = "Thomas Steinich", UserName = @"LINDE\DE027C", Email = "Thomas.Steinich@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Architecture },
                    new User { Id = 30, DisplayName = "Armin Kress", UserName = @"LINDE\DE228D", Email = "armin.kress@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Architecture },
                    new User { Id = 31, DisplayName = "Dave Hodson", UserName = @"LINDE\UK02B0", Email = "David.Hodson@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Security },
                    new User { Id = 32, DisplayName = "Tom Jones", UserName = @"LINDE\UK09C3", Email = "Tom.Jones@linde.com", Created = DateTimeOffset.Now, SecurityRole = SecurityRole.User | SecurityRole.Consulting },
			        //new User { Id = 33, DisplayName = "", UserName = @"", Email = "", Created = DateTimeOffset.Now },

			        //new User { Id = , DisplayName = "", UserName = @"", Email = "" },
			    };

				users.ForEach(s => context.Users.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

			if (!context.BusinessUnits.Any())
			{
				var businessUnits = new List<BusinessUnit>
				{
					new BusinessUnit { Id = 1,  Name = "APAC Cluster", Description = "", ItHeadId = 7, Active = true },
					new BusinessUnit { Id = 2,  Name = "APAC China", Description = "", ItHeadId = 8, Active = true },
					new BusinessUnit { Id = 3,  Name = "APAC Korea", Description = "", ItHeadId = 9, Active = true },
					new BusinessUnit { Id = 4,  Name = "APAC ASEAN", Description = "", ItHeadId = 10, Active = true },
					new BusinessUnit { Id = 5,  Name = "APAC South Asia", Description = "", ItHeadId = 10, Active = true },
					new BusinessUnit { Id = 6,  Name = "APAC South Pacific", Description = "", ItHeadId = 7, Active = true },
					new BusinessUnit { Id = 7,  Name = "EMEA Cluster", Description = "", ItHeadId = 11, Active = true },
					new BusinessUnit { Id = 8,  Name = "EMEA RAF", Description = "", ItHeadId = 12, Active = true },
					new BusinessUnit { Id = 9,  Name = "EMEA REW", Description = "", ItHeadId = 13, Active = true },
					new BusinessUnit { Id = 10, Name = "EMEA REN", Description = "", ItHeadId = 14, Active = true },
					new BusinessUnit { Id = 11, Name = "EMEA RES", Description = "", ItHeadId = 15, Active = true },
					new BusinessUnit { Id = 12, Name = "EMEA REE", Description = "", ItHeadId = 11, Active = true },
					new BusinessUnit { Id = 13, Name = "AMERICAS Cluster", Description = "", ItHeadId = 16, Active = false },
					new BusinessUnit { Id = 14, Name = "AMERICAS Canada", Description = "", ItHeadId = 17, Active = true },
					new BusinessUnit { Id = 15, Name = "AMERICAS US", Description = "", ItHeadId = 16, Active = false },
					new BusinessUnit { Id = 16, Name = "AMERICAS South LATAM", Description = "", ItHeadId = 18, Active = true },
					new BusinessUnit { Id = 17, Name = "AMERICAS North LATAM", Description = "", ItHeadId = 19, Active = true },
					new BusinessUnit { Id = 18, Name = "Corporate & Support Functions", Description = "", ItHeadId = 20, Active = true },
					new BusinessUnit { Id = 19, Name = "Global Applications", Description = "", ItHeadId = 20, Active = true },
					new BusinessUnit { Id = 20, Name = "Global Infrastructure", Description = "", ItHeadId = 21, Active = true },
					new BusinessUnit { Id = 21, Name = "Global Delivery Services", Description = "", ItHeadId = 22, Active = true },
					new BusinessUnit { Id = 22, Name = "IT Strategy & Security", Description = "", ItHeadId = 23, Active = true },
					new BusinessUnit { Id = 23, Name = "PMO & IT Control", Description = "", ItHeadId = 6, Active = true },
					new BusinessUnit { Id = 24, Name = "AMERICAS NuCO2", Description = "", ItHeadId = 24, Active = true },
					new BusinessUnit { Id = 25, Name = "AMERICAS PST", Description = "", ItHeadId = 25, Active = true },
					new BusinessUnit { Id = 26, Name = "AMERICAS USIG", Description = "", ItHeadId = 26, Active = true },
					new BusinessUnit { Id = 27, Name = "AMERICAS PDI", Description = "", ItHeadId = 27, Active = true },
					new BusinessUnit { Id = 28, Name = "EMEA RUI", Description = "", ItHeadId = 28, Active = true },
				};

				businessUnits.ForEach(s => context.BusinessUnits.Add(s));

				context.Database.OpenConnection();
				try
				{
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessUnits ON");
					context.SaveChanges();
					context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.BusinessUnits OFF");
				}
				finally
				{
					context.Database.CloseConnection();
				}
			}

            if (context.Workflows.Any()) return;

            context.Add(ItDemandReview);
            context.Add(ProceedLocallyL1);
            context.Add(ProceedLocallyL2);
            context.Add(FastTrackL3);
            context.Add(BigProjectL4);

            context.Database.OpenConnection();
            try
            {
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Workflows ON");
                context.SaveChanges();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.workflows OFF");
            }
            finally
            {
                context.Database.CloseConnection();
            }

            CreateChecklistTemplates(context);
        }

        private static Workflow _itDemandReview = null!;
        public static Workflow ItDemandReview
        {
            get
            {
                if (_itDemandReview != null) return _itDemandReview;

                _itDemandReview = new Workflow
                {
					Id = WorkflowType.ItDemandReview,
                    Name = WorkflowType.ItDemandReview.GetDescription<WorkflowType>(),
                    Items = new List<WorkflowItem>
					{
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.ItDemandReview, "Demand Review Gate 1 Planning", Resources.DR_Gate_1_Process, 1)
                            .CreateChild(WorkflowItemType.Decision, "Demand Review Gate 1", Resources.DR_Gate_1_Decision),
                    }
                };

                return _itDemandReview;
            }
        }

        private static Workflow _proceedLocallyL1 = null!;
        public static Workflow ProceedLocallyL1
        {
            get
            {
                if (_proceedLocallyL1 != null) return _proceedLocallyL1;

                _proceedLocallyL1 = new Workflow
                {
                    Id = WorkflowType.ProceedLocallyL1,
                    Name = WorkflowType.ProceedLocallyL1.GetDescription<WorkflowType>(),
                    Items = new List<WorkflowItem>
                    {
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.ProceedLocallyL1, "IT Head Request Approval", Resources.IT_Head_Approval_Process, 1)
                            .CreateChild(WorkflowItemType.Decision, "IT Head Approval Review",Resources.IT_Head_Approval_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.ProceedLocallyL1, "Gate 7 Project Closeout", Resources.Gate_7_Process, 2)
                            .CreateChild(WorkflowItemType.Decision, "Gate 7 Closeout",Resources.Gate_7_Decision)
                    }
                };

                return _proceedLocallyL1;
            }
        }

        private static Workflow _proceedLocallyL2 = null!;
        public static Workflow ProceedLocallyL2
        {
            get
            {
                if (_proceedLocallyL2 != null) return _proceedLocallyL2;

                _proceedLocallyL2 = new Workflow
                {
                    Id = WorkflowType.ProceedLocallyL2,
                    Name = WorkflowType.ProceedLocallyL2.GetDescription<WorkflowType>(),
                    Items = new List<WorkflowItem>
                    {
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.ProceedLocallyL2, "Demand Review Gate 1 Planning", Resources.DR_Gate_1_Process, 1)
                            .CreateChild(WorkflowItemType.Decision, "Demand Review Gate 1",Resources.DR_Gate_1_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.ProceedLocallyL2, "IT Head Request Approval", Resources.IT_Head_Approval_Process, 2)
                            .CreateChild(WorkflowItemType.Decision, "IT Head Approval Review",Resources.IT_Head_Approval_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.ProceedLocallyL2, "Gate 7 Project Closeout", Resources.Gate_7_Process, 3)
                            .CreateChild(WorkflowItemType.Decision, "Gate 7 Closeout",Resources.Gate_7_Decision)
                    }
                };

                return _proceedLocallyL2;
            }
        }

        private static Workflow _fastTrackL3 = null!;
        public static Workflow FastTrackL3
        {
            get
            {
                if (_fastTrackL3 != null) return _fastTrackL3;

                _fastTrackL3 = new Workflow
                {
                    Id = WorkflowType.FastTrackL3,
                    Name = WorkflowType.FastTrackL3.GetDescription<WorkflowType>(),
                    Items = new List<WorkflowItem>
                    {
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.FastTrackL3, "Demand Review Gate 1 Planning", Resources.DR_Gate_1_Process, 1)
                            .CreateChild(WorkflowItemType.Decision, "Demand Review Gate 1",Resources.DR_Gate_1_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.FastTrackL3, "CAB", Resources.CAB_Process, 2)
                            .CreateChild(WorkflowItemType.Decision, "CAB Approval",Resources.CAB_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.FastTrackL3, "IT Head Request Approval", Resources.IT_Head_Approval_Process, 3)
                            .CreateChild(WorkflowItemType.Decision, "IT Head Approval Review",Resources.IT_Head_Approval_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.FastTrackL3, "Gate 5 Pre-Go Live Planning", Resources.Gate_5_Process, 4)
                            .CreateChild(WorkflowItemType.Decision, "Gate 5 Pre-Go Live Review",Resources.Gate_5_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.FastTrackL3, "Gate 7 Project Closeout", Resources.Gate_7_Process, 5)
                            .CreateChild(WorkflowItemType.Decision, "Gate 7 Closeout",Resources.Gate_7_Decision)
                    }
                };

                return _fastTrackL3;
            }
        }

        private static Workflow _bigProjectL4 = null!;
        public static Workflow BigProjectL4
        {
            get
            {
                if (_bigProjectL4 != null) return _bigProjectL4;

                _bigProjectL4 = new Workflow
                {
                    Id = WorkflowType.BigProjectL4,
                    Name = WorkflowType.BigProjectL4.GetDescription<WorkflowType>(),
                    Items = new List<WorkflowItem>
                    {
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Demand Review Gate 1 Planning", Resources.DR_Gate_1_Process, 1)
                            .CreateChild(WorkflowItemType.Decision, "Demand Review Gate 1",Resources.DR_Gate_1_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "IT Head Initial Review", Resources.L1_IT_Head_Initial_Approval_Process, 2)
                            .CreateChild(WorkflowItemType.Decision, "IT Head Approval to Proceed",Resources.L1_IT_Head_Initial_Approval_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Solution Design", Resources.Solution_Design_Process, 3)
                            .CreateChild(WorkflowItemType.Decision, "Solution Design Review",Resources.Solution_Design_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Security and Compliance Planning", Resources.Security_Planning_Process, 4)
                            .CreateChild(WorkflowItemType.Decision, "Security and Compliance Review",Resources.Security_Planning_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Architecture Board", Resources.Architecture_Board_Process, 5)
                            .CreateChild(WorkflowItemType.Decision, "Architecture Board Review",Resources.Architecture_Board_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Demand Review Gate 2 Planning", Resources.DR_Gate_1_Process, 6)
                            .CreateChild(WorkflowItemType.Decision, "Demand Review Gate 2",Resources.DR_Gate_2_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "IT Head Request Approval", Resources.IT_Head_Approval_Process, 7)
                            .CreateChild(WorkflowItemType.Decision, "IT Head Approval Review",Resources.IT_Head_Approval_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Gate 3 Planning", Resources.Gate_3_Process, 8)
                            .CreateChild(WorkflowItemType.Decision, "Gate 3 Plan Review",Resources.Gate_3_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Gate 4 Execute Planning", Resources.Gate_4_Process, 9)
                            .CreateChild(WorkflowItemType.Decision, "Gate 4 Execute Review",Resources.Gate_4_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Gate 5 Pre-Go Live Planning", Resources.Gate_5_Process, 10)
                            .CreateChild(WorkflowItemType.Decision, "Gate 5 Pre-Go Live Review",Resources.Gate_5_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Gate 6 Go Live Planning", Resources.Gate_6_Process, 11)
                            .CreateChild(WorkflowItemType.Decision, "Gate 6 Go Live Review",Resources.Gate_6_Decision),
                        new WorkflowItem(WorkflowItemType.Process, WorkflowType.BigProjectL4, "Gate 7 Project Closeout", Resources.Gate_7_Process, 12)
                            .CreateChild(WorkflowItemType.Decision, "Gate 7 Closeout",Resources.Gate_7_Decision)
                    }
                };

                return _bigProjectL4;
            }
        }

        private static void CreateChecklistTemplates(ItDemandContext context)
        {
			var workflows = context.Workflows.ToArray();
            foreach (var workflow in workflows)
            {
                var templates = FindTemplates(workflow.Id);
                
                foreach (var template in templates)
                {
                    var process = workflow.Items.SingleOrDefault(x => x.Stage == template.Key && x.WorkflowItemType == WorkflowItemType.Process);

                    if (process == null) continue;

                    foreach (var item in template.Value)
                    {
						item.WorkflowItemId = process.Id;
                        process.ChecklistTemplates.Add(item);
                        context.SaveChanges();
                    }   
                }
            }
        }

        private static Dictionary<int, ChecklistTemplate[]> FindTemplates(WorkflowType projectType)
        {
            var checklistTemplates = new Dictionary<int, ChecklistTemplate[]>();

            switch (projectType)
            {
                case WorkflowType.ItDemandReview:
                    checklistTemplates.Add(1, new[] { DemandReviewGate1 });
                    break;

                case WorkflowType.ProceedLocallyL1:
                    checklistTemplates.Add(1, new[] { ItHeadApproval });
                    checklistTemplates.Add(2, new[] { Gate7Closeout });
                    break;

                case WorkflowType.ProceedLocallyL2:
                    checklistTemplates.Add(2, new[] { ItHeadApproval });
                    checklistTemplates.Add(3, new[] { Gate7Closeout });
                    break;

                case WorkflowType.FastTrackL3:
                    checklistTemplates.Add(2, new[] { CabReview });
                    checklistTemplates.Add(3, new[] { ItHeadApproval });
                    checklistTemplates.Add(4, new[] { Gate5PreGoLive });
                    checklistTemplates.Add(5, new[] { Gate7Closeout });
                    break;

                case WorkflowType.BigProjectL4:
                    checklistTemplates.Add(2, new[] { ItHeadInitialReview });
                    checklistTemplates.Add(3, new[] { SolutionDesignReview });
                    checklistTemplates.Add(4, new[] { SecurityComplianceReview });
                    checklistTemplates.Add(5, new[] { ArchitectureBoardReview });
                    checklistTemplates.Add(6, new[] { DemandReviewGate2 });
                    checklistTemplates.Add(7, new[] { ItHeadApproval });
                    checklistTemplates.Add(8, new[] { Gate3Plan });
                    checklistTemplates.Add(9, new[] { Gate4Execute });
                    checklistTemplates.Add(10, new[] { Gate5PreGoLive });
                    checklistTemplates.Add(11, new[] { Gate6GoLive });
                    checklistTemplates.Add(12, new[] { Gate7Closeout });
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(projectType), projectType, null);
            }

            return checklistTemplates;
        }

        #region IT Demand Checklists
        private static ChecklistTemplate DemandReviewGate1 => new()
        {
            Name = "Demand Review Gate 1",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            Questions = new[]
            {
                CreateQuestion("1.1", "Additional Scrutiny?", QuestionType.MultiSelect, "", "Architecture,Security,Other"),
                CreateQuestion("1.2", "Date:", QuestionType.Date),
                CreateQuestion("1.3", "Comment By:", QuestionType.UserSelect),
                CreateQuestion("1.4", "Status Comment:", QuestionType.MultilineText),
                CreateQuestion("1.5", "Required Action:", QuestionType.MultilineText),
                CreateQuestion("1.6", "Action With:", QuestionType.UserSelect),
                CreateQuestion("1.7", "Execution Workflow", QuestionType.CustomChoice, "Any", "Proceed Locally (L2),Fast Track (L3),Big Project (L4)"),
                CreateQuestion("1.8", "CAB Review", QuestionType.YesNo, "Yes,No"),
                CreateQuestion("1.9", "Assigned Enterprise Architect", QuestionType.UserSelect),
                CreateQuestion("1.10", "Solution Architect", QuestionType.UserSelect),
                CreateQuestion("1.11", "Assigned Security Reviewer", QuestionType.UserSelect),
                CreateQuestion("1.12", "Security Review Comments", QuestionType.MultilineText)
            },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsPmo, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate ItHeadApproval => new()
        {
            Name = "IT Head Approval",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            //Questions = new[] { },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsHead, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate CabReview => new()
        {
            Name = "CAB Review",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            //Questions = new[]
            //{
            //    CreateQuestion("2.1", "Section Header Example", QuestionType.Header ),
            //    CreateQuestion("2.1.1", "Any questions we want to have answered for this checklist?")
            //},
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.Gbc, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate ItHeadInitialReview => new()
        {
            Name = "IT Head Initial Review",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            //Questions = new[] { },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsHead, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate SolutionDesignReview => new()
        {
            Name = "Solution Design",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            Questions = new[]
            {
                CreateQuestion("2.1", "Solution Design Document completed", QuestionType.YesNo),
                CreateQuestion("2.2", "Business/User Requirements document completed", QuestionType.YesNo)
            },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ProjectManager, Type = ApproverType.UserPicker, Required = true }
            }
        };

        private static ChecklistTemplate SecurityComplianceReview => new()
        {
            Name = "Security and Compliance Review",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            //Questions = new[]
            //{
            //    CreateQuestion("1.1", "Section Header Example", QuestionType.Header ),
            //    CreateQuestion("1.1.1", "Any questions we want to have answered for this checklist?")
            //},
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.RiskCompliance, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate ArchitectureBoardReview => new()
        {
            Name = "Architecture Board Review",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            Questions = new[]
            {
                CreateQuestion("1.1", "Architecture Board Outcome", QuestionType.MultilineText),
            },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ArchitectureTeam, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate DemandReviewGate2 => new()
        {
            Name = "Demand Review Gate 2",
            GateReviewDescription =
                "ToDo: Add a Gate Review Description. Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            //Questions = new[]
            //{
            //    CreateQuestion("1.1", "Section Header Example", QuestionType.Header ),
            //    CreateQuestion("1.1.1", "Any questions we want to have answered for this checklist?")
            //},
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsPmo, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate Gate3Plan => new()
        {
            Name = "Gate 3 Plan Checklist",
            GateReviewDescription =
                "Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            Questions = new[]
            {
                CreateQuestion("1.1", "Project Charter"),
                CreateQuestion("1.2", "Business Requirements Document"),
                CreateQuestion("1.3", "Solution Design Document"),
                CreateQuestion("1.4", "IC Submission"),
                CreateQuestion("1.5", "Master Project Plan"),
                CreateQuestion("1.6", "Budget"),
                CreateQuestion("1.7", "Risk Management Plan"),
                CreateQuestion("1.8", "Change Control Log"),
                CreateQuestion("1.9", "Project Communication Plan"),
                CreateQuestion("1.10", "Project Test Plan"),
                CreateQuestion("1.11", "Steering Committee Update")
            },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsPmo, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate Gate4Execute => new()
        {
            Name = "Gate 4 Execute Checklist",
            GateReviewDescription =
                "Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            Questions = new[]
            {
                CreateQuestion("1.1", "Business Service Level Description"),
                CreateQuestion("1.2", "Project Kick-Off Meeting"),
                CreateQuestion("1.3", "Project Plan Updated"),
                CreateQuestion("1.4", "Project Plan Executive Summary"),
                CreateQuestion("1.5", "Steering Committee Update")
            },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsPmo, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate Gate5PreGoLive => new()
        {
            Name = "Gate 5 Pre-Go Live Checklist",
            GateReviewDescription =
                "Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            Questions = new[]
            {
                CreateQuestion("1.1", "Product Acceptance Checklist"),
                CreateQuestion("1.2", "Pre-Go Live Checklist"),
                CreateQuestion("1.3", "Steering Committee Update")
            },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsPmo, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate Gate6GoLive => new()
        {
            Name = "Gate 6 Go Live Checklist",
            GateReviewDescription =
                "Are there any instructions we would like to put here explaining what this checklist is or conditions for approving it?",
            Questions = new[]
            {
                CreateQuestion("1.1", "Go Live Executed"),
                CreateQuestion("1.2", "Support Plan Executed"),
                CreateQuestion("1.3", "Final Steering Committee Update")
            },
            Approvers = new[] {
                new ChecklistTemplateApprover { SortIndex = 0, Role = ApproverRole.ItsPmo, Type = ApproverType.Dropdown, Required = true }
            }
        };

        private static ChecklistTemplate Gate7Closeout => new()
        {
            Name = "Gate 7 Closeout Checklist",
            GateReviewDescription =
                "Review and approve final closeout of this project.",
            Questions = new[]
            {
                CreateQuestion("1.1", "HyperCare End"),
                CreateQuestion("1.2", "Post Implementation Review")
            },
            Approvers = new[] {
                new ChecklistTemplateApprover{ SortIndex = 0, Role = ApproverRole.ItsPmo, Type = ApproverType.Dropdown, Required = true }
            }
        };
        #endregion

        private static ChecklistTemplateQuestion CreateQuestion(string path, string text, QuestionType questionType = QuestionType.YesNoNa, string acceptedAnswers = "", string customChoices = "", string helpText = "")
        {
            return new ChecklistTemplateQuestion { Path = path, Text = text, QuestionType = questionType, AcceptedAnswers = acceptedAnswers, CustomChoices = customChoices, HelpText = helpText };
        }

        private static void InitializeSecurityRoles(ItDemandContext context)
		{
			const string webAppServiceAccount = @"LINDE\s4qs83";
            const string lindeIsToolsTeam = @"LINDE\GLGGCELindeISToolsTeam";
            //const string ownersGroup = @"MUCLVAD1\DGD0_PX_LEA_Enam_Developers";

            var sqlAddServiceAccount =
				$"CREATE USER[{webAppServiceAccount}] FOR LOGIN[{webAppServiceAccount}]" +
				$"EXEC sp_addrolemember N'db_datawriter', N'{webAppServiceAccount}'" +
				$"EXEC sp_addrolemember N'db_datareader', N'{webAppServiceAccount}'";

			context.Database.ExecuteSqlRaw(sqlAddServiceAccount);

            var sqlLindeIsToolsTeam =
            	$"CREATE USER [{lindeIsToolsTeam}] FOR LOGIN [{lindeIsToolsTeam}]" +
                $"EXEC sp_addrolemember N'db_datawriter', N'{lindeIsToolsTeam}'" +
                $"EXEC sp_addrolemember N'db_datareader', N'{lindeIsToolsTeam}'";

            context.Database.ExecuteSqlRaw(sqlLindeIsToolsTeam);

            //var sqlAddOwnersGroup =
            //	$"CREATE USER [{ownersGroup}] FOR LOGIN [{ownersGroup}]" +
            //	$"EXEC sp_addrolemember N'db_owner', N'{ownersGroup}'";

            //context.Database.ExecuteSqlRaw(sqlAddOwnersGroup);
        }
    }
}
