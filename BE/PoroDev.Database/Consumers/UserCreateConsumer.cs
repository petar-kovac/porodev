﻿using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;
using System.Reflection;

namespace PoroDev.Database.Consumers
{
    public class UserCreateConsumer : IConsumer<UserCreateRequestServiceToDatabase>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserCreateConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserCreateRequestServiceToDatabase> context)
        {
            var model = context.Message;

            DataUserModel modelForDB = new DataUserModel()
            {
                Id = model.Id,
                AvatarUrl = model.AvatarUrl,
                Department = model.Department,
                Email = model.Email,
                Lastname = model.Lastname,
                Name = model.Name,
                Position = model.Position,
                Role = model.Role,
                Password = model.Password,
                Salt = model.Salt,
                DateCreated = DateTime.Now
            };
            var createdModel = await _unitOfWork.Users.CreateAsync(modelForDB);
            await _unitOfWork.SaveChanges();

            UserCreateResponseDatabaseToService returnModel = new()
            {
                Entity = createdModel,
                ExceptionName = null,
                HumanReadableMessage = null
            };
            returnmodel = UserCreate<UserCreateResponseDatabaseToService, DataUserModel>(createdModel);
            returnmodel = UserCreate<UserCreateResponseDatabaseToService, DataUserModel>(exceprtion );



            await context.RespondAsync<UserCreateResponseDatabaseToService>(returnModel);
        }
    }
}
