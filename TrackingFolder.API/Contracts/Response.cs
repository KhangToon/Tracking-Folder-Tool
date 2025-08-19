namespace TrackingFolder.API.Contracts
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public Response() { }

        public Response(T data, string message = "", bool success = true)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }
}
