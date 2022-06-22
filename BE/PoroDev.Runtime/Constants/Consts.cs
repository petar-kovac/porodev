using System;

namespace PoroDev.Runtime.Constants
{
    public static class Consts
    {
        public static readonly string RUNTIME_FOLDER_ROUTE = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "Runtime");
        public static readonly string ZIPPED_FILE_ROUTE = Directory.GetFiles(RUNTIME_FOLDER_ROUTE, "*.zip", SearchOption.AllDirectories).FirstOrDefault();
        public static readonly string ZIPPED_FILE_NAME = Path.GetFileNameWithoutExtension(ZIPPED_FILE_ROUTE);
        public static readonly string PROJECT_PATH = Path.Combine(Path.Combine(RUNTIME_FOLDER_ROUTE, ZIPPED_FILE_NAME), ZIPPED_FILE_NAME);

    }
}
