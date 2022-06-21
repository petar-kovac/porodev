using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserCreateConsumer : BaseDbConsumer, IConsumer<UserCreateRequestServiceToDatabase>
    {
        public UserCreateConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task Consume(ConsumeContext<UserCreateRequestServiceToDatabase> context)
        {
            var modelForDB = _mapper.Map<DataUserModel>(context.Message);

            var dbReturn = await _unitOfWork.Users.CreateAsync(modelForDB);
            await _unitOfWork.SaveChanges();

            var returnModel = _mapper.Map<CommunicationModel<DataUserModel>>(dbReturn);

            await context.RespondAsync(returnModel);
        }
    }
}