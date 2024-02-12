using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary
{
    public static class LogHelper
    {
        
        public static void Log(LogTarget target, string message)
        {
            
            
            switch (target)
            {
                case LogTarget.File:
                    FileLogger.LogActivity(message);
                    break;
                case LogTarget.Database:
                    DBLogger.LogActivity(message);
                    break;
                default:
                    return;
            }
        }

    }
}
