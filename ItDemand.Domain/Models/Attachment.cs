using System.ComponentModel.DataAnnotations.Schema;

namespace ItDemand.Domain.Models
{
	public class Attachment
	{
        public int Id { get; set; }

        public int DemandRequestId { get; set; }
        [ForeignKey("DemandRequestId")]
        public virtual DemandRequest DemandRequest { get; set; } = null!;

        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] Contents { get; set; } = Array.Empty<byte>();

        public int? CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User? CreatedBy { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
