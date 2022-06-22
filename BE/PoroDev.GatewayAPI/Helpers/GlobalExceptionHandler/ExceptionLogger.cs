namespace PoroDev.GatewayAPI.Helpers.GlobalExceptionHandler
{
    public class ExceptionLogger
    {
        private static string filePath = Directory.GetCurrentDirectory() + "\\logs\\logs.csv";

        public static void WriteNewLog(string hrmessage, Exception e)
        {
            if (File.Exists(filePath))
            {
                WriteLog(hrmessage, e);
            }
            else
            {
                CreateDir();
                WriteHeaders();
                WriteLog(hrmessage, e);
            }
        }

        private static void WriteLog(string hrmessage, Exception e)
        {
            string newLog = DateTime.Now.ToString() + ',' + hrmessage + ',' + e.GetType() + ',' + e.Message + ',' +e.StackTrace + "\n";
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(newLog);
            }
        }

        private static void WriteHeaders()
        {
            string headers = "Time,HumanReadableMessage,Exception,ExceptionMessage,StackTrace";
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(headers);
            }
        }

        private static void CreateDir()
        {
            string dirPath = Directory.GetCurrentDirectory() + "\\logs";
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
    }
}