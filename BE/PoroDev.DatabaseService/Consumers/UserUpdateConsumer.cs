using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;
using static PoroDev.Database.Constants.Constants;

namespace PoroDev.Database.Consumers
{
    public class UserUpdateConsumer : IConsumer<UserUpdateRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserUpdateConsumer(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserUpdateRequestServiceToDatabase> context)
        {
            UserUpdateResponseDatabaseToService returnModel;
            DataUserModel model = new()
            {
                AvatarUrl = context.Message.AvatarUrl,
                Department = context.Message.Department,
                Email = context.Message.Email,
                Lastname = context.Message.Lastname,
                Name = context.Message.Name,
                Position = context.Message.Position,
                Role = context.Message.Role,
                Password = context.Message.Password,
                Salt = context.Message.Salt,
            };

            var userToBeUpdated = await _unitOfWork.Users.FindAsync(user => user.Email.Trim().Equals(model.Email.Trim()));

            var updatedModel = _mapper.Map<DataUserModel>(model);
            updatedModel.Id = userToBeUpdated.Entity.Id;
            updatedModel.DateCreated = userToBeUpdated.Entity.DateCreated;

            //I hash&salt password inside user service
            updatedModel.Password = userToBeUpdated.Entity.Password;
            updatedModel.Salt = userToBeUpdated.Entity.Salt;

            try
            {
                await _unitOfWork.Users.UpdateAsync(updatedModel, updatedModel.Id);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception exception)
            {
                returnModel = CreateResponseModel<UserUpdateResponseDatabaseToService, DataUserModel>(nameof(exception), InternalDatabaseError);
                await context.RespondAsync(returnModel);
            }

            var updatedModelResponse = CreateResponseModel<UserUpdateResponseDatabaseToService, DataUserModel>(updatedModel);
            await context.RespondAsync<UserUpdateResponseDatabaseToService>(updatedModelResponse);
        }
    }

    /* public async Task Consume(ConsumeContext<UserUpdateRequestServiceToDatabase> context)
     {
         DataUserModel model = new()
         {
             AvatarUrl = context.Message.AvatarUrl,
             Department = context.Message.Department,
             Email = context.Message.Email,
             Lastname = context.Message.Lastname,
             Name = context.Message.Name,
             Position = context.Message.Position,
             Role = context.Message.Role,
             Password = context.Message.Password,
             Salt = context.Message.Salt,
         };

         var userToBeUpdated = await _unitOfWork.Users.FindAsync(user => user.Email.Trim().Equals(model.Email.Trim()));

         if (userToBeUpdated == null)
         {
             var updatedModelException = CreateResponseModel<UserUpdateResponseDatabaseToService, DataUserModel>("KeyNotFoundException", "User with this email doesn't exists!");
             await context.RespondAsync<UserUpdateResponseDatabaseToService>(updatedModelException);
         }
         else
         {
             var updatedModel = _mapper.Map<DataUserModel>(model);
             updatedModel.Id = userToBeUpdated.Entity.Id;
             updatedModel.DateCreated = userToBeUpdated.DateCreated;

             //I hash&salt password inside user service
             updatedModel.Password = userToBeUpdated.Password;
             updatedModel.Salt = userToBeUpdated.Salt;

             await _unitOfWork.Users.UpdateAsync(updatedModel, updatedModel.Id);
             await _unitOfWork.SaveChanges();

             var updatedModelResponse = CreateResponse<UserUpdateResponseDatabaseToService, DataUserModel>.CreateResponseModel(updatedModel);
             await context.RespondAsync<UserUpdateResponseDatabaseToService>(updatedModelResponse);
         }
     }*/
}