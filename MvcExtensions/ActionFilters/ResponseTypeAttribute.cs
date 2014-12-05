using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSquared.MvcExtensions.ActionFilters
{
    /// <summary>
    /// Use this to specify the entity type returned by an action when the declared return type
    /// is <see cref="System.Net.Http.HttpResponseMessage"/> or <see cref="IHttpActionResult"/>.
    /// The <see cref="ResponseType"/> will be read by <see cref="ApiExplorer"/> when generating <see cref="ApiDescription"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ResponseTypeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseTypeAttribute"/> class.
        /// </summary>
        /// <param name="responseType">The response type.</param>
        public ResponseTypeAttribute(Type responseType)
        {
            ResponseType = responseType;
        }

        /// <summary>
        /// Gets the response type.
        /// </summary>
        public Type ResponseType { get; private set; }
    }
}
