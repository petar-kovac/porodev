using AutoMapper;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.LoginUser;
using PoroDev.Common.Exceptions;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserModels.LoginUser;
using PoroDev.DatabaseService.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static PoroDev.DatabaseService.Constants.Constants;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserLoginConsumer : BaseDbConsumer, IConsumer<UserLoginRequestServiceToDatabase>
    {
        public UserLoginConsumer(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task Consume(ConsumeContext<UserLoginRequestServiceToDatabase> context)
        {
            CommunicationModel<LoginUserModel> returnModel;
            var userToLogIn = await _unitOfWork.Users.FindAsync(user => user.Email.Equals(context.Message.Email));

            returnModel = _mapper.Map<CommunicationModel<LoginUserModel>>(userToLogIn);
            if (userToLogIn.ExceptionName != null)
            {
                CheckAndChangeErrorType(returnModel);
                await context.RespondAsync(returnModel);
                return;
            }

            if (!CheckPasswordAndProcessError(returnModel, userToLogIn.Entity, context.Message))
            {
                await context.RespondAsync(returnModel);
                return;
            }

            if (!CheckIfUserIsVerified(returnModel, userToLogIn.Entity))
            {
                await context.RespondAsync(returnModel);
                return;
            }

            returnModel.Entity.Jwt = CreateToken(userToLogIn.Entity);

            await context.RespondAsync(returnModel);
        }

        public string CreateToken(DataUserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Common.Constants.Constants.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool CheckIfUserIsVerified(CommunicationModel<LoginUserModel> returnModel, DataUserModel loginModel)
        {
            if (loginModel.VerifiedAt == null)
            {
                returnModel.Entity = null;
                returnModel.HumanReadableMessage = UserNotVerifiedExceptionMessage;
                returnModel.ExceptionName = nameof(UserNotVerifiedException);
                return false;
            }
            return true;
        }

        private void CheckAndChangeErrorType(CommunicationModel<LoginUserModel> model)
        {
            if (model.ExceptionName == nameof(UserNotFoundException))
            {
                ProcessInvalidCredentialsException(model);
            }
        }

        private bool CheckPasswordAndProcessError(CommunicationModel<LoginUserModel> returnModel, DataUserModel dataModel, UserLoginRequestServiceToDatabase loginModel)
        {
            var passwordsMatches = VerifyPasswordHash(loginModel.Password, dataModel.Password, dataModel.Salt);
            if (passwordsMatches == false)
            {
                ProcessInvalidCredentialsException(returnModel);
                return false;
            }
            return true;
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

        private void ProcessInvalidCredentialsException(CommunicationModel<LoginUserModel> returnModel)
        {
            returnModel.ExceptionName = nameof(InvalidCredentialsExceptions);
            returnModel.HumanReadableMessage = InvalidCredentials;
        }
    }
}