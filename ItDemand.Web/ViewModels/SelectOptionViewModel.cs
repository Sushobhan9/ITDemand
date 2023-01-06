namespace ItDemand.Web.ViewModels
{
	public class SelectOptionViewModel
	{
		public int Id { get; set; }
		public int? ParentId { get; set; } // for cascading drop-downs
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int SortOrder { get; set; }
	}
}
