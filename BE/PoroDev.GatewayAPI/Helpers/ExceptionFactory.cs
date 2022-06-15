using PoroDev.Common.Exceptions;

namespace PoroDev.GatewayAPI.Helpers
{
    public static class ExceptionFactory
    {
        public static void ThrowException(string nameOfException, string errorMessage)
        {
            throw nameOfException switch
            {
                nameof(DatabaseException) => new DatabaseException(errorMessage),
                nameof(EmailFormatException) => new EmailFormatException(errorMessage),
                nameof(FailedToLogInException) => new FailedToLogInException(errorMessage),
                nameof(PasswordFormatException) => new PasswordFormatException(errorMessage),
                nameof(PositionFormatException) => new PositionFormatException(errorMessage),
                nameof(UserNotFoundException) => new UserNotFoundException(errorMessage),
                nameof(UserExistsException) => new UserExistsException(errorMessage),
                nameof(InvalidCredentialsExceptions) => new InvalidCredentialsExceptions(errorMessage),
                nameof(FullNameFormatException) => new FullNameFormatException(errorMessage),
                _ => new Exception("Exception not found!"),
            };
        }
    }
}