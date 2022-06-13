﻿using MassTransit;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;

namespace PoroDev.Database.Consumers
{
    public class UserReadByEmailConsumer : IConsumer<UserReadByEmailRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserReadByEmailConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserReadByEmailRequestServiceToDatabase> context)
        {
            try
            {
                var fetchUser = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email.Trim()));

                if (fetchUser == null)
                {
                    throw new UserNotFoundException("User with this email does not exist.");
                }

                var returnUser = CreateResponseModel<UserReadByEmailResponseDatabaseToService, DataUserModel>(fetchUser.Entity);

                await context.RespondAsync(returnUser);
            }
            catch (UserNotFoundException ex)
            {
                string exceptionName = nameof(UserNotFoundException);
                string humanReadableMessage = ex.HumanReadableErrorMessage;

                var returnUser = CreateResponseModel<UserReadByEmailResponseDatabaseToService, DataUserModel>(exceptionName, humanReadableMessage);

                await context.RespondAsync(returnUser);
            }
            catch (Exception)
            {
                string exceptionName = nameof(DatabaseException);
                string humanReadableMessage = "Internal database error.";

                var returnUser = CreateResponseModel<UserReadByEmailResponseDatabaseToService, DataUserModel>(exceptionName, humanReadableMessage);

                await context.RespondAsync(returnUser);
            }
        }
    }
}