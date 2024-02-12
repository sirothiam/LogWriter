using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Threading;
namespace LoggerLibrary
{
    public class LoggerService
    {

        private readonly ILogger<LoggerService> _logger;

        // constructor
        public LoggerService(ILogger<LoggerService> logger = null)
        {
            _logger = logger;
        }

        /// <summary>
        /// Function To Log Mesaage On Console.
        /// </summary>
        /// <param name="message"></param>
        public void LogActivity(string message)
        {

            string strAppNamespace = string.Empty;
            string strErrorStatus = string.Empty;
            string strMessage = string.Empty;
            string strErrorDescription=string.Empty;
            int iPriority = 0;

            if (!string.IsNullOrEmpty(message))
            {
                string[] array = message.Split('|');
                strAppNamespace = array[0].ToString();
                strErrorStatus = array[1].ToString();
                strMessage = array[2].ToString();

                
                //Level.FATAL.ToString()
                //Level.ERROR.ToString()
                //Level.WARN.ToString()
                //Level.INFO.ToString()
                //Level.DEBUG.ToString()

                if (strErrorStatus.ToUpper() == ConfigHelper.Level_Fatal)
                {
                    iPriority = 1;
                    strErrorDescription = LogDescription(strAppNamespace, strErrorStatus, strMessage, iPriority);
                    _logger?.LogCritical(strErrorDescription);

                }
                else if (strErrorStatus.ToUpper() == ConfigHelper.Level_Error)
                {
                    iPriority = 2;
                    strErrorDescription = LogDescription(strAppNamespace, strErrorStatus, strMessage, iPriority);
                    _logger?.LogError(strErrorDescription);
                }
                else if (strErrorStatus.ToUpper() == ConfigHelper.Level_Warn)
                {
                    iPriority = 3;
                    strErrorDescription = LogDescription(strAppNamespace, strErrorStatus, strMessage, iPriority);
                    _logger?.LogWarning(strErrorDescription);
                }
                else if (strErrorStatus.ToUpper() == ConfigHelper.Level_Info)
                {
                    iPriority = 4;
                    strErrorDescription = LogDescription(strAppNamespace, strErrorStatus, strMessage, iPriority);
                    _logger?.LogInformation(strErrorDescription);
                }
                else if (strErrorStatus.ToUpper() == ConfigHelper.Level_Debug)
                {
                    iPriority = 5;
                    strErrorDescription = LogDescription(strAppNamespace, strErrorStatus, strMessage, iPriority);
                    _logger?.LogDebug(strErrorDescription);
                }

            }

        }
        /// <summary>
        /// Function To Generate LogDescription Details.
        /// </summary>
        /// <param name="strAppNamespace"></param>
        /// <param name="strErrorStatus"></param>
        /// <param name="strMessage"></param>
        /// <param name="iPriority"></param>
        /// <returns></returns>
        private string LogDescription(string strAppNamespace, string strErrorStatus, string strMessage,int iPriority) 
        {
            string Msg = "";
            Msg = "\n---------------------------- Start -----------------------------------\n" + Environment.NewLine;
            Msg += "Time : " + DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + Environment.NewLine;
            Msg += "Name Space : " + strAppNamespace + Environment.NewLine;
            Msg += "Level : " + strErrorStatus + Environment.NewLine;
            Msg += "Message : " + strMessage + Environment.NewLine;
            Msg += "Priority : " + iPriority.ToString() + Environment.NewLine;
            Msg += "\n---------------------------- End -----------------------------------\n" + Environment.NewLine;
            return Msg;

        }

    }
}