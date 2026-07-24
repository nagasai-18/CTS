using System.Text.Json;
using Confluent.Kafka;

namespace KafkaChat.WinFormsApp;

internal sealed class KafkaChatClient : IDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly IProducer<Null, string> producer;
    private readonly CancellationTokenSource cancellationTokenSource = new();
    private readonly string groupId;
    private IConsumer<Ignore, string>? consumer;
    private Task? consumerTask;

    public KafkaChatClient(string clientName)
    {
        groupId = $"winforms-chat-{clientName}-{Guid.NewGuid():N}";

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = KafkaSettings.BootstrapServers
        };

        producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public event Action<ChatMessage>? MessageReceived;

    public Task StartAsync()
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = KafkaSettings.BootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Latest,
            EnableAutoCommit = true,
            EnablePartitionEof = false
        };

        consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        consumer.Subscribe(KafkaSettings.Topic);

        consumerTask = Task.Run(() => ConsumeLoop(cancellationTokenSource.Token), cancellationTokenSource.Token);
        return Task.CompletedTask;
    }

    public async Task SendAsync(string sender, string text)
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

    private void ConsumeLoop(CancellationToken cancellationToken)
    {
        if (consumer is null)
        {
            return;
        }

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(cancellationToken);
                var chatMessage = JsonSerializer.Deserialize<ChatMessage>(result.Message.Value, JsonOptions);

                if (chatMessage is null)
                {
                    continue;
                }

                MessageReceived?.Invoke(chatMessage);
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
        cancellationTokenSource.Cancel();
        consumerTask?.Wait(2000);
        consumer?.Dispose();
        producer.Flush(TimeSpan.FromSeconds(2));
        producer.Dispose();
        cancellationTokenSource.Dispose();
    }
}