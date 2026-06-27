using System.Net;

namespace soclean.business.Exceptions;

public interface IBaseException
{
    public HttpStatusCode StatusCode { get; set; }
}
