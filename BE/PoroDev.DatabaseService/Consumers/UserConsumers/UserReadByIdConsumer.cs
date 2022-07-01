using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserReadByIdConsumer : BaseDbConsumer, IConsumer<UserReadByIdRequestServiceToDataBase>
    {
        public UserReadByIdConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task Consume(ConsumeContext<UserReadByIdRequestServiceToDataBase> context)
        {
            var fetchUser = await _unitOfWork.Users.FindAsync(user => user.Id.Equals(context.Message.Id));

            var returnUser = _mapper.Map<CommunicationModel<DataUserModel>>(fetchUser);

            await context.RespondAsync(returnUser);
        }
    }
}
