namespace PoroDev.DatabaseService.Constants
{
    public static class Constants
    {
        public const string UserNotFoundExceptionMessage = "There is no user that match the required filter.";
        public const string InternalDatabaseError = "Unexpected error occurred within database.";
        public const string InvalidCredentials = "Username and password does not match.";
        public const string SecretKey = "this is a custom Secret key for authentification";
        public const string UserNotVerifiedExceptionMessage = "You have to validate your email adress to be able to use your account.";
        public const string InvalidTokenExceptionMessage = "Verification token is not valid.";
    }
}