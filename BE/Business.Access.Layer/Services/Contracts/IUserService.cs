using Business.Access.Layer.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Services.Contracts
{
    public interface IUserService
    {
        Task<Guid?> Create(BusinessUserModel model);

        Task<BusinessUserModel> GetByMail(string mail);

        Task<BusinessUserModel> Update(BusinessUserModel model);

        Task<BusinessUserModel> Delete(string mail);
    }
}
