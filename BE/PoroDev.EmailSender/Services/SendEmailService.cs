using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.EmailSender;
using PoroDev.Common.Models.EmailSenderModels;
using PoroDev.EmailSender.Services.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using static PoroDev.Common.Extensions.CreateResponseExtension;

namespace PoroDev.EmailSender.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CommunicationModel<SendEmailModel>> SendEmail(SendEmailRequest emailModel)
        {
            try
            {
                var client = new SendGridClient(_configuration.GetValue<string>("SendGridSettings:ApiKey"));
                EmailAddress from = new EmailAddress(_configuration.GetValue<string>("SendGridSettings:EmailAdress"));
                EmailAddress to = new EmailAddress(emailModel.EmailReceiver);
                string subject = emailModel.Subject;
                string plainTextContent = emailModel.plainTextContent;
                string htmlTextContent = emailModel.htmlTextContent;

                var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlTextContent);
                var response = await client.SendEmailAsync(message);
                Console.WriteLine(response.StatusCode.ToString());
                var returnModel = CreateResponseModel<CommunicationModel<SendEmailModel>, SendEmailModel>(new SendEmailModel() { StatusCode = response.StatusCode });
                return returnModel;
            }
            catch (Exception exception)
            {
                var returnModel = CreateResponseModel<CommunicationModel<SendEmailModel>, SendEmailModel>(nameof(exception), exception.Message);
                return returnModel;
            }
        }
    }
}