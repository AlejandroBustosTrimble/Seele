using RAK.Fwk.Common.DependencyInjection;

namespace RAK.Fwk.Common.Log
{
    /// <summary>
    /// Contenedor del Logger
    /// </summary>
    public static class LoggerContainer
    {
        #region Members

        private static ILogger instance;

        #endregion

        #region Properties

        /// <summary>
        /// Singleton del Logger
        /// </summary>
        public static ILogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = DIEngineContainer.Instance.Resolve<ILogger>();
                }

                return instance;
            }
        }

        #endregion
    }
}
