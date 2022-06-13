using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Extensions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;

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

            var userToBeUpdated = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Trim().Equals(model.Email.Trim()));

            if (userToBeUpdated == null)
            {
                var updatedModelException = CreateResponse<UserUpdateResponseDatabaseToService, DataUserModel>.CreateResponseModel("KeyNotFoundException", "User with this email doesn't exists!");
                await context.RespondAsync<UserUpdateResponseDatabaseToService>(updatedModelException);
            }
            else
            {
                var updatedModel = _mapper.Map<DataUserModel>(model);
                updatedModel.Id = userToBeUpdated.Id;
                updatedModel.DateCreated = userToBeUpdated.DateCreated;

                //I hash&salt password inside user service
                updatedModel.Password = userToBeUpdated.Password;
                updatedModel.Salt = userToBeUpdated.Salt;

                await _unitOfWork.Users.UpdateAsync(updatedModel, updatedModel.Id);
                await _unitOfWork.SaveChanges();

                var updatedModelResponse = CreateResponse<UserUpdateResponseDatabaseToService, DataUserModel>.CreateResponseModel(updatedModel);
                await context.RespondAsync<UserUpdateResponseDatabaseToService>(updatedModelResponse);
            }
        }
    }
}