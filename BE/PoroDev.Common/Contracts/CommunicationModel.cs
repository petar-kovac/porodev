using PoroDev.Common.Exceptions.Contract;

namespace PoroDev.Common.Contracts
{
    public class CommunicationModel<T> where T : class, new()
    {
        public T? Entity { get; set; }

        public string? ExceptionName { get; set; }

        public string? HumanReadableMessage { get; set; }

        public CommunicationModel()
        {
        }

        public CommunicationModel(ICustomException exception)
        {
            Entity = null;
            ExceptionName = exception.GetType().Name;
            HumanReadableMessage = exception.HumanReadableErrorMessage;
        }
    }
}