using Confluent.Kafka;
using System.Diagnostics;
using System.Net;

namespace Producer.Services.Implementations
{
    public class SendMessage : ISendMessage
    {
        private readonly string bootstrapClient = "localhost:9092";
        private readonly string topic = "my-topic";
        public SendMessage()
        {

        }

        public async Task<bool> SendOrderRequest(string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapClient,
                ClientId = Dns.GetHostName(),
            };

            try
            {
                using var producer = new ProducerBuilder<string, string>(config).Build();
                var result = await producer.ProduceAsync(topic, new Message<string, string>
                {
                    Key = "message_id_1",
                    Value = message
                });

                Debug.WriteLine($"Delivery Timestamp: { result.Timestamp.UtcDateTime }");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: { ex.Message }");
            }

            return await Task.FromResult(false);
        }
    }
}
