﻿using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Database.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers
{
    public class UserReadByIdConsumer : IConsumer<UserReadByIdRequestServiceToDataBase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserReadByIdConsumer(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserReadByIdRequestServiceToDataBase> context)
        {
            var fetchUser = await _unitOfWork.Users.FindAsync(user => user.Id.Equals(context.Message.Id));

            var returnUser = _mapper.Map<CommunicationModel<DataUserModel>>(fetchUser);

            await context.RespondAsync(returnUser);
        }
    }
}
