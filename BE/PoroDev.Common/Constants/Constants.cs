namespace PoroDev.Common.Constants
{
    public static class Constants
    {
        public const string SecretKey = "this is a custom Secret key for authentification";
        public const string InternalDatabaseError = "Unexpected error occurred within database.";

        public static readonly ulong MAX_DOWNLOAD_TOTAL = 5000000000;
        public static readonly ulong MAX_UPLOAD_TOTAL = 5000000000;
        public static readonly ushort MAX_RUNTIME_TOTAL = 50;

    }
}