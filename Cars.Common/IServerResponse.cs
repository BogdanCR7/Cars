namespace Cars.Common
{
    public interface IServerResponse
    {
        public bool HasError { get; set; }
        public string? Message { get; set; }
    }
}
