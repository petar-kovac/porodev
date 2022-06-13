using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Exceptions;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;
using System.Reflection;

namespace PoroDev.Database.Consumers
{
    public class UserCreateConsumer : IConsumer<UserCreateRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCreateConsumer(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserCreateRequestServiceToDatabase> context)
        {
            var modelForDB = _mapper.Map<DataUserModel>(context.Message);

            try
            {
                await _unitOfWork.Users.CreateAsync(modelForDB);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                string exceptionName = nameof(DatabaseException);
                string humanReadableMessage = "There was a problem with the database!" + $" {ex.Message}";

                var returnModelException = CreateResponseModel<UserCreateResponseDatabaseToService, DataUserModel>(exceptionName, humanReadableMessage);

                await context.RespondAsync(returnModelException);
            }

            var returnModel = CreateResponseModel<UserCreateResponseDatabaseToService, DataUserModel>(modelForDB);

            await context.RespondAsync(returnModel);
        }
    }
}
