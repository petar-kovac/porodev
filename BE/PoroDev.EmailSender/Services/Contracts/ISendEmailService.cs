using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.EmailSender;
using PoroDev.Common.Models.EmailSenderModels;

namespace PoroDev.EmailSender.Services.Contracts
{
    public interface ISendEmailService
    {
        Task<CommunicationModel<SendEmailModel>> SendEmail(SendEmailRequest emailModel);
    }
}
