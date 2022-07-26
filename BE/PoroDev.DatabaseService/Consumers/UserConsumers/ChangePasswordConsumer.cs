using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ChangePassword;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using System.Security.Cryptography;
using static PoroDev.DatabaseService.Constants.Constants;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class ChangePasswordConsumer : BaseDbConsumer, IConsumer<ChangePasswordRequestServiceToDatabase>
    {
        public ChangePasswordConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<ChangePasswordRequestServiceToDatabase> context)
        {
            var userToUpdate = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email));
            if(userToUpdate == null)
            {
                await context.RespondAsync(new CommunicationModel<DataUserModel>(new UserNotFoundException(UserNotFoundExceptionMessage)));
                return;
            }
            GetHashAndSalt(context.Message.OldPassword, out byte[] oldSalt, out byte[] oldHash);
            if(!VerifyPasswordHash(context.Message.OldPassword,userToUpdate.Entity.Password, userToUpdate.Entity.Salt))
            {
                await context.RespondAsync(new CommunicationModel<DataUserModel>(new WrongOldPasswordException(WrongOldPasswordExceptionMessage)));
                return;
            }
            GetHashAndSalt(context.Message.NewPassword, out byte[] newSalt, out byte[] newHash);
            userToUpdate.Entity.Password = newHash;
            userToUpdate.Entity.Salt = newSalt;
            await _unitOfWork.SaveChanges();

            await context.RespondAsync<CommunicationModel<DataUserModel>>(userToUpdate);

        }

        private void GetHashAndSalt(string password, out byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (!computeHash.SequenceEqual(passwordHash))
                    return false;
            }
            return true;
        }
    }
}
