using Market.Application.Common.Errors;
using Market.Infrastructure.Common;
using System.Net;


namespace Market.Infrastructure.Errors
{
    public class UserNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage => Constants.USER_NOTFOUND;
    }
}
