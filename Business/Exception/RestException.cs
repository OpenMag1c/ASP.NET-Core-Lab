using System.Net;

namespace Business.Exception
{
    public class RestException : System.Exception
    {
        public RestException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }

        public HttpStatusCode Code { get; }

        public object Errors { get; set; }
    }
}
