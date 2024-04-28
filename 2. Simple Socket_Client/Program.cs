using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _2._Simple_Socket_Client;

class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;
        
        Console.WriteLine("Enter the server's IP address: ");
        var ip = IPAddress.Parse(Console.ReadLine());
        Console.WriteLine("Enter port: ");
        var port = int.Parse(Console.ReadLine());
        IPEndPoint endPoint = new IPEndPoint(ip, port);
        try
        {
            TCPClient client = new TCPClient(endPoint);
            Console.Write("Enter your message to the server: ");
            var message = Console.ReadLine();
            client.Send(message);
            Console.WriteLine("Server's response: ");
            var response = client.Recieve();
            Console.WriteLine(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}