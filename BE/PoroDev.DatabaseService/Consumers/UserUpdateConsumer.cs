using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
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
            var model = _mapper.Map<DataUserModel>(context.Message);

            var userToBeUpdated = await _unitOfWork.Users.FindAsync(user => user.Email.Trim().Equals(model.Email.Trim()));

            if(userToBeUpdated.ExceptionName != null)
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


            var updatedUser = await _unitOfWork.Users.UpdateAsync(updatedModel.Entity, updatedModel.Entity.Id);
            await _unitOfWork.SaveChanges();

            var returnModel = _mapper.Map<CommunicationModel<DataUserModel>>(updatedUser);
            await context.RespondAsync(returnModel);
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