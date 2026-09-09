using System.Net;

namespace soclean.business.Exceptions;

public class SignInException : Exception, IBaseException
{
    public SignInException(string message = "Not sign in") : base(message)
    {

    }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Conflict;

}
