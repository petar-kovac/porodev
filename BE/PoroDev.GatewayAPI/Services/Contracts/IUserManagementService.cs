﻿using Microsoft.AspNetCore.Mvc;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Contracts.DeleteUser;
using PoroDev.Common.Contracts.Update;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.GatewayAPI.Services.Contracts
{
    public interface IUserManagementService
    {
        Task<DataUserModel> CreateUser(UserCreateRequestGatewayToService createModel);

        Task DeleteUser(UserDeleteRequestGatewayToService deleteModel);

        Task<DataUserModel> UpdateUser(UserUpdateRequestGatewayToService updateModel);

    }
}
