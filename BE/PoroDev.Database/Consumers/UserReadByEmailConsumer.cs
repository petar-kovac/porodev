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
        }
    }
}