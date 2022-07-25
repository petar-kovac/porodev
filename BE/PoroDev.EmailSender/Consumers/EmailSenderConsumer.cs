using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.EmailSender;
using PoroDev.Common.Models.EmailSenderModels;
using PoroDev.EmailSender.Services.Contracts;

namespace PoroDev.EmailSender.Consumers
{
    public class EmailSenderConsumer : IConsumer<SendEmailRequest>
    {
        private readonly ISendEmailService _sendEmailService;

        public EmailSenderConsumer(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        public async Task Consume(ConsumeContext<SendEmailRequest> context)
        {
            try
            {
                var returnModel = await _sendEmailService.SendEmail(context.Message);
                await context.RespondAsync<CommunicationModel<SendEmailModel>>(returnModel);
            }
            catch (Exception ex)
            {
                var returnModel = new CommunicationModel<SendEmailModel>()
                {
                    Entity = null,
                    ExceptionName = nameof(ex),
                    HumanReadableMessage = ex.Message
                };
                await context.RespondAsync<CommunicationModel<SendEmailModel>>(returnModel);
            }
        }
    }
}