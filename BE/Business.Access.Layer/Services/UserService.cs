﻿using AutoMapper;
using Business.Access.Layer.Exceptions;
using Business.Access.Layer.Helpers.GlobalExceptionHandler;
using Business.Access.Layer.Models.UserModels;
using Business.Access.Layer.Services.Contracts;
using Data.Access.Layer.Models;
using Data.Access.Layer.Repositories.Contracts;
using System.Security.Cryptography;

namespace Business.Access.Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private const int MIN_PASSWORD_LENGTH = 8;
        private readonly string EMAIL_DOMAIN = "@boing.rs";

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserLoginResponseModel> Login(UserLoginRequestModel loginModel)
        {
            var dataUserModel = await GetUserByMail(loginModel.Email);
            VerifyPasswordHash(loginModel.Password, dataUserModel.Password, dataUserModel.Salt);

            UserLoginResponseModel response = _mapper.Map<UserLoginResponseModel>(dataUserModel);
            return response;
        }

        private void VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (!computeHash.SequenceEqual(passwordHash))
                    throw new FailedToLogInException("Email or password is not valid.");
            }
        }

        private void CheckPassword(string password)
        {
            if (password.Length < MIN_PASSWORD_LENGTH)
                throw new PasswordFormatException($"Password must be at least {MIN_PASSWORD_LENGTH} characters!");
            if (!password.Any(char.IsUpper))
                throw new PasswordFormatException("Password must contain at least 1 uppercase letter!");
            if (!password.Any(char.IsLower))
                throw new PasswordFormatException("Password must contain at least 1 lowercase letter!");
            if (!password.Any(char.IsDigit))
                throw new PasswordFormatException("Password must contain at least 1 number!");

            CheckPasswordSpecialCharacter(password);
        }

        private async Task CheckEmail(string email)
        {
            if (email.Contains(EMAIL_DOMAIN) == false)
                throw new EmailFormatException($"Only emails with {EMAIL_DOMAIN} are accepted!");
            if (await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(email)) != null)
                throw new EmailFormatException("User with that email already exists!");
        }

        private void CheckPasswordSpecialCharacter(string password)
        {
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            bool flag = false;

            foreach (char ch in specialChArray)
            {
                if (password.Contains(ch))
                    flag = true;
            }

            if (!flag)
                throw new PasswordFormatException("Password must contain at least 1 special character!");
        }

        private void GetHashAndSalt(string password, out byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerModel, Enums.UserRole role)
        {
           await CheckEmail(registerModel.Email);
            CheckPassword(registerModel.Password);

            GetHashAndSalt(registerModel.Password, out byte[] salt, out byte[] hash);

            DataUserModel userToAdd = new()
            {
                Id = Guid.NewGuid(),
                Name = registerModel.Name,
                Lastname = registerModel.Lastname,
                Email = registerModel.Email,
                Password = hash,
                Salt = salt,
                Department = Enums.UserDepartment.notDefined,
                Role = role,
                Position = registerModel.Position,
                AvatarUrl = registerModel.AvatarUrl,
                DateCreated = DateTime.Now
            };

            var newUser = await _unitOfWork.Users.CreateAsync(userToAdd);
            await _unitOfWork.SaveChanges();

            var returnModel = _mapper.Map<UserRegisterResponseModel>(newUser);

            return returnModel;
        }

        public async Task<Guid> CreateUser(UserCreateRequestModel model)
        {
            var exists = await _unitOfWork.Users.FindSingleAsync(c => c.Email.Equals(model.Email));

            if (exists != null) 
                throw new AppException("User already exists");

            var userToCreate = _mapper.Map<DataUserModel>(model);
            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);
            userToCreate.Password = hash;
            userToCreate.Salt = salt;
            userToCreate.Id = Guid.NewGuid();
            userToCreate.DateCreated = DateTime.Now;

            var created = await _unitOfWork.Users.CreateAsync(userToCreate);

            await _unitOfWork.SaveChanges();

            if (created != null)
                return created.Id;
            return Guid.Empty;
        }

        public async Task<UserCreateRequestModel> DeleteUser(string mail)
        {
            if (mail == null)
                throw new AppException("Mail is null.");

            var userForDeletion = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(mail));

            if (userForDeletion == null)
                throw new UserNotFoundException("User with that email doesn't exist.");

            var userReturnModel = _mapper.Map<UserCreateRequestModel>(userForDeletion);

            _unitOfWork.Users.Delete(userForDeletion);

            await _unitOfWork.SaveChanges();

            return userReturnModel;
        }

        public async Task<DataUserModel> GetUserByMail(string email)
        {
            if (email == null)
            {
                throw new KeyNotFoundException("User email has NULL value.");
            }

            var userForRead = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(email));

            if (userForRead == null)
            {
                throw new KeyNotFoundException("Email or password is not valid.");
            }

            return userForRead;
        }

        public async Task<UserCreateRequestModel> UpdateUser(UserCreateRequestModel model)
        {
            if (model.Email == null)
                throw new KeyNotFoundException("User email has NULL value.");

            var userToBeUpdated = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(model.Email));
            if (userToBeUpdated == null)
                throw new KeyNotFoundException("User with this email doesn't exists!");

            var mappedUser = _mapper.Map<DataUserModel>(model);
            mappedUser.Id = userToBeUpdated.Id;
            mappedUser.DateCreated = userToBeUpdated.DateCreated;

            GetHashAndSalt(model.PasswordUnhashed, out byte[] salt, out byte[] hash);

            mappedUser.Password = hash;
            mappedUser.Salt = salt;

            await _unitOfWork.Users.UpdateAsync(mappedUser, mappedUser.Id);
            await _unitOfWork.SaveChanges();
            return _mapper.Map<UserCreateRequestModel>(mappedUser);
        }
    }
}