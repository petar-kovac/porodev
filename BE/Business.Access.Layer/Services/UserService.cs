using AutoMapper;
using Business.Access.Layer.Exceptions;
using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using Business.Access.Layer.Models.UserModels;
using Business.Access.Layer.Services.Contracts;
using Data.Access.Layer.Models;
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
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Guid?> Create(BusinessUserModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessUserModel> Delete(string mail)
        {
            if (mail == null)
                throw new AppException("Mail is null.");
            
            var query = _unitOfWork.Users.Query();

            if (query.SingleOrDefault(user => user.Email.Equals(mail)) == null)
                throw new UserNotFoundException("User with that email doesn't exist.");

            var userReturnModel = _mapper.Map<BusinessUserModel>(query.First());

            _unitOfWork.Users.Delete(query.First());

            await _unitOfWork.SaveChanges();

            return userReturnModel;
            
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
