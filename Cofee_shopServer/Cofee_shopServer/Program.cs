using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace CoffeeShopChatbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
      
            Dictionary<int, string> coffeeMenu = new Dictionary<int, string>
            {
                { 1, "Espresso" },
                { 2, "Latte" },
                { 3, "Cappuccino" },
                { 4, "Americano" },
                { 5, "Mocha" }
            };

            TcpListener server = new TcpListener(8888);
            server.Start();
            Console.WriteLine("Coffee Shop Chatbot Server Started!\nWaiting for clients...");

            while (true)
            {
                Socket clientSocket = server.AcceptSocket();
                Console.WriteLine("Client connected.");

                NetworkStream stream = new NetworkStream(clientSocket);
                StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
                StreamReader reader = new StreamReader(stream);

                writer.WriteLine("Welcome to Coffee Shop");
                foreach (var item in coffeeMenu)
                {
                    writer.WriteLine($"{item.Key}. {item.Value}");
                }
                writer.WriteLine("Please enter the number of the coffee you want to order.");

                try
                {
                    while (true)
                    {
                        string clientMessage = reader.ReadLine();

                        if (int.TryParse(clientMessage, out int coffeeChoice) && coffeeMenu.ContainsKey(coffeeChoice))
                        {
                            string coffeeName = coffeeMenu[coffeeChoice];
                            writer.WriteLine($"Thank you for ordering {coffeeName}. Your order will be ready shortly!");
                            Console.WriteLine($"Client ordered: {coffeeName}");
                        }
                        else
                        {
                            writer.WriteLine("Invalid choice. Please enter a number from the menu.");
                        }
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("Client disconnected.");
                }
                finally
                {
                    reader.Close();
                    writer.Close();
                    stream.Close();
                    clientSocket.Close();
                }
            }
        }
    }
}
