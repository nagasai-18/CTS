using System.Text.Json;
using Confluent.Kafka;

namespace KafkaChat.ConsoleApp;

internal sealed class KafkaChatClient : IDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly IProducer<Null, string> producer;

    public KafkaChatClient()
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = KafkaSettings.BootstrapServers
        };

        producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async Task PublishAsync(string sender, string text)
    {
        var message = new ChatMessage
        {
            Sender = sender,
            Text = text,
            SentAt = DateTimeOffset.Now
        };

        var payload = JsonSerializer.Serialize(message, JsonOptions);
        await producer.ProduceAsync(KafkaSettings.Topic, new Message<Null, string> { Value = payload });
    }

    public void Consume(CancellationToken cancellationToken)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = KafkaSettings.BootstrapServers,
            GroupId = KafkaSettings.ConsumerGroupId,
            AutoOffsetReset = AutoOffsetReset.Latest,
            EnableAutoCommit = true,
            EnablePartitionEof = false
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        consumer.Subscribe(KafkaSettings.Topic);

        try
        {
            Console.WriteLine($"Listening on topic '{KafkaSettings.Topic}'... Press Ctrl+C to stop.");

            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(cancellationToken);
                var chatMessage = JsonSerializer.Deserialize<ChatMessage>(result.Message.Value, JsonOptions);

                if (chatMessage is null)
                {
                    continue;
                }

                Console.WriteLine($"[{chatMessage.SentAt:HH:mm:ss}] {chatMessage.Sender}: {chatMessage.Text}");
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            consumer.Close();
        }
    }

    public void Dispose()
    {
        producer.Flush(TimeSpan.FromSeconds(2));
        producer.Dispose();
    }
}