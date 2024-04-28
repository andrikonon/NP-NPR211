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
        try
        {
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            using Socket server = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(endPoint);
            Console.Write("Enter your message to the server: ");
            var message = Console.ReadLine();
            byte[] data = Encoding.Unicode.GetBytes(message);
            server.Send(data);
            byte[] buffer = new byte[1024];
            int bytes = 0;
            Console.WriteLine("Server's response: ");
            do
            {
                bytes = server.Receive(buffer);
                Console.Write(Encoding.Unicode.GetString(buffer));
            } while (server.Available > 0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}