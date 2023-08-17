
using System.Net;
using Market.Application.Common.Errors;

namespace Market.Infrastructure.Errors
{
    public class ServiceException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
