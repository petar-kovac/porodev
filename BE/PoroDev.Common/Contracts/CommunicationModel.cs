namespace PoroDev.Common.Contracts
{
    public abstract class CommunicationModel<T> where T : class, new()
    {
        public T? Entity { get; set; }

        public string? ExceptionName { get; set; }

        public string? HumanReadableMessage { get; set; }
    }
}