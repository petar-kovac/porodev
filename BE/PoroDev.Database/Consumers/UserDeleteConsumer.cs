using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.DeleteUser;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Database.Repositories.Contracts;
using PoroDev.Common.Exceptions;
using static PoroDev.Database.Constants.Constants;
using PoroDev.Common.Models.UnitOfWorkResponse;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Contracts;

namespace PoroDev.Database.Consumers
{
    public class UserDeleteConsumer : IConsumer<UserDeleteRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserDeleteConsumer(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserDeleteRequestServiceToDatabase> context)
        {
            CommunicationModel<DeleteUserModel> returnModel;
            var userToDelete = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email.Trim()));
          
            if (userToDelete.ExceptionName != null)
            {
                returnModel = _mapper.Map<CommunicationModel<DeleteUserModel>>(userToDelete);
                await context.RespondAsync(returnModel);
                return;
            }

            var deletedUser = await _unitOfWork.Users.Delete(userToDelete.Entity);
            await _unitOfWork.SaveChanges();

            returnModel = _mapper.Map<CommunicationModel<DeleteUserModel>>(deletedUser);

            await context.RespondAsync(returnModel);
        }
    }
}
