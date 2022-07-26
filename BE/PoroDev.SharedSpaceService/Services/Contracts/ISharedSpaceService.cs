﻿using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.SharedSpace.AddFile;
using PoroDev.Common.Contracts.SharedSpace.AddUser;
using PoroDev.Common.Contracts.SharedSpace.Create;
using PoroDev.Common.Contracts.SharedSpace.GetAllUsers;
using PoroDev.Common.Contracts.SharedSpace.QueryFiles;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.SharedSpaceService.Services.Contracts
{
    public interface ISharedSpaceService
    {
        Task<CommunicationModel<SharedSpace>> Create(CreateSharedSpaceRequestGatewayToService createModel);

        Task<CommunicationModel<SharedSpacesUsers>> AddUserToSharedSpace(AddUserToSharedSpaceRequestGatewayToService addModel);

        Task<CommunicationModel<SharedSpacesFiles>> AddFile(AddFileToSharedSpaceGatewayToService requestModel);

        Task<CommunicationModel<List<QueryFilesResponse>>> QueryFiles(QueryFilesServiceToDatabase requestModel);

        Task<CommunicationModel<List<DataUserModel>>> GetAllUsersFromSharedSpace(GetAllUsersFromSharedSpaceRequestGatewayToService model);
    }
}