using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wellAI
{
    public partial class WellAI : Form
    {
        public WellAI()
        {
            MaximizeBox = false;
            InitializeComponent();
            // Configura el FlowLayoutPanel
            flowLayoutPanelChat.AutoScroll = true;
            flowLayoutPanelChat.WrapContents = false;
            flowLayoutPanelChat.FlowDirection = FlowDirection.TopDown;
        }

        private static readonly HttpClient client = new HttpClient();

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            string userMessage = textBoxMessage.Text.Trim();
            if (string.IsNullOrEmpty(userMessage))
            {
                MessageBox.Show("Por favor, escribe un mensaje.");
                return;
            }

            AddMessageToChat("Tú: " + userMessage, true);
            textBoxMessage.Clear();

            string responseMessage = await SendMessageToChatbotAPI(userMessage);
            AddMessageToChat("WellAI: " + responseMessage, false);
        }

        private void textBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                botonEnviar.PerformClick();
                e.SuppressKeyPress = true; // Evita que se inserte una nueva línea en el TextBox
            }
        }

        private void AddMessageToChat(string message, bool isUser)
        {
            var messagePanel = new RoundedPanel
            {
                CornerRadius = 8,
                Padding = new Padding(10),
                Margin = new Padding(10),
                BackColor = isUser ? Color.LightGreen : Color.LightBlue,
                AutoSize = true
            };

            var messageLabel = new Label
            {
                Text = message,
                AutoSize = true,
                MaximumSize = new Size(flowLayoutPanelChat.Width - 40, 0)
            };

            messagePanel.Controls.Add(messageLabel);
            flowLayoutPanelChat.Controls.Add(messagePanel);
            flowLayoutPanelChat.ScrollControlIntoView(messagePanel);
        }

        static async Task<string> SendMessageToChatbotAPI(string message)
        {
            var url = $"https://api.freegpt4.ddns.net/?text={Uri.EscapeDataString(message)}";

            try
            {
                var response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();

                // Print the response string for debugging
                Console.WriteLine(responseString);

                return responseString;
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                return $"Error: {ex.Message}";
            }
        }
    }
}
