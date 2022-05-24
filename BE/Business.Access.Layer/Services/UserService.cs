using Business.Access.Layer.Models.UserModels;
using Business.Access.Layer.Services.Contracts;
using Data.Access.Layer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Guid?> Create(BusinessUserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BusinessUserModel> Delete(string mail)
        {
            throw new NotImplementedException();
        }

        public Task<BusinessUserModel> GetByMail(string mail)
        {
            throw new NotImplementedException();
        }

        public Task<BusinessUserModel> Update(string mail, BusinessUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
