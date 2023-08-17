
using Market.Application.Common.Errors;
using Market.Infrastructure.Common;
using System.Net;

namespace Market.Infrastructure.Errors
{
    public class AttributeNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => Constants.ATTRIBUTE_NOTFOUND;
    }
}
