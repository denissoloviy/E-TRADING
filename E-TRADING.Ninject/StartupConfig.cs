namespace E_TRADING.Ninject
{
    using System.Web.Http;

    /// <summary>
    /// The startup config.
    /// </summary>
    public class StartupConfig
    {
        private static HttpConfiguration _config;

        /// <summary>
        /// Gets the config.
        /// </summary>
        public static HttpConfiguration Config
        {
            get
            {
                return _config ?? (_config = new HttpConfiguration());
            }
        }
    }
}