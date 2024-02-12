using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace LoggerLibrary
    {
    
    /// <summary>
    /// Class To Perform Message Logging.
    /// </summary>
    public static class DBLogger
    {

        #region"Function To Log Messages In Database"

        /// <summary>
        /// Functon Log Logging Information to Database.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        public static void LogActivity(string message, string status = "")
        {
            string strAppNamespace = string.Empty;
            string strErrorStatus = string.Empty;
            string strMessage = string.Empty;
            int iPriority = 0;
            SqlConnection? connection= null;
            try
            {

                string? strConnectionString = ConfigurationManager.AppSettings["ConnectionString"]?.ToString();
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
                connection = new SqlConnection(strConnectionString);
                string strInsertQuery = "INSERT INTO [dbo].[ApplicationLogTable](Time,Level,Priority,ApplicationNameSpace,Message)VALUES(@Time,@Level,@Priority,@AppNameSpace,@Message)";
                SqlCommand cmd = new SqlCommand(strInsertQuery, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Time", SqlDbType.DateTime)).Value = DateTime.Now;
                cmd.Parameters.Add(new SqlParameter("@Level", SqlDbType.VarChar)).Value = strErrorStatus;
                cmd.Parameters.Add(new SqlParameter("@Priority", SqlDbType.Int)).Value = iPriority;
                cmd.Parameters.Add(new SqlParameter("@AppNameSpace", SqlDbType.VarChar)).Value = strAppNamespace;
                cmd.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar)).Value = strMessage;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ;
            }
            finally
            {
                connection?.Close();
            }

        }

        #endregion
    }
}