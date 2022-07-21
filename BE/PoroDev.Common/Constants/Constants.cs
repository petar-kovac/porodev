namespace PoroDev.Common.Constants
{
    public static class Constants
    {
        public const string SecretKey = "this is a custom Secret key for authentification";

        public static readonly ulong MAX_DOWNLOAD_TOTAL = 5000000000;
        public static readonly ulong MAX_UPLOAD_TOTAL = 5000000000;
        public static readonly ushort MAX_RUNTIME_TOTAL = 50;

        public static readonly double PRICE_PER_MB = 0.002;
        public static readonly double PRICE_RUNTIME = 0.1;
    }
}