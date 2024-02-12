using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerLibrary
{
    public enum LogTarget
    {
        File, 
        Database, 
        EventLog
    }

    public enum Level
    {
        FATAL,
        ERROR,
        WARN,
        INFO,
        DEBUG
    }

}
