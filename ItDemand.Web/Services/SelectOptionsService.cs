using AutoMapper;
using ItDemand.Domain.DataContext;
using ItDemand.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ItDemand.Web.Services
{
	public class SelectOptionsService
	{
		private readonly ItDemandContext _db;
		private readonly IMapper _mapper;

		public SelectOptionsService(ItDemandContext dbContext, IMapper mapper)
		{
			_db = dbContext;
			_mapper = mapper;
		}

        public IEnumerable<SelectOptionViewModel> GetApplicationTypeOptions()
        {
            var applicationTypes = _db.ApplicationTypes;
            return
                applicationTypes
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetBusinessProcessL1Options()
        {
            var businessProcessL1s = _db.BusinessProcessL1s;
            return
                businessProcessL1s
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetBusinessProcessL2Options(int? parentId)
        {
            if (parentId == null) return Array.Empty<SelectOptionViewModel>();

            var businessProcessL2s = _db.BusinessProcessL2s.Where(x => x.BusinessProcessL1Id == parentId);
            return
                businessProcessL2s
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetBusinessProcessL3Options(int? parentId)
        {
            if (parentId == null) return Array.Empty<SelectOptionViewModel>();

            var businessProcessL3s = _db.BusinessProcessL3s.Where(x => x.BusinessProcessL2Id == parentId);
            return
                businessProcessL3s
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetBusinessUnitOptions()
        {
            var businessUnits = _db.BusinessUnits;
            return
                businessUnits
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetComplianceItemOptions()
        {
            var complianceItems = _db.ComplianceItems;
            return
                complianceItems
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetCountryOptions()
        {
            var countries = _db.Countries;
            return
                countries
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetDcuOptions()
        {
            var dcuItems = _db.DCUs;
            return
                dcuItems
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetItPlatformOptions()
        {
            var itPlatforms = _db.ItPlatforms;
            return
                itPlatforms
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetItSegmentOptions()
        {
            var itSegments = _db.ItSegments;
            return
                itSegments
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetProcessAreaOptions()
        {
            var processAreas = _db.ProcessAreas;
            return
                processAreas
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetNumberOfUsersImpactedOptions()
        {
            var usersImpacted = _db.UsersImpacted;
            return
                usersImpacted
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }

        public IEnumerable<SelectOptionViewModel> GetBusinessDriverOptions()
        {
            var businessDrivers = _db.BusinessDrivers;
            return
                businessDrivers
                    .Where(x => x.Active)
                    .Select(x => _mapper.Map<SelectOptionViewModel>(x));
        }
    }
}
