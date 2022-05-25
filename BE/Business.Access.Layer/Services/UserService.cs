using AutoMapper;
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

        public async Task<Guid?> Create(BusinessUserModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessUserModel> Delete(string mail)
        {
            var userForDeletion = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(mail));

            if (userForDeletion == null)
                throw new KeyNotFoundException("User with that email doesn't exist!");

            var userReturnModel = _mapper.Map<BusinessUserModel>(userForDeletion);

            _unitOfWork.Users.Delete(userForDeletion);

            await _unitOfWork.SaveChanges();

            return userReturnModel;
            
        }

        public Task<BusinessUserModel> GetByMail(string mail)
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessUserModel> Update(BusinessUserModel model)
        {
            var userToBeUpdated = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(model.Email));
            if (userToBeUpdated == null)
                throw new KeyNotFoundException("User with this email doesn't exists!");

            var mappedUser = _mapper.Map<DataUserModel>(model);
            mappedUser.Id = userToBeUpdated.Id;
            await _unitOfWork.Users.UpdateAsync(mappedUser, mappedUser.Id);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<BusinessUserModel>(mappedUser);

        }
    }
}
