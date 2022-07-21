using AutoMapper;
using MassTransit;
using PoroDev.Common.Contracts;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.DatabaseService.Repositories.Contracts;

namespace PoroDev.DatabaseService.Consumers.RuntimeDataConsumers
{
    public class RuntimeDataCreateConsumer : BaseDbConsumer, IConsumer<RuntimeData>
    {
        public RuntimeDataCreateConsumer(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileRepository) : base(unitOfWork, mapper, fileRepository)
        {
        }

        public async Task Consume(ConsumeContext<RuntimeData> context)
        {
            var dbResponse = await _unitOfWork.RuntimeData.CreateAsync(context.Message);

            var userModel = await _unitOfWork.Users.GetByIdAsync(context.Message.UserId);

            userModel.RuntimeTotal += 1;

            await _unitOfWork.SaveChanges();

            var responseModel = _mapper.Map<CommunicationModel<RuntimeData>>(dbResponse);

            await context.RespondAsync(responseModel);
        }
    }
}