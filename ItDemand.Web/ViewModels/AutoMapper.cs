using ItDemand.Domain.Interfaces;
using ItDemand.Domain.Models;
using ItDemand.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItDemand.Web.ViewModels
{
	public class AutoMapper : global::AutoMapper.Profile
	{
		public AutoMapper()
		{
            CreateMap<ActiveDirectoryUser, User>();
            CreateMap<Attachment, AttachmentViewModel>();

            CreateMap<Checklist, ChecklistViewModel>().ReverseMap();
            CreateMap<ChecklistApprover, ChecklistApproverViewModel>().ReverseMap();
            CreateMap<ChecklistQuestion, ChecklistQuestionViewModel>().ReverseMap();

            CreateMap<ISelectListOption, SelectOptionViewModel>();

			CreateMap<SelectOptionViewModel, SelectListItem>()
				.ForMember(d => d.Value, opt => opt.MapFrom(x => x.Id))
				.ForMember(d => d.Text, opt => opt.MapFrom(x => x.Name));

			CreateMap<User, UserViewModel>().ReverseMap();
			

			CreateMap<DemandRequest, DemandRequestViewModel>()
				.ForMember(x => x.AffectedBusinessUnits, d => d.MapFrom(m => m.AffectedBusinessUnits.Select(x => x.BusinessUnitId)))
                .ForMember(x => x.ComplianceRelevant, d => d.MapFrom(m => m.ComplianceRelevant.Select(x => x.ComplianceItemId)))
                .ForMember(x => x.RequestCorrectionsByDisplayName, m => m.MapFrom(x => x.RequestCorrectionsBy == null ? string.Empty : x.RequestCorrectionsBy.DisplayName))
                .ForMember(x => x.CancelledByDisplayName, m => m.MapFrom(x => x.CancelledBy == null ? string.Empty : x.CancelledBy.DisplayName))
                .ReverseMap()
				.ForMember(x => x.AffectedBusinessUnits, opt => opt.Ignore())
				.ForMember(x => x.ComplianceRelevant, opt => opt.Ignore())
                .ForMember(x => x.RequestOwnerId, opt => opt.Ignore())
                .ForMember(x => x.RequestOwner, opt => opt.Ignore())
                .ForMember(x => x.RequestSponsorId, opt => opt.Ignore())
                .ForMember(x => x.RequestSponsor, opt => opt.Ignore())
                .ForMember(x => x.ProjectManagerId, opt => opt.Ignore())
                .ForMember(x => x.ProjectManager, opt => opt.Ignore())
                .ForMember(x => x.ItHeadId, opt => opt.Ignore())
                .ForMember(x => x.ItHead, opt => opt.Ignore())
                .ForMember(x => x.AssignedSmeId, opt => opt.Ignore())
                .ForMember(x => x.AssignedSme, opt => opt.Ignore())
                .ForMember(x => x.UsersImpacted, opt => opt.Ignore())
                .ForMember(x => x.Attachments, opt => opt.Ignore())
                .ForMember(x => x.CancelledById, opt => opt.Ignore())
                .ForMember(x => x.CancelledBy, opt => opt.Ignore())
                .ForMember(x => x.RequestCorrectionsById, opt => opt.Ignore())
                .ForMember(x => x.RequestCorrectionsBy, opt => opt.Ignore());
		}
	}
}
