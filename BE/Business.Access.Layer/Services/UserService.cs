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

        public async Task<Guid?> Create(BusinessUserModel model)
        {
            var existis = await _unitOfWork.Users.FindSingleAsync(c => c.Email.Equals(model.Email)); ;
            if (existis != null) throw new AppException("User already exists");

            var userToCreate = _mapper.Map<DataUserModel>(model);
            userToCreate.Id = Guid.NewGuid();
            userToCreate.DateCreated = DateTime.Now;

            var created = await _unitOfWork.Users.CreateAsync(userToCreate);

            await _unitOfWork.SaveChanges();
            return created.Id;
            
        }

        public async Task<BusinessUserModel> Delete(string mail)
        {
            if (mail == null)
                throw new AppException("Mail is null.");

            var userForDeletion = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(mail));

            if (userForDeletion == null)
                throw new UserNotFoundException("User with that email doesn't exist.");

            var userReturnModel = _mapper.Map<BusinessUserModel>(userForDeletion);

            _unitOfWork.Users.Delete(userForDeletion);

            await _unitOfWork.SaveChanges();

            return userReturnModel;
            
        }

        public async Task<BusinessUserModel> GetByMail(string email)
        {
            if (email == null)
            {
                throw new KeyNotFoundException("User email has NULL value.");
            }

            var userForRead = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(email));
            

            if(userForRead == null)
            {
                throw new KeyNotFoundException("User with that email doesn't exist!");
            }

            var userReturnModel = _mapper.Map<BusinessUserModel>(userForRead);

            return userReturnModel;
          
        }

        public async Task<BusinessUserModel> Update(BusinessUserModel model)
        {
            if (model.Email == null)
                throw new KeyNotFoundException("User email has NULL value.");

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
