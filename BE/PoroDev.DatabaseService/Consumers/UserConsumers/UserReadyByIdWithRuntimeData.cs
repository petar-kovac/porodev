using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.UserConsumers
{
    public class UserReadyByIdWithRuntimeData : BaseDbConsumer, IConsumer<UserReadByIdWithRuntimeRequestServiceToDataBase>
    {
        public UserReadyByIdWithRuntimeData(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<UserReadByIdWithRuntimeRequestServiceToDataBase> context)
        {
            var fetchUser = await _unitOfWork.Users.GetUserByIdWithRuntimeDatas(context.Message.Id);

            var returnUser = _mapper.Map<CommunicationModel<DataUserModel>>(fetchUser);

            await context.RespondAsync(returnUser);
        }
    }
}