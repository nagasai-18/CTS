namespace KafkaChat.WinFormsApp;

public partial class Form1 : Form
{
    private KafkaChatClient? chatClient;

    public Form1()
    {
        InitializeComponent();
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (string.IsNullOrWhiteSpace(txtClientName.Text))
        {
            txtClientName.Text = $"Client-{Random.Shared.Next(1000, 9999)}";
        }

        lblStatus.Text = "Connecting to Kafka...";

        chatClient = new KafkaChatClient(txtClientName.Text.Trim());
        chatClient.MessageReceived += OnMessageReceived;

        try
        {
            await chatClient.StartAsync();
            lblStatus.Text = $"Connected as {txtClientName.Text.Trim()}";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Connection failed: {ex.Message}";
        }
    }

    private async void btnSend_Click(object sender, EventArgs e)
    {
        if (chatClient is null)
        {
            lblStatus.Text = "Kafka client is not ready.";
            return;
        }

        var messageText = txtMessage.Text.Trim();

        if (string.IsNullOrWhiteSpace(messageText))
        {
            return;
        }

        try
        {
            await chatClient.SendAsync(txtClientName.Text.Trim(), messageText);
            txtMessage.Clear();
            lblStatus.Text = "Message sent.";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Send failed: {ex.Message}";
        }
    }

    private void OnMessageReceived(ChatMessage message)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => OnMessageReceived(message)));
            return;
        }

        lstMessages.Items.Add($"[{message.SentAt:HH:mm:ss}] {message.Sender}: {message.Text}");
        lstMessages.TopIndex = lstMessages.Items.Count - 1;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        chatClient?.Dispose();
        base.OnFormClosing(e);
    }
}
