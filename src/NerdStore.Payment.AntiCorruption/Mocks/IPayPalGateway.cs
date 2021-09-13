namespace NerdStore.Payment.AntiCorruption
{
    public interface IPayPalGateway
    {
        bool CommitTransaction(string cardHashKey, string orderId, decimal amount);
        string GetCardHashKey(string serviceKey, string creditCard);
        string GetServiceKey(string apiKey, string encriptionKey);
    }
}
