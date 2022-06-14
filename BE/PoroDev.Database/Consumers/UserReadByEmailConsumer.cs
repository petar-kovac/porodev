using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;

namespace PoroDev.Database.Consumers
{
    public class UserReadByEmailConsumer : IConsumer<UserReadByEmailRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserReadByEmailConsumer(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserReadByEmailRequestServiceToDatabase> context)
        {
            var fetchUser = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email.Trim()));

            var returnUser = _mapper.Map<CommunicationModel<DataUserModel>>(fetchUser);

            await context.RespondAsync(returnUser);

            //try
            //{
            //    var fetchUser = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email.Trim()));

            //    if (fetchUser == null)
            //    {
            //        throw new UserNotFoundException("User with this email does not exist.");
            //    }

            //    var returnUser = CreateResponseModel<UserReadByEmailResponseDatabaseToService, DataUserModel>(fetchUser.Entity);

            //    await context.RespondAsync(returnUser);
            //}
            //catch (UserNotFoundException ex)
            //{
            //    string exceptionName = nameof(UserNotFoundException);
            //    string humanReadableMessage = ex.HumanReadableErrorMessage;

            //    var returnUser = CreateResponseModel<UserReadByEmailResponseDatabaseToService, DataUserModel>(exceptionName, humanReadableMessage);

            //    await context.RespondAsync(returnUser);
            //}
            //catch (Exception)
            //{
            //    string exceptionName = nameof(DatabaseException);
            //    string humanReadableMessage = "Internal database error.";

            //    var returnUser = CreateResponseModel<UserReadByEmailResponseDatabaseToService, DataUserModel>(exceptionName, humanReadableMessage);

            //    await context.RespondAsync(returnUser);
            //}
        }
    }
}