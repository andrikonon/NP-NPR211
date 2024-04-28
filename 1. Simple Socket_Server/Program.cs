using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _1._Simple_Socket_Server;

class Program
{
    static void Main(string[] args)
    {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 2083;

        IPEndPoint endPoint = new(ip, port);

        Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        Console.Title = endPoint.ToString();
        try
        {
            socket.Bind(endPoint);
            socket.Listen(10);
            Console.WriteLine($"Running server on {endPoint}");
            while (true)
            {
                Console.WriteLine("Successfully started the server, waiting for requests");
                Socket client = socket.Accept();
                
                Console.WriteLine($"Client end point {client.RemoteEndPoint}");
                
                int bytes = 0;
                byte[] data = new byte[1024];
                do
                {
                    bytes = client.Receive(data);
                    Console.WriteLine($"Message {Encoding.Unicode.GetString(data)}");
                } while (client.Available > 0);

                string message = $"Thanks. Your request has been accepted {DateTime.Now}.";
                data = Encoding.Unicode.GetBytes(message);
                client.Send(data);
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("Hello, World!");
    }
}