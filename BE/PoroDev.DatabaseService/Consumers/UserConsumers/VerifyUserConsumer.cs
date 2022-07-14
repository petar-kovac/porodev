using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.Verify;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.DatabaseService.Constants.Constants;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class VerifyUserConsumer : BaseDbConsumer, IConsumer<VerifyEmailRequestServiceToDatabase>
    {
        public VerifyUserConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public async Task Consume(ConsumeContext<VerifyEmailRequestServiceToDatabase> context)
        {
            var userToVerify = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email));

            if(userToVerify.ExceptionName != null)
            {
                await context.RespondAsync<CommunicationModel<DataUserModel>>(userToVerify);
            }

            if(userToVerify.Entity.VerificationToken.Equals(context.Message.Token))
            {
                userToVerify.Entity.VerifiedAt = DateTime.Now;
                await _unitOfWork.SaveChanges();
                await context.RespondAsync<CommunicationModel<DataUserModel>>(userToVerify);
            }
            else
            {
                var returnModel = new CommunicationModel<DataUserModel>()
                {
                    Entity = null,
                    ExceptionName = nameof(InvalidVerificationTokenException),
                    HumanReadableMessage = InvalidTokenExceptionMessage
                };
                await context.RespondAsync<CommunicationModel<DataUserModel>>(returnModel);
            }
        }
    }
}
