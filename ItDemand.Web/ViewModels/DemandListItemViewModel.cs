using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;
using ItDemand.Domain.Utils;

namespace ItDemand.Web.ViewModels
{
    public class DemandListItemViewModel
    {
        public int Id { get; set; }
        public WorkflowType? ExecutionType { get; set; }
        public string Name { get; set; }
        public DemandState DemandState { get; set; }
        public string DemandStateDisplay { get; set; }
        public string RequestOwner { get; set; }
        public string RequestSponsor { get; set; }
        public string ProjectManager { get; set; }
        public string FundingBusinessUnit { get; set; }
        public string ItHead { get; set; }
        public string ApplicationType { get; set; }
        public string UsersImpacted { get; set; }
        public string EstimatedCost { get; set; }
        public string SecurityAssessment { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Comments { get; set; }

        public string DisplayId => $"Demand-{Id}";
        
        public DemandListItemViewModel(DemandRequest request)
        {
            Id = request.Id;
            ExecutionType = request.ExecutionType;
            Name = request.Name;
            DemandState = request.DemandState;
            DemandStateDisplay = request.DemandState.GetDescription<DemandState>();
            RequestOwner = request.RequestOwner?.DisplayName ?? string.Empty;
            RequestSponsor = request.RequestSponsor?.DisplayName ?? string.Empty;
            ProjectManager = request.ProjectManager?.DisplayName ?? string.Empty;
            FundingBusinessUnit = request.BusinessUnit?.Name ?? string.Empty;
            ItHead = request.ItHead?.DisplayName ?? string.Empty;
            ApplicationType = request.ApplicationType?.Name ?? string.Empty;
            UsersImpacted = request.UsersImpacted?.Name ?? string.Empty;
            EstimatedCost = request.EstimatedTotalCost;
            SecurityAssessment = request.SecurityAssessment;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            Comments = request.Comments;
        }
    }
}
