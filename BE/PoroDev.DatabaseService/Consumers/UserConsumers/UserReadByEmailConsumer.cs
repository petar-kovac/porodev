using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserReadByEmailConsumer : BaseDbConsumer, IConsumer<UserReadByEmailRequestServiceToDatabase>
    {
        public UserReadByEmailConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task Consume(ConsumeContext<UserReadByEmailRequestServiceToDatabase> context)
        {
            var fetchUser = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email.Trim()));

            var returnUser = _mapper.Map<CommunicationModel<DataUserModel>>(fetchUser);

            await context.RespondAsync(returnUser);
        }
    }
}