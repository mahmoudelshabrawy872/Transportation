using System.Net;

namespace TransportationAPI.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool IsSuccess { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public object Result { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
