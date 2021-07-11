using System;
using System.Threading.Tasks;

namespace RAK.Fwk.Common.Log
{
    /// <summary>
    /// Interfaz de logs
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Agrega un log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logType"></param>
        /// <param name="callerMethod"></param>
        /// <param name="callerName"></param>
        /// <param name="sourceLineNumber"></param>
        /// <returns></returns>
        Task AddLog(string message,
                        LogType logType = LogType.Info,
                        [System.Runtime.CompilerServices.CallerMemberName] string callerMethod = "",
                        [System.Runtime.CompilerServices.CallerFilePath] string callerName = "",
                        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0,
                        LogLevel logLvl = LogLevel.Minimum
                    );

        /// <summary>
        /// Agrega un log de un error
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="extraInfo"></param>
        /// <param name="callerMethod"></param>
        /// <param name="callerName"></param>
        /// <returns></returns>
        Task AddErrorLog(Exception ex,
                            string extraInfo = "",
                            [System.Runtime.CompilerServices.CallerMemberName] string callerMethod = "",
                            [System.Runtime.CompilerServices.CallerFilePath] string callerName = "",
                            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
                        );

    }

    /// <summary>
    /// Tipo de log 
    /// </summary>
    public enum LogType
    {
        Info,
        Warn,
        Error,
        Fatal
    }

    public enum LogLevel
    {
        Minimum = 1,
        Normal = 2,
        Maximum = 3
    }
}
