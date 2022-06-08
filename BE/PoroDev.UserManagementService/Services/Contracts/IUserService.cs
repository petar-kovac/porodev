using Data.Access.Layer.Models;
using PoroDev.UserManagementService.Models.UserModels;

namespace PoroDev.UserManagementService.Services.Contracts
{
    public interface IUserService
    {
        Task<Guid> CreateUser(UserCreateRequestModel model);

        Task<DataUserModel> GetUserByMail(string mail);

        Task<UserCreateRequestModel> UpdateUser(UserCreateRequestModel model);

        Task<UserCreateRequestModel> DeleteUser(string mail);

        Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, Enums.UserRole role);

        Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel);
    }
}