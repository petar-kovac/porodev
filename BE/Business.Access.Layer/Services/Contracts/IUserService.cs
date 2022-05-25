using Business.Access.Layer.Models.UserModels;

namespace Business.Access.Layer.Services.Contracts
{
    public interface IUserService
    {
        Task<Guid?> CreateUser(BusinessUserModel model);

        Task<BusinessUserModel> GetUserByMail(string mail);

        Task<BusinessUserModel> UpdateUser(BusinessUserModel model);

        Task<BusinessUserModel> DeleteUser(string mail);
    }
}