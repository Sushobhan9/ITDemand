using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace ItDemand.Domain.Models
{
    public class MailItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string To { get; set; } = string.Empty;
        [Required]
        public string From { get; set; } = string.Empty;
        public string CC { get; set; } = string.Empty;
        public string Bcc { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Sent { get; set; }
        public string Error { get; set; } = string.Empty;

        public MailItem()
        {
            Created = DateTimeOffset.Now;
        }

        public MailMessage ToMailMessage()
        {
            var result = new MailMessage
            {
                Body = Body,
                IsBodyHtml = true,
                Subject = Subject,
                From = new MailAddress(From)
            };

            if (!string.IsNullOrWhiteSpace(CC))
                CC.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => result.CC.Add(x));

            if (!string.IsNullOrWhiteSpace(Bcc))
                Bcc.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => result.Bcc.Add(x));

            if (!string.IsNullOrWhiteSpace(To))
                To.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => result.To.Add(x));

            return result;
        }

        public static MailItem FromMailMessage(MailMessage message)
        {
            return new MailItem
            {
                Body = message.Body,
                Subject = message.Subject,
                To = string.Join(",", message.To.Select(x => x.Address)),
                From = message.From?.Address ?? "no-reply@linde.com",
                Bcc = string.Join(",", message.Bcc.Select(x => x.Address)),
                CC = string.Join(",", message.CC.Select(x => x.Address))
            };
        }
    }
}
