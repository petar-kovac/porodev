﻿namespace PoroDev.Runtime.Services
{
    public abstract class RuntimePathsAbstract
    {
        public string ZippedFilePath { get; set; }

        public string ZippedFileName { get; set; }

        public string ProjectPath { get; set; }

        public string RuntimeFolderPath { get; set; }

        protected virtual Exception SetFolderPath(string runtimeFolderPath)
        {
            RuntimeFolderPath = runtimeFolderPath;

            try
            {
                ZippedFilePath = Directory.GetFiles(RuntimeFolderPath, "*.zip", SearchOption.AllDirectories).FirstOrDefault();

                ZippedFileName = Path.GetFileNameWithoutExtension(ZippedFilePath);

                ProjectPath = Path.Combine(Path.Combine(RuntimeFolderPath, ZippedFileName), ZippedFileName);
            }
            catch (DirectoryNotFoundException ex)
            {
                Directory.CreateDirectory(RuntimeFolderPath);
                SetFolderPath(runtimeFolderPath);
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }
    }
}