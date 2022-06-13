using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;

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

            var dbReturn = await _unitOfWork.Users.CreateAsync(modelForDB);
            await _unitOfWork.SaveChanges();

            var returnModel = _mapper.Map<UserCreateResponseDatabaseToService>(dbReturn);

            await context.RespondAsync(returnModel);
        }
    }
}