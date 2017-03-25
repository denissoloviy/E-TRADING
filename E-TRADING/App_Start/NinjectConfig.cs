using E_TRADING.Ninject.Core;
using E_TRADING.Ninject.Modules;

namespace E_TRADING
{
    public class NinjectConfig
    {
        /// <summary>
        /// Starts initialization of the Ninject bindings.
        /// </summary>
        public static void Start()
        {
            NinjectWebCommon.Start(new CoreModule());
        }

        /// <summary>
        /// Destroys the Ninject bindings.
        /// </summary>
        public static void Stop()
        {
            NinjectWebCommon.Stop();
        }
    }
}