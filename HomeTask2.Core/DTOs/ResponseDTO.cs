using System.Net;

namespace HomeTask2.Core.DTOs
{
    public class ResponseDTO<T>
    {
        public T? Data { get; }
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public ResponseDTO(HttpStatusCode statusCode, string message, T? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
