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
                var client = new SendGridClient("");//enter your sendgrid api key 
                EmailAddress from = new EmailAddress("srdjan.coralic@htecgroup.com");
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
    }
}