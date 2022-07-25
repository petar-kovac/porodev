using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.Query;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers.Query
{
    public class QueryAllConsumer : BaseDbConsumer, IConsumer<QueryAllUsersRequestServiceToDatabase>
    {
        public QueryAllConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<QueryAllUsersRequestServiceToDatabase> context)
        {
            var result = (await _unitOfWork.Users.FindAllAsync(user => (int)user.Role == 1)).ToList();

            await context.RespondAsync(new CommunicationModel<List<DataUserModel>>(result));
        }
    }
}