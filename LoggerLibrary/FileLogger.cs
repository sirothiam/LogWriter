using System.Configuration;
using System.Globalization;

namespace LoggerLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public static class FileLogger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        public static void LogActivity(string message, string status = "")
        {

            string strAppNamespace=string.Empty ;
            string strErrorStatus = string.Empty;
            string strMessage = string.Empty;
            int iPriority =0;
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    string[] array = message.Split('|');
                    strAppNamespace = array[0].ToString();
                    strErrorStatus = array[1].ToString();
                    strMessage = array[2].ToString();
                    if (strErrorStatus.ToUpper() == Level.FATAL.ToString())
                    {
                        iPriority = 1;

                    }
                    else if (strErrorStatus.ToUpper() == Level.ERROR.ToString())
                    {
                        iPriority = 2;
                    }
                    else if (strErrorStatus.ToUpper() == Level.WARN.ToString())
                    {
                        iPriority = 3;
                    }
                    else if (strErrorStatus.ToUpper() == Level.INFO.ToString())
                    {
                        iPriority = 4;
                    }
                    else if (strErrorStatus.ToUpper() == Level.DEBUG.ToString())
                    {
                        iPriority = 5;
                    }
                }

                    string? LogFolderPath = ConfigurationManager.AppSettings["LogFile"];

                    if (string.IsNullOrEmpty(LogFolderPath))
                    {
                        LogFolderPath = Environment.CurrentDirectory;

                    }
                    if (!Directory.Exists(LogFolderPath))
                    {

                        DirectoryInfo di = Directory.CreateDirectory(LogFolderPath);
                    }
                

                    string date = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
                    string dateFolder = Path.Combine(LogFolderPath, date);
                    LogFolderPath = dateFolder + ".txt";
                    string Msg = "";
                    Msg = "\n---------------------------- Start -----------------------------------\n" + Environment.NewLine;
                    Msg += "Time : " + DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + Environment.NewLine;
                    Msg += "Name Space : " + strAppNamespace + Environment.NewLine;
                    Msg += "Level : " + strErrorStatus + Environment.NewLine;
                    Msg += "Message : " + strMessage + Environment.NewLine;
                    Msg += "Priority : " + iPriority.ToString() + Environment.NewLine;
                    Msg += "\n---------------------------- End -----------------------------------\n" + Environment.NewLine;
                    Log(LogFolderPath, Msg);
            }
                catch (Exception ex)
                {
                    throw  ;
                }
            
        }

        private static void Log(string sFilePath, string sErrMsg)
        {
            StreamWriter sw;
            try
            {
                sw = File.AppendText(sFilePath);
                sw.WriteLine(sErrMsg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ;
            }
        }
    }
}