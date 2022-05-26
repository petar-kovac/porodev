using Business.Access.Layer.Models.UserModels;
using Data.Access.Layer.Models;

namespace Business.Access.Layer.Services.Contracts
{
    public interface IUserService
    {
        Task<Guid?> CreateUser(BusinessUserModel model);

        Task<DataUserModel> GetUserByMail(string mail);

        Task<BusinessUserModel> UpdateUser(BusinessUserModel model);

        Task<BusinessUserModel> DeleteUser(string mail);

        Task Register(UserRegisterModel registerModel);

        Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel);
    }
}