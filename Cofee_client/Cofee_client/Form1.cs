using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cofee_client
{
    public partial class Form1 : Form
    {
        TcpClient client = null;
        NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;
        Thread listenerThread;


        public Form1()
        {
            InitializeComponent();
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8888);
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

                Console.WriteLine("Connected to Coffee Shop Chatbot.\n");
                string serverMessage;
                while (!string.IsNullOrEmpty(serverMessage = reader.ReadLine()))
                {
                    Console.WriteLine(serverMessage);
                }

                while (true)
                {
                    Console.Write("Enter your choice: ");
                    string userInput = Console.ReadLine();
                    writer.WriteLine(userInput); // Send input to the server

                    string response = reader.ReadLine(); // Read response from the server
                    Console.WriteLine(response);

                    if (response.StartsWith("Thank you for ordering"))
                    {
                        break; // Exit after order confirmation
                    }
                }

                reader.Close();
                writer.Close();
                stream.Close();
                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Error: Unable to connect to the server. " + ex.Message);
            }
        }
    }
private void button1_Click(object sender, EventArgs e)
{
    if (!string.IsNullOrWhiteSpace(textBox2.Text))
    {
        try
        {
            textBox1.Text += "You: " + textBox2.Text + "\r\n";
            writer.WriteLine(textBox2.Text);

            textBox2.Clear();
        }
        catch (IOException ex)
        {
            MessageBox.Show("Error: Unable to send message. " + ex.Message);
        }
    }
    else
    {
        MessageBox.Show("Please enter a valid choice.");
    }
}


    }

       

      