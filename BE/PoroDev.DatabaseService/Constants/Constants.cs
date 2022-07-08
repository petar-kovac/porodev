

namespace PoroDev.DatabaseService.Constants
{
    public static class Constants
    {
        public const string UserNotFoundExceptionMessage = "There is no user that match the required filter.";
        public const string InternalDatabaseError = "Unexpected error occurred within database.";
        public const string InvalidCredentials = "Username and password does not match.";
        public const string SecretKey = "this is a custom Secret key for authentification";

        public static readonly byte[] secretKey = Convert.FromBase64String("dLz8jpZT5fpypiMiy6ZoYyInNyelyVKTWh0O5YV/DWE=");
        public static readonly byte[] secretIv = Convert.FromBase64String("Jcb8G24OjbJ1NwHF47GR9A==");

    }
}