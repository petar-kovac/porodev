using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.RegisterUser;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class RegisterUserConsumer : BaseDbConsumer, IConsumer<RegisterUserRequestServiceToDatabase>
    {
        public RegisterUserConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task Consume(ConsumeContext<RegisterUserRequestServiceToDatabase> context)
        {
            var modelForDB = _mapper.Map<DataUserModel>(context.Message);

            var dbResponse = await _unitOfWork.Users.CreateAsync(modelForDB);
            await _unitOfWork.SaveChanges();

            var responseModel = _mapper.Map<CommunicationModel<DataUserModel>>(dbResponse);

            await context.RespondAsync(responseModel);
        }
    }
}