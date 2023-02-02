using ItDemand.Domain.DataContext;
using ItDemand.Domain.Models;
using ItDemand.Web.Models;
using System.Net.Mail;
using System.Text;

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
        private readonly ItDemandContext _db;

        public MailOptions Options { get; private set; } = new MailOptions();

        private readonly string _baseUrl;
        private readonly bool _isTesting;

        private const string Disclaimer =
            "<strong><em>Note: this e-mail was sent from a notification-only email address that cannot accept incoming e-mail. Please do not reply to this message.</em></strong>";

        private const string TestWarning =
            "<p><strong>***** This email is originating from a testing site! The email is only relevant to the specific testing being done and should not be considered business related.  *****</strong></p>";

        public NotificationService(ItDemandContext dbContext)
        {
            _db = dbContext;

#if DEBUG
            _baseUrl = "http://localhost:42472";
            _isTesting = true;
            Options.PickupDirectory = @"c:\temp\itdemand";
            Options.NoReplyAddress = new MailAddress("itdemand-dev@linde.com", "IT Demand System (Dev)");
#elif STAGING
            _baseUrl = "https://itdemandstage.linde.grp";
            _isTesting = true;
            Options.PickupDirectory = @"c:\temp\itdemandemails";
            Options.NoReplyAddress = new MailAddress("itdemand-stage@linde.com", "IT Demand System (Stage)");
#else
            _baseUrl = "https://itdemand.linde.grp";
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
				   <td><strong>Demand ID:</strong></td><td></td><td>GPS-IT-{model.DemandId}</td>
			       </tr>
                   <tr>
				   <td><strong>Demand Name:</strong></td><td></td><td><a href=""{_baseUrl}/Demand/DemandRequestForm/{model.DemandId}"">{model.DemandName}</a></td>
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
				    <td><strong>Demand ID:</strong></td><td></td><td>GPS-IT-{model.DemandId}</td>
			        </tr>
                    <tr>
				    <td><strong>Demand Name:</strong></td><td></td><td><a href=""{_baseUrl}/Demand/DemandRequestForm/{model.DemandId}"">{model.DemandName}</a></td>
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

        #region Checklist Approval Notifications
        public void SendChecklistApprovalRequest(ChecklistApprovalNotificationModel model)
        {
            var mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;

            var body = new StringBuilder();

            body.AppendLine(
                $@"<p>A <strong>{model.ChecklistTitle}</strong> checklist in the <a href=""{_baseUrl}"">IT Demand Management System</a> has been submitted for approval. Click the link below to review this checklist.</p>
					<table>
						<tbody>
							<tr>
								<td><strong>Checklist:</strong></td><td></td><td><a href=""{_baseUrl}{model.ChecklistRelativePath}"">{model.ChecklistTitle}</a></td>
							</tr>
							<tr>
								<td><strong>Demand Id:</strong></td><td></td><td>GPS-IT-{model.DemandId}</td>
							</tr>
							<tr>
								<td><strong>Demand Name:</strong></td><td></td><td>{model.DemandName}</td>
							</tr>
						</tbody>
					</table>"
                );

            body.AppendLine(
                $@"<p>To Approve:
						<ol>
							<li>Click the <strong>Click to Sign</strong> button next to your name.</li>
							<li>Ensure the date is correct in the date field below your approval box.</li>
							<li>(Optionally) Add feedback or comments in the Approver Comments section.</li> 
							<li>Click the <strong>Save</strong> button located at the bottom right of the form.</li>
						</ol>
					</p>
					<p>To Reject & Request Corrections:
						<ol>
							<li>Add feedback or comments in the Approver Comments section.</li>
							<li>Click the <strong>Reject & Request Corrections</strong> button on the bottom left of the page.</li>
							<li>Checklist will be marked as Rejected & Requested Corrections.</li>
							<li>An email will be sent to the submitter informing them of the request (including any comments made).</li>
						</ol>
					</p>"
                );

            body.AppendLine($@"<p>{Disclaimer}</p>");

            mailMessage.Body = body.ToString();

            mailMessage.Subject = $"[IT Demand System] - {model.ChecklistTitle} Approval Request (GPS-IT-{model.DemandId})";

            foreach (var approver in model.Approvers)
            {
                if (!string.IsNullOrEmpty(approver.Email))
                {
                    mailMessage.To.Add(new MailAddress(approver.Email, approver.DisplayName));
                }
            }

            if (!string.IsNullOrEmpty(model.SentBy.Email))
            {
                mailMessage.CC.Add(new MailAddress(model.SentBy.Email, model.SentBy.DisplayName));
            }

            SendMail(mailMessage);
        }

        public void SendChecklistCorrectionsRequest(ChecklistApprovalNotificationModel model)
        {
            var mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;

            var body = new StringBuilder();

            body.AppendLine(
                $@"<p>The following Checklist in the <a href=""{_baseUrl}"">IT Demand Management System</a> has been sent back with a request for corrections. Click the link below to review this checklist.</p>
					<table>
						<tbody>
							<tr>
								<td><strong>Checklist:</strong></td><td></td><td><a href=""{_baseUrl}{model.ChecklistRelativePath}"">{model.ChecklistTitle}</a></td>
							</tr>
							<tr>
								<td><strong>Demand Id:</strong></td><td></td><td>GPS-IT-{model.DemandId}</td>
							</tr>
							<tr>
								<td><strong>Demand Name:</strong></td><td></td><td>{model.DemandName}</td>
							</tr>
						</tbody>
					</table>"
                );

            body.AppendLine(
                $@"<p style=""text-decoration: underline;"">Approver Comments</p><p>{(string.IsNullOrEmpty(model.Comments) ? "None." : model.Comments)}</p>"
            );

            body.AppendLine($@"<p>{Disclaimer}</p>");

            mailMessage.Body = body.ToString();

            mailMessage.Subject = $"[IT Demand System] - {model.ChecklistTitle} Corrections Requested (GPS-IT-{model.DemandId})";

            if (!string.IsNullOrEmpty(model.SentBy.Email))
            {
                mailMessage.CC.Add(new MailAddress(model.SentBy.Email, model.SentBy.DisplayName));
            }

            foreach (var approver in model.Approvers)
            {
                if (!string.IsNullOrEmpty(approver.Email))
                {
                    mailMessage.CC.Add(new MailAddress(approver.Email, approver.DisplayName));
                }
            }

            SendMail(mailMessage);
        }

        public void SendChecklistApproved(ChecklistApprovalNotificationModel model)
        {
            var mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;

            var body = new StringBuilder();

            body.AppendLine(
                $@"<p>The following Checklist in the <a href=""{_baseUrl}"">IT Demand Management System</a> has been approved. Click the link below to review this checklist.</p>
					<table>
						<tbody>
							<tr>
								<td><strong>Checklist:</strong></td><td></td><td><a href=""{_baseUrl}{model.ChecklistRelativePath}"">{model.ChecklistTitle}</a></td>
							</tr>
							<tr>
								<td><strong>Demand Id:</strong></td><td></td><td>GPS-IT-{model.DemandId}</td>
							</tr>
							<tr>
								<td><strong>Demand Name:</strong></td><td></td><td>{model.DemandName}</td>
							</tr>
						</tbody>
					</table>"
                );

            body.AppendLine(
                $@"<p style=""text-decoration: underline;"">Approver Comments</p><p>{(string.IsNullOrEmpty(model.Comments) ? "None." : model.Comments)}</p>"
            );

            body.AppendLine($@"<p>{Disclaimer}</p>");

            mailMessage.Body = body.ToString();

            mailMessage.Subject = $"[IT Demand System] - {model.ChecklistTitle} Approved (GPS-IT-{model.DemandId})";

            if (!string.IsNullOrEmpty(model.SentBy.Email))
            {
                mailMessage.CC.Add(new MailAddress(model.SentBy.Email, model.SentBy.DisplayName));
            }

            foreach (var approver in model.Approvers)
            {
                if (!string.IsNullOrEmpty(approver.Email))
                {
                    mailMessage.CC.Add(new MailAddress(approver.Email, approver.DisplayName));
                }
            }

            SendMail(mailMessage);
        }


        #endregion

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
