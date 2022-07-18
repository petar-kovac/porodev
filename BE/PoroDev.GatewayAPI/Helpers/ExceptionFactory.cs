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
                nameof(PoroDev.Common.Exceptions.FileNotFoundException) => new PoroDev.Common.Exceptions.FileNotFoundException(errorMessage),
                nameof(PasswordFormatException) => new PasswordFormatException(errorMessage),
                nameof(PositionFormatException) => new PositionFormatException(errorMessage),
                nameof(UserNotFoundException) => new UserNotFoundException(errorMessage),
                nameof(UserExistsException) => new UserExistsException(errorMessage),
                nameof(InvalidCredentialsExceptions) => new InvalidCredentialsExceptions(errorMessage),
                nameof(FullNameFormatException) => new FullNameFormatException(errorMessage),
                nameof(NoHeaderWithJwtException) => new NoHeaderWithJwtException(errorMessage),
                nameof(JWTValidationException) => new JWTValidationException(errorMessage),
                nameof(IdFormatException) => new IdFormatException(errorMessage),
                nameof(RequestNullException) => new RequestNullException(errorMessage),
                nameof(ZippedFileException) => new ZippedFileException(errorMessage),
                nameof(DockerRuntimeException) => new DockerRuntimeException(errorMessage),
                nameof(FileUploadExistException) => new FileUploadExistException(errorMessage),
                nameof(FileUploadFormatException) => new FileUploadFormatException(errorMessage),
                nameof(FileUploadException) => new FileUploadException(errorMessage),
                nameof(UserPermissionException) => new UserPermissionException(errorMessage),

                _ => new Exception("Exception not found!"),
            };
        }
    }
}