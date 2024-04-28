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


        Console.Title = endPoint.ToString();
        try
        {
            TCPServer server = new TCPServer(endPoint);
            Console.WriteLine($"Running server on {endPoint}");
            while (true)
            {
                Console.WriteLine("Successfully started the server, waiting for requests");
                await server.Respond();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("Hello, World!");
    }
}