using ItDemand.Domain.Enums;

namespace ItDemand.Web.ViewModels
{
    public class GateViewModel
    {
        public double Number { get; private set; }
        public string Name { get; private set; }
        public List<ChecklistViewModel> Documents { get; private set; }

        public GateViewModel(WorkflowItemViewModel item, List<ChecklistViewModel> documents)
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
