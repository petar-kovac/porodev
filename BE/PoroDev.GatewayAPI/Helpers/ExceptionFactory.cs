using PoroDev.Common.Exceptions;

namespace PoroDev.GatewayAPI.Helpers
{
    public static class ExceptionFactory
    {
        public static void ThrowException(string nameOfException, string errorMessage)
        {
            switch (nameOfException)
            {
                case nameof(DatabaseException):
                    throw new DatabaseException(errorMessage);

                case nameof(EmailFormatException):
                    throw new EmailFormatException(errorMessage);

                case nameof(FailedToLogInException):
                    throw new FailedToLogInException(errorMessage);

                case nameof(PasswordFormatException):
                    throw new PasswordFormatException(errorMessage);

                case nameof(PositionFormatException):
                    throw new PositionFormatException(errorMessage);

                case nameof(UserNotFoundException):
                    throw new UserNotFoundException(errorMessage);

                default:
                    throw new Exception("Exception not found!");
            }
        }
    }
}
