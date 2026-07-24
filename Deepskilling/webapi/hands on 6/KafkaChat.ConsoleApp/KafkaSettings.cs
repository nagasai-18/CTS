namespace KafkaChat.ConsoleApp;

internal static class KafkaSettings
{
    public const string BootstrapServers = "localhost:9092";
    public const string Topic = "chat-message";
    public const string ConsumerGroupId = "console-chat-consumer";
}