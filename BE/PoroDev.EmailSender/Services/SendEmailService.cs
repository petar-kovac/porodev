using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.EmailSender;
using PoroDev.Common.Models.EmailSenderModels;
using PoroDev.EmailSender.Services.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace PoroDev.EmailSender.Services
{
    public class SendEmailService : ISendEmailService
    {
        public async Task<CommunicationModel<SendEmailModel>> SendEmail(SendEmailRequest emailModel)
        {
            try
            {
                var client = new SendGridClient("");//enter your api key 
                EmailAddress from = new EmailAddress("srdjan.stanojcic@htecgroup.com");
                EmailAddress to = new EmailAddress(emailModel.EmailReceiver);
                string subject = emailModel.Subject;
                string plainTextContent = emailModel.plainTextContent;
                string htmlTextContent = emailModel.htmlTextContent;

                var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlTextContent);
                var response = await client.SendEmailAsync(message);
                Console.WriteLine(response.StatusCode.ToString());

                var returnModel = new CommunicationModel<SendEmailModel>()
                {
                    Entity = new SendEmailModel() { StatusCode = response.StatusCode },
                    ExceptionName = null,
                    HumanReadableMessage = null
                };
                return returnModel;
            }
            catch (Exception ex)
            {
                var returnModel = new CommunicationModel<SendEmailModel>()
                {
                    Entity = null,
                    ExceptionName = nameof(ex),
                    HumanReadableMessage = ex.Message
                };
                return returnModel;
            }
        }

        public async Task<CommunicationModel<SendEmailModel>> SendVerificationMail(string reciever)
        {
            var client = new SendGridClient("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");
            EmailAddress from = new EmailAddress(reciever);
            EmailAddress to = new EmailAddress("srdjanstanojcic031@gmail.com", "Blabla");
            string subject = "Verification email";
            string plainTextContent = "Verification email.";
            string htmlTextContent = "<h1>Confirm an email</h1><br><p style='color:blue'>Please confirm that you wanted to register on PoroDev platform.</p>";

            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlTextContent);
            var response = await client.SendEmailAsync(message);
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                var returnmodel = new CommunicationModel<SendEmailModel>()
                {
                    Entity = new SendEmailModel() { StatusCode = response.StatusCode },
                    ExceptionName = null,
                    HumanReadableMessage = null
                };
                return returnmodel;
            }
            return null;
        }
    }
}