using MassTransit;
using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Create;
using PoroDev.UserManagementService.Services.Contracts;

namespace PoroDev.UserManagementService.Consumers
{
    public class UserCreateConsumer : IConsumer<IUserCreateRequestGatewayToService>
    {
        private readonly IUserService _userService;

        public UserCreateConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<IUserCreateRequestGatewayToService> context)
        {
            var model = context.Message;

            UserCreateRequestGatewayToService temp = new()
            {
                AvatarUrl = model.AvatarUrl,
                Department = model.Department,
                Email = model.Email,
                Lastname = model.Lastname,
                Name = model.Name,
                PasswordUnhashed = model.PasswordUnhashed,
                Position = model.Position,
                Role = model.Role,
            };

            var modelToReturn = await _userService.CreateUser(temp);
            modelToReturn.ErrorName = null;
            modelToReturn.ErrorMessage = null;

            await context.RespondAsync<IUserCreateResponseServiceToGateway>(modelToReturn);
        }
    }
}
