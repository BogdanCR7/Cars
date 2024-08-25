namespace Cars.Common
{
    public class ServerResponse<T> : IServerResponse
    {
        public bool HasError { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; } 
    }
}
