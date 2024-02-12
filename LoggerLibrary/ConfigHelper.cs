using System.Configuration;

namespace LoggerLibrary
{
    public class ConfigHelper
    {
       
        static string? _lblFatal = ConfigurationManager.AppSettings["FATAL"]?.ToString();
        static string? _lblError = ConfigurationManager.AppSettings["ERROR"]?.ToString();
        static string? _lblWarn = ConfigurationManager.AppSettings["WARN"]?.ToString();
        static string? _lblInfo = ConfigurationManager.AppSettings["INFO"]?.ToString();
        static string? _lblDebug = ConfigurationManager.AppSettings["DEBUG"]?.ToString();

        public static string? Level_Fatal
        {
            get
            {
                return _lblFatal;
            }
        }

        public static string? Level_Error
        {
            get
            {
                return _lblError;
            }
        }
        public static string? Level_Warn
        {
            get
            {
                return _lblWarn;
            }
        }

        public static string? Level_Info
        {
            get
            {
                return _lblInfo;
            }
        }

        public static string? Level_Debug
        {
            get
            {
                return _lblDebug;
            }
        }

    }
}