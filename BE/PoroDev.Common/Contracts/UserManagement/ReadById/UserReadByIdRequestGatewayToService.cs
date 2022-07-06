namespace PoroDev.Common.Contracts.UserManagement.ReadById
{
    public class UserReadByIdRequestGatewayToService
    {
        public Guid Id { get; set; }

        public UserReadByIdRequestGatewayToService()
        {
        }

        public UserReadByIdRequestGatewayToService(Guid id)
        {
            Id = id;
        }
    }
}