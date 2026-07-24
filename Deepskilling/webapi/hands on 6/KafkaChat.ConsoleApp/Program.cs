namespace KafkaChat.ConsoleApp;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "Kafka Console Chat";

        Console.WriteLine("Kafka Chat Console App");
        Console.WriteLine("Kafka broker: localhost:9092");
        Console.WriteLine("Topic: chat-message");
        Console.WriteLine();

        var mode = args.FirstOrDefault()?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(mode))
        {
            Console.Write("Enter mode (produce/consume): ");
            mode = Console.ReadLine()?.Trim().ToLowerInvariant();
        }

        using var client = new KafkaChatClient();
        using var cancellationTokenSource = new CancellationTokenSource();

        Console.CancelKeyPress += (_, eventArgs) =>
        {
            eventArgs.Cancel = true;
            cancellationTokenSource.Cancel();
        };

        switch (mode)
        {
            case "consume":
                client.Consume(cancellationTokenSource.Token);
                break;
            case "produce":
                await RunProducerAsync(client, cancellationTokenSource.Token);
                break;
            default:
                Console.WriteLine("Unknown mode. Use produce or consume.");
                break;
        }
    }

    private static async Task RunProducerAsync(KafkaChatClient client, CancellationToken cancellationToken)
    {
        Console.Write("Enter your name: ");
        var sender = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(sender))
        {
            sender = Environment.UserName;
        }

        Console.WriteLine("Type messages and press Enter. Type /exit to quit.");

        while (!cancellationToken.IsCancellationRequested)
        {
            var text = Console.ReadLine();

            if (text is null)
            {
                continue;
            }

            if (text.Equals("/exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                continue;
            }

            await client.PublishAsync(sender, text);
            Console.WriteLine("Message sent.");
        }
    }
}
