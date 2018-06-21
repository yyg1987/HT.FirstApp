using System.Net;

namespace HT.FirstApp.Model
{
    public class ResponseMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
