using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.Update;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserUpdateConsumer : BaseDbConsumer, IConsumer<UserUpdateRequestServiceToDatabase>
    {
        public UserUpdateConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<UserUpdateRequestServiceToDatabase> context)
        {
            var model = _mapper.Map<DataUserModel>(context.Message);

            var userToBeUpdated = await _unitOfWork.Users.FindAsync(user => user.Email.Trim().Equals(model.Email.Trim()));

            if (userToBeUpdated.ExceptionName != null)
            {
                var returnModelException = _mapper.Map<CommunicationModel<DataUserModel>>(userToBeUpdated);
                await context.RespondAsync(returnModelException);
                return;
            }

            CommunicationModel<DataUserModel> updatedModel = new()
            {
                Entity = model,
                ExceptionName = null,
                HumanReadableMessage = null
            };

            updatedModel.Entity.Id = userToBeUpdated.Entity.Id;
            updatedModel.Entity.DateCreated = userToBeUpdated.Entity.DateCreated;
            updatedModel.Entity.Password = userToBeUpdated.Entity.Password;
            updatedModel.Entity.Salt = userToBeUpdated.Entity.Salt;
            updatedModel.Entity.VerificationToken = userToBeUpdated.Entity.VerificationToken;
            updatedModel.Entity.VerifiedAt = userToBeUpdated.Entity.VerifiedAt;
            updatedModel.Entity.UsersSharedSpaces = userToBeUpdated.Entity.UsersSharedSpaces;
            updatedModel.Entity.fileDatas = userToBeUpdated.Entity.fileDatas;

            var updatedUser = await _unitOfWork.Users.UpdateAsync(updatedModel.Entity, updatedModel.Entity.Id);
            await _unitOfWork.SaveChanges();

            var returnModel = _mapper.Map<CommunicationModel<DataUserModel>>(updatedUser);
            await context.RespondAsync(returnModel);
        }
    }
}