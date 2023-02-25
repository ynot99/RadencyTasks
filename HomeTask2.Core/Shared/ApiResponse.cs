namespace HomeTask2.Core.Shared
{
    public class ApiResponse<T>
    {
        public string? Message { get; }
        public T? Data { get; }

        public ApiResponse(T? data, string? message = null)
        {
            Data = data;
            Message = message;
        }

    }
}
