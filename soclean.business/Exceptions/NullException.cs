using System.Net;

namespace soclean.business.Exceptions;

public class NullException : Exception, IBaseException
{
    public NullException(string message = "null exception") : base(message)
    {

    }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Conflict;

}
