namespace KafkaChat.ConsoleApp;

internal sealed class ChatMessage
{
    public string Sender { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTimeOffset SentAt { get; set; }
}