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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            AppendMessage("Tú: " + userMessage);
            textBoxMessage.Clear();

            string responseMessage = await SendMessageToChatbotAPI(userMessage);
            AppendMessage("IA: " + responseMessage);
        }

        private void AppendMessage(string message)
        {
            richTextBoxChat.AppendText(message + Environment.NewLine);
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
