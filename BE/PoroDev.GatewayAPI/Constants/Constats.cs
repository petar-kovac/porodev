namespace PoroDev.GatewayAPI.Constants
{
    public static class Constats
    {
        public const string EmptyEmail = "Email can't be empty.";

        public const string InvalidEmail = "Invalid Email format";

        public const string NullRequest = "Api request is invalid or null.";

        public const string EmptyPassword = "Password can't be empty.";

        public static readonly string CANNOT_VALIDATE_JWT = "Could not validate JWT token";

        public const string InvalidToken = "Invalid verification token.";

        public const string FileUploadExceptionMessage = "File or Id is invalid or null";

        public const string InvalidHourValueExceptionMessage = "Invalid hour value, please enter value between 0 and 23";

        public const string InvalidDayValueExceptionMessage = "Invalid day value, please enter value between 1 and 31";
    }
}