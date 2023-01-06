using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItDemand.Domain.Repositories
{
	public class ProjectRepository
	{
		private readonly ItDemandContext _context;

		public ProjectRepository(ItDemandContext context)
		{
			_context = context;
		}

		public IEnumerable<BusinessUnit> GetBusinessUnits()
		{
			return _context.BusinessUnits.Where(x => x.Active);
		}

		public BusinessUnit? GetBusinessUnit(int id)
		{
			if (id < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(id), $"id must be greater than zero (id: {id})");
			}

			var businessUnit = _context.BusinessUnits.SingleOrDefault(x => x.Id == id);
			return businessUnit;
		}
	}
}
