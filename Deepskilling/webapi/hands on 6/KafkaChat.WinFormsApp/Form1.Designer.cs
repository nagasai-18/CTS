namespace KafkaChat.WinFormsApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private Label lblClientName;
    private TextBox txtClientName;
    private Label lblMessage;
    private TextBox txtMessage;
    private Button btnSend;
    private ListBox lstMessages;
    private Label lblStatus;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblClientName = new Label();
        txtClientName = new TextBox();
        lblMessage = new Label();
        txtMessage = new TextBox();
        btnSend = new Button();
        lstMessages = new ListBox();
        lblStatus = new Label();
        SuspendLayout();

        lblClientName.AutoSize = true;
        lblClientName.Location = new Point(24, 22);
        lblClientName.Name = "lblClientName";
        lblClientName.Size = new Size(78, 15);
        lblClientName.TabIndex = 0;
        lblClientName.Text = "Client Name";

        txtClientName.Location = new Point(24, 40);
        txtClientName.Name = "txtClientName";
        txtClientName.Size = new Size(260, 23);
        txtClientName.TabIndex = 1;

        lblMessage.AutoSize = true;
        lblMessage.Location = new Point(24, 78);
        lblMessage.Name = "lblMessage";
        lblMessage.Size = new Size(53, 15);
        lblMessage.TabIndex = 2;
        lblMessage.Text = "Message";

        txtMessage.Location = new Point(24, 96);
        txtMessage.Name = "txtMessage";
        txtMessage.Size = new Size(450, 23);
        txtMessage.TabIndex = 3;

        btnSend.Location = new Point(492, 95);
        btnSend.Name = "btnSend";
        btnSend.Size = new Size(100, 25);
        btnSend.TabIndex = 4;
        btnSend.Text = "Send";
        btnSend.UseVisualStyleBackColor = true;
        btnSend.Click += btnSend_Click;

        lstMessages.FormattingEnabled = true;
        lstMessages.ItemHeight = 15;
        lstMessages.Location = new Point(24, 136);
        lstMessages.Name = "lstMessages";
        lstMessages.Size = new Size(744, 274);
        lstMessages.TabIndex = 5;

        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(24, 424);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(119, 15);
        lblStatus.TabIndex = 6;
        lblStatus.Text = "Waiting to connect...";

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 460);
        Controls.Add(lblStatus);
        Controls.Add(lstMessages);
        Controls.Add(btnSend);
        Controls.Add(txtMessage);
        Controls.Add(lblMessage);
        Controls.Add(txtClientName);
        Controls.Add(lblClientName);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Kafka WinForms Chat";
        ResumeLayout(false);
        PerformLayout();
    }
}
