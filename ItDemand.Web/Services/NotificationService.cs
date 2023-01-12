using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using ItDemand.Web.Models;
using System.Net.Mail;

namespace ItDemand.Web.Services
{
    public class MailOptions
    {
        public string SmtpHost { get; set; } = "smtprelay.linde.grp";
        public int SmtpPort { get; set; } = 25;
        public string SmtpUsername { get; set; } = "s0aj56";
        public string SmtpPassword { get; set; } = "Fm5@91274%i1t497";
        public string PickupDirectory { get; set; } = string.Empty;
        public MailAddress NoReplyAddress { get; set; } = new MailAddress("itdemand@linde.com", "IT Demand System");
    }

    public class NotificationService
    {
        private readonly ApplicationLog _log;
        private readonly ItDemandContext _db;

        public MailOptions Options { get; private set; } = new MailOptions();

        private readonly string _baseUrl;
        private readonly bool _isTesting;

        private const string Disclaimer =
            "<strong><em>Note: this e-mail was sent from a notification-only email address that cannot accept incoming e-mail. Please do not reply to this message.</em></strong>";

        private const string TestWarning =
            "<p><strong>***** This email is originating from a testing site! The email is only relevant to the specific testing being done and should not be considered business related.  *****</strong></p>";

        public NotificationService(ApplicationLog log, ItDemandContext dbContext)
        {
            _db = dbContext;
            _log = log;

#if DEBUG
            _baseUrl = "http://localhost:42472";
            _isTesting = true;
            Options.PickupDirectory = @"c:\temp\itdemand";
            Options.NoReplyAddress = new MailAddress("itdemand-dev@linde.com", "IT Demand System (Dev)");
#elif STAGING
            _baseUrl = "http://itdemandstage.linde.grp";
            _isTesting = true;
            Options.NoReplyAddress = new MailAddress("itdemand-stage@linde.com", "IT Demand System (Stage)");
#else
            _baseUrl = "http://itdemand.linde.grp";
            _isTesting = false;
#endif
        }

        public void NewDemandRequestSubmitted(NotifySubmitForReviewModel model)
        {
            var mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;

            mailMessage.Body =
                $@"<p>A new IT Demand Request has been submitted for review in the <a href=""{_baseUrl}"">IT Demand System</a>.</p>
                   <table>
                   <tbody>
                   <tr>
				   <td><strong>Demand ID:</strong></td><td></td><td>IT-Demand-{model.DemandId}</td>
			       </tr>
                   <tr>
				   <td><strong>Demand Name:</strong></td><td></td><td><a href=""{_baseUrl}/Demand/{model.DemandId}"">{model.DemandName}</a></td>
			       </tr>
                   </tbody>
	               </table>
                   <p>{Disclaimer}</p>";

            // Set To: to be anyone assigned the PMO permission
            var pmoGroup = _db.Users.Where(x => x.SecurityRole.HasFlag(Domain.Enums.SecurityRole.Pmo)).ToArray();
            foreach (var assignment in pmoGroup)
            {
                if (!string.IsNullOrEmpty(assignment.Email))
                {
                    mailMessage.To.Add(new MailAddress(assignment.Email, assignment.DisplayName));
                }
            }

            // Set CC: to be anyone assigned the Architecture permission
            var architectureGroup = _db.Users.Where(x => x.SecurityRole.HasFlag(Domain.Enums.SecurityRole.Architecture)).ToArray();

            foreach (var assignment in architectureGroup)
            {
                if (!string.IsNullOrEmpty(assignment.Email))
                {
                    mailMessage.CC.Add(new MailAddress(assignment.Email, assignment.DisplayName));
                }
            }

            // Also CC: the request owner
            if (!string.IsNullOrEmpty(model.RequestOwner.Email))
            {
                mailMessage.CC.Add(new MailAddress(model.RequestOwner.Email, model.RequestOwner.DisplayName));
            }

            //if (_developerBccForITEnabled) mailMessage.Bcc.Add(new MailAddress("Richard.Dillon@linde.com"));

            mailMessage.Subject = "[IT Demand System] - New Demand Request" +
                                  (string.IsNullOrEmpty(model.DemandName) ? "" : ": " + model.DemandName);

            SendMail(mailMessage);
        }

        public void RequestCorrections(NotifyCorrectionRequestModel model)
        {
            var mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;

            mailMessage.Body =
                $@"<p>Additional information and/or corrections have been requested by IT PMO for the Demand listed below before it can be approved and categorized.</p>
                    <table>
                    <tbody>
                    <tr>
				    <td><strong>Demand ID:</strong></td><td></td><td>IT-Demand-{model.DemandId}</td>
			        </tr>
                    <tr>
				    <td><strong>Demand Name:</strong></td><td></td><td><a href=""{_baseUrl}/Demand/{model.DemandId}"">{model.DemandName}</a></td>
			        </tr>
                    </tbody>
	                </table>
                    <p><strong>Correction Request Comments</strong></p>
                    <p>{model.CorrectionComments}</p>
                    <p>{Disclaimer}</p>";

            if (!string.IsNullOrEmpty(model.RequestOwner.Email))
            {
                mailMessage.To.Add(new MailAddress(model.RequestOwner.Email, model.RequestOwner.DisplayName));
            }

            if (!string.IsNullOrEmpty(model.RequestedBy.Email))
            {
                mailMessage.CC.Add(new MailAddress(model.RequestedBy.Email, model.RequestedBy.DisplayName));
            }

            //if (_developerBccForITEnabled) mailMessage.Bcc.Add(new MailAddress("Richard.Dillon@linde.com"));
            //mailMessage.Bcc.Add(new MailAddress("Richard.Dillon@linde.com"));

            mailMessage.Subject = "[IT Demand System] - Demand Submission Correction Request" +
                                  (string.IsNullOrEmpty(model.DemandName) ? "" : ": " + model.DemandName);

            SendMail(mailMessage);
        }

        private void SendMail(MailMessage mailMessage)
        {
            if (_isTesting)
                mailMessage.Body = string.Concat(TestWarning, mailMessage.Body);

            mailMessage.From = Options.NoReplyAddress;

            var mailItem = MailItem.FromMailMessage(mailMessage);

            using (var client = new SmtpClient(Options.SmtpHost, Options.SmtpPort))
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(Options.PickupDirectory))
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        client.PickupDirectoryLocation = Options.PickupDirectory;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(Options.SmtpUsername))
                            client.Credentials = new System.Net.NetworkCredential(Options.SmtpUsername, Options.SmtpPassword);
                        else
                            client.UseDefaultCredentials = true;

                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    }

                    client.Send(mailMessage);
                    mailItem.Sent = DateTimeOffset.Now;
                }
                catch (Exception ex)
                {
                    mailItem.Error = ex.Message;
                }
            }

            if (string.IsNullOrEmpty(mailItem.To))
            {
                mailItem.To = "missing_to@linde.com";
            }

            _db.Add(mailItem);
            _db.SaveChanges();
        }
    }
}
