using ItDemand.Domain.Enums;
using ItDemand.Domain.Models;

namespace ItDemand.Web.ViewModels
{
    public class GateViewModel
    {
        public double Number { get; private set; }
        public string Name { get; private set; }
        public List<Checklist> Documents { get; private set; }

        public GateViewModel(WorkflowItem item, List<Checklist> documents)
        {
            Number = item.SequenceNumber;
            Name = item.Name;
            Documents = documents;
        }

        public double PercentComplete
        {
            get
            {
                var approved = Documents.Count(x => x.Status == StatusType.Approved);
                var total = Documents.Count;
                return Convert.ToDouble(approved) / total;
            }
        }

        public bool IsComplete
        {
            get { return Documents.All(x => x.Status == StatusType.Approved); }
        }
    }
}
