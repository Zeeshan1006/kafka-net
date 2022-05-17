namespace Producer.Services
{
    public interface ISendMessage
    {
        Task<bool> SendOrderRequest(string message);
    }
}
