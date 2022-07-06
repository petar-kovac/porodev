namespace PoroDev.Runtime.Constants
{
    public static class Consts
    {
        public static readonly string RUNTIME_FOLDER_ROUTE = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "Runtime");

        public static readonly string DELETE_DOCKER_FILE_EXCEPTION = "Exception happened while deleting docker image, check process path?";

        public static readonly string GET_FILE_OUTPUT_EXCEPTION = "Exception happened while getting file output.";
    }
}