using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;

namespace E_TRADING.Ninject.Core
{
    /// <summary>
    /// The Ninject dependency scope.
    /// </summary>
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyScope"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            _resolver = resolver;
        }

        /// <summary>
        /// Disposes of the dependency scope.
        /// </summary>
        public void Dispose()
        {
            var disposable = _resolver as IDisposable;
            disposable?.Dispose();
            _resolver = null;
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The <see cref="object"/>.</returns>
        /// <exception cref="ObjectDisposedException"></exception>
        public object GetService(Type serviceType)
        {
            if (_resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has been disposed");
            }

            return _resolver.TryGet(serviceType);
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        /// <exception cref="ObjectDisposedException"></exception>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has been disposed");
            }

            return _resolver.GetAll(serviceType);
        }
    }
}