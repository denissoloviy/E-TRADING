using System.Web.Http.Dependencies;
using Ninject;

namespace E_TRADING.Ninject.Core
{
    /// <summary>
    /// The Ninject dependency resolver.
    /// </summary>
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyResolver"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Begins the scope.
        /// </summary>
        /// <returns>The <see cref="IDependencyScope" />.</returns>
        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }
}