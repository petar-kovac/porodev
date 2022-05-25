using AutoMapper;
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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Register(UserRegisterModel registerModel)
        {
            if (registerModel.Email.Contains("@boing.rs") == false)
                throw new AppException("Email format!");

            if (registerModel.Password.Length < 8)
                throw new AppException("Password must be at least 8 characters!");
            if (!registerModel.Password.Any(char.IsUpper))
                throw new AppException("Password must contain at least 1 uppercase letter!");
            if (!registerModel.Password.Any(char.IsLower))
                throw new AppException("Password must contain at least 1 lowercase letter!");
            if (!registerModel.Password.Any(char.IsDigit))
                throw new AppException("Password must contain at least 1 number!");

            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            bool flag = false;
            foreach (char ch in specialChArray)
            {
                if (registerModel.Password.Contains(ch))
                    flag = true;
            }

            if (!flag)
                throw new AppException("Password must contain at least 1 special character!");

            byte[] hash;
            byte[] salt;

            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerModel.Password));
            }

            BusinessUserModel userToAdd = new()
            {
                Name = registerModel.Name,
                Lastname = registerModel.Lastname,
                Email = registerModel.Email,
                AvatarUrl = registerModel.AvatarUrl,
                Password = hash,
                Salt = salt,
                Department = Enums.UserDepartment.notDefined,
                Position = registerModel.Position
            };

            await CreateUser(userToAdd);
        }

        public async Task<Guid?> CreateUser(BusinessUserModel model)
        {
            var exists = await _unitOfWork.Users.FindSingleAsync(c => c.Email.Equals(model.Email)); ;
            if (exists != null) throw new AppException("User already exists");

            var userToCreate = _mapper.Map<DataUserModel>(model);
            userToCreate.Role = 0;
            userToCreate.Id = Guid.NewGuid();
            userToCreate.DateCreated = DateTime.Now;

            var created = await _unitOfWork.Users.CreateAsync(userToCreate);

            await _unitOfWork.SaveChanges();
            return created.Id;
        }

        public async Task<BusinessUserModel> DeleteUser(string mail)
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

        public async Task<BusinessUserModel> GetUserByMail(string email)
        {
            if (email == null)
            {
                throw new KeyNotFoundException("User email has NULL value.");
            }

            var userForRead = await _unitOfWork.Users.FindSingleAsync(user => user.Email.Equals(email));

            if (userForRead == null)
            {
                throw new KeyNotFoundException("User with that email doesn't exist!");
            }

            var userReturnModel = _mapper.Map<BusinessUserModel>(userForRead);

            return userReturnModel;
        }

        public async Task<BusinessUserModel> UpdateUser(BusinessUserModel model)
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