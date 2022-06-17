using Business.Access.Layer.Models.UserModels;
using Data.Access.Layer.Models;

namespace Business.Access.Layer.Services.Contracts
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