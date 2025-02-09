using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;

namespace AternalJaguar
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = "banner.txt";  

            try
            {
                string content = File.ReadAllText(filePath);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(content); 
                Console.ResetColor();
            }
            catch (Exception)
            {
                Console.Clear();
            }
            
            Console.Title = "AternalJaguar";
            string[] ports = { "21", "22", "23", "25", "445", "3389", "5900", "4444", "10134", "1608", "1604", "50050", "139", "500", "80", "137", "139", "1433", "1434", "3306", "443" };
            Console.WriteLine("Enter the IP address: ");
            string ip = Console.ReadLine();

            try
            {
                var tasks = new List<Task>();

                foreach (string port in ports)
                {
                    tasks.Add(CheckPortAsync(ip, port));
                }

                await Task.WhenAll(tasks);
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Error has been detected while running AternalJaguar");
                Console.ReadLine();
            }
        }

        private static async Task CheckPortAsync(string ip, string port)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    await tcpClient.ConnectAsync(ip, Convert.ToInt32(port));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[+] Port {port} is open");
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[-] Port {port} is closed");
                }
            }
        }
    }
}