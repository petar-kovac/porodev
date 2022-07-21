using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadAllUsers;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;
using static PoroDev.Common.Extensions.CreateResponseExtension;
namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class ReadAllUsersConsumer : BaseDbConsumer, IConsumer<ReadAllUsersRequestServiceToDatabase>
    {

        public ReadAllUsersConsumer(IUnitOfWork unitofWork, IMapper mapper, IFileRepository filerRepository) : base(unitofWork, mapper, filerRepository)
        {

        }
        public async Task Consume(ConsumeContext<ReadAllUsersRequestServiceToDatabase> context)
        {
            var AllUsers = await _unitOfWork.Users.FindAllAsync(user => user.Email.Contains(""));
            var returnModel = CreateResponseModel<CommunicationModel<List<DataUserModel>>, List<DataUserModel>>(AllUsers.ToList<DataUserModel>());
            await context.RespondAsync(returnModel);
        }
    }
}
