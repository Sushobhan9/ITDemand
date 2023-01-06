using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItDemand.Domain.Enums
{
	public enum StatusType
	{
		New = 0,
		InProgress = 1,
		WaitingApproval = 2,
		Approved = 3,
		Rejected = 4
	}
}
