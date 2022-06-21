﻿using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.LoginUser;
using PoroDev.Common.Contracts.ReadUser;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Contracts.UserMenagement.ReadById;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.DeleteUser;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.Common.Models.UserModels.RegisterUser;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<CommunicationModel<DataUserModel>> CreateUser(UserCreateRequestGatewayToService model);

        Task<CommunicationModel<DataUserModel>> ReadUserByEmail(UserReadByEmailRequestGatewayToService model);

        Task<CommunicationModel<DataUserModel>> ReadUserById(UserReadByIdRequestGatewayToService model);
        
        Task<CommunicationModel<DataUserModel>> UpdateUser(UserUpdateRequestGatewayToService model);

        Task<CommunicationModel<DeleteUserModel>> DeleteUser(UserDeleteRequestGatewayToService model);

        Task<CommunicationModel<RegisterUserResponse>> RegisterUser(RegisterUserRequestGatewayToService registerModel);

        Task<CommunicationModel<LoginUserModel>> LoginUser(UserLoginRequestGatewayToService userToLoginModel);
    }
}