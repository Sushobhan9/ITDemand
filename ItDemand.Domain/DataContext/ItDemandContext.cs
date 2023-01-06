using ItDemand.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ItDemand.Domain.DataContext
{
	public class ItDemandContext : DbContext
	{
		public DbSet<ApplicationType> ApplicationTypes { get; set; } = null!;
		public DbSet<Attachment> Attachments { get; set; } = null!;
		public DbSet<BusinessDriver> BusinessDrivers { get; set; } = null!;
		public DbSet<BusinessProcessL1> BusinessProcessL1s { get; set; } = null!;
		public DbSet<BusinessProcessL2> BusinessProcessL2s { get; set; } = null!;
		public DbSet<BusinessProcessL3> BusinessProcessL3s { get; set; } = null!;
		public DbSet<BusinessUnit> BusinessUnits { get; set; } = null!;
		public DbSet<Checklist> Checklists { get; set; } = null!;
		public DbSet<ChecklistApprover> ChecklistApprovers { get; set; } = null!;
		public DbSet<ChecklistQuestion> ChecklistQuestions { get; set; } = null!;
		public DbSet<ChecklistTemplate> ChecklistTemplates { get; set; } = null!;
		public DbSet<ChecklistTemplateApprover> ChecklistTemplateApprovers { get; set; } = null!;
		public DbSet<ChecklistTemplateQuestion> ChecklistTemplateQuestions { get; set; } = null!;
		public DbSet<ComplianceItem> ComplianceItems { get; set; } = null!;
		public DbSet<Country> Countries { get; set; } = null!;
		public DbSet<DemandRequest> DemandRequests { get; set; } = null!;
        public DbSet<DemandRequestBusinessUnit> DemandRequestBusinessUnits { get; set; } = null!;
        public DbSet<DemandRequestComplianceItem> DemandRequestComplianceItems { get; set; } = null!;
        public DbSet<DCU> DCUs { get; set; } = null!;
		public DbSet<ItPlatform> ItPlatforms { get; set; } = null!;
		public DbSet<ItSegment> ItSegments { get; set; } = null!;
        public DbSet<MailItem> MailItems { get; set; } = null!;
        public DbSet<ProcessArea> ProcessAreas { get; set; } = null!;
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<UsersImpacted> UsersImpacted { get; set; } = null!;
		public DbSet<Workflow> Workflows { get; set; } = null!;
		public DbSet<WorkflowItem> WorkflowItems { get; set; } = null!;

		public ItDemandContext(DbContextOptions<ItDemandContext> options)
		   : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            // Configure the join tables for the many to many relationships
            //

            modelBuilder.Entity<DemandRequestBusinessUnit>().HasKey(pbu => new { pbu.DemandRequestId, pbu.BusinessUnitId });

			modelBuilder.Entity<DemandRequestBusinessUnit>()
				.HasOne(u => u.DemandRequest)
				.WithMany(u => u.AffectedBusinessUnits)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<DemandRequestComplianceItem>().HasKey(pbu => new { pbu.DemandRequestId, pbu.ComplianceItemId });

			modelBuilder.Entity<DemandRequestComplianceItem>()
				.HasOne(u => u.DemandRequest)
				.WithMany(u => u.ComplianceRelevant)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			// Configure Delete constraints to avoid cascading paths.
			//
			
			modelBuilder.Entity<DemandRequest>()
				   .HasOne(u => u.CreatedBy)
				   .WithMany()
				   .IsRequired()
				   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Checklist>()
				   .HasOne(u => u.WorkflowItem)
				   .WithMany()
				   .IsRequired()
				   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Attachment>()
				   .HasOne(u => u.CreatedBy)
				   .WithMany()
				   .IsRequired()
				   .OnDelete(DeleteBehavior.Restrict);
		}

        /// <summary>
        /// Cancelling a demand only deletes the workflow artifacts related to an IT Demand Request.
        /// <param name="id"></param>
        /// <param name="onCancel"></param>
        public void CancelDemand(int id, Action<DemandRequest>? onCancel = null)
        {
            using var transaction = Database.BeginTransaction();
            try
            {
                var model = DemandRequests.Find(id);
                if (model != null)
                {
                    Database.ExecuteSqlRaw($"Delete from ChecklistApprovers where ChecklistId in (select Id from Checklists where DemandRequestId = {id})");
                    Database.ExecuteSqlRaw($"Delete from ChecklistQuestions where ChecklistId in (select Id from Checklists where DemandRequestId = {id})");
                    Database.ExecuteSqlRaw($"Delete from Checklists where DemandRequestId = {id}");
                    onCancel?.Invoke(model);
					SaveChanges();
					transaction.Commit();
				}
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void DeleteDemand(int id, Action<DemandRequest>? onDelete = null)
        {
            using var transaction = Database.BeginTransaction();
            try
            {
                var model = DemandRequests.Find(id);
                if (model != null)
                {
                    Database.ExecuteSqlRaw($"Delete from ChecklistApprovers where ChecklistId in (select Id from Checklists where DemandRequestId = {id})");
                    Database.ExecuteSqlRaw($"Delete from ChecklistQuestions where ChecklistId in (select Id from Checklists where DemandRequestId = {id})");
                    Database.ExecuteSqlRaw($"Delete from Checklists where DemandRequestId = {id}");
                    Database.ExecuteSqlRaw($"Delete from Attachments where DemandRequestId = {id}");
                    Database.ExecuteSqlRaw($"Delete from DemandRequestBusinessUnits where DemandRequestId = {id}");
                    Database.ExecuteSqlRaw($"Delete from DemandRequestComplianceItems where DemandRequestId = {id}");
                    Remove(model);
                    onDelete?.Invoke(model);
                    SaveChanges();
                    transaction.Commit();
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
