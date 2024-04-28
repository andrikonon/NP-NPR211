using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _1._Simple_Socket_Server;

class Program
{
    static async Task Main(string[] args)
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;
        
        var hostName = Dns.GetHostName();
        Console.WriteLine($"This machine is {hostName}");

        IPHostEntry localhost = await Dns.GetHostEntryAsync(hostName);

        int selectIP = 0;
        int i = 0;
        Console.WriteLine("0. localhost");
        foreach (var address in localhost.AddressList)
        {
            await Console.Out.WriteLineAsync($"{++i}. {address.ToString()}");
        }


        Console.Write(">> ");
        selectIP = int.Parse(Console.ReadLine()) - 1;
        IPAddress ip;
        if (selectIP == -1)
        {
            ip = IPAddress.Parse("127.0.0.1");
        }
        else
        {
            selectIP = int.Clamp(selectIP, 0, localhost.AddressList.Length);
            ip = localhost.AddressList[selectIP];
        }
        
        int port = 2083;

        IPEndPoint endPoint = new(ip, port);

        using Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        Console.Title = endPoint.ToString();
        try
        {
            socket.Bind(endPoint);
            socket.Listen(10);
            Console.WriteLine($"Running server on {endPoint}");
            while (true)
            {
                Console.WriteLine("Successfully started the server, waiting for requests");
                using Socket client = await socket.AcceptAsync();
                
                Console.WriteLine($"Client end point {client.RemoteEndPoint}");
                
                int bytes = 0;
                byte[] buffer = new byte[1024];
                string clientMessage = "";
                Console.WriteLine("Message from client: ");
                do
                {
                    bytes = client.Receive(buffer);
                    clientMessage += Encoding.Unicode.GetString(buffer);
                } while (client.Available > 0);
                
                Console.WriteLine(clientMessage);

                string message = $"Thanks. Your request `{clientMessage}` has been accepted {DateTime.Now}.";
                byte[] data = Encoding.Unicode.GetBytes(message);
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