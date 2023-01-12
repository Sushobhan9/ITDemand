namespace ItDemand.Web.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ChecklistViewModel[] Checklists { get; set; } = Array.Empty<ChecklistViewModel>();

        public static ProjectViewModel Empty => new()
        {
            Id = 0,
            Name = "Not Found",
            Checklists = Array.Empty<ChecklistViewModel>()
        };

        //public IEnumerable<GateViewModel> Gates
        //{
        //    get
        //    {
        //        return Checklists
        //            .OrderBy(x => x.WorkflowItem.SequenceNumber)
        //            .GroupBy(x => new { x.WorkflowItem })
        //            .Select(x => new GateViewModel(x.Key.WorkflowItem, x.ToList()));
        //    }
        //}
    }
}
