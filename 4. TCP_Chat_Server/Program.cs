using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _4._TCP_Chat_Server;

internal class Program
{
    private static readonly object _lock = new();
    private static readonly Dictionary<int, TcpClient> _list_clients = new();
    
    static void Main(string[] args)
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;

        int count = 1;

        string fileName = "config.txt";
        IPAddress ip;
        int port;
        using StreamReader sr = new(fileName);
        ip = IPAddress.Parse(sr.ReadLine());
        port = int.Parse(sr.ReadLine());
        TcpListener socketServer = new(ip, port);
        socketServer.Start();
        Console.WriteLine($"Запуск сервера {ip}:{port}");
        while (true)
        {
            TcpClient client = socketServer.AcceptTcpClient();
            lock (_lock)
            {
                _list_clients.Add(count, client);
            }
            Console.WriteLine($"Появився на сервері новий клієнт {client.Client.RemoteEndPoint}");
            Thread t = new(HandleClients);
            t.Start(count);
            count++;
        }
    }

    public static void HandleClients(object c)
    {
        int id = (int)c;
        TcpClient client;
        lock (_lock)
        {
            client = _list_clients[id];
        }

        try
        {
            while (true)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[16054400];
                int byte_count = stream.Read(buffer);
                if (byte_count == 0)
                    break;
                string data = Encoding.UTF8.GetString(buffer, 0, byte_count);
                Console.WriteLine($"Повідомлення клієнта {data}");
                Broadcast(data);
            }
        }
        catch
        {
        }

        lock (_lock)
        {
            Console.WriteLine($"Клієнт покинув чат {client.Client.RemoteEndPoint}");
            _list_clients.Remove(id);
        }
        client.Client.Shutdown(SocketShutdown.Both);
        client.Close();
    }

    private static void Broadcast(string data)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(data);
        lock (_lock)
        {
            try
            {
                foreach (var c in _list_clients.Values)
                {
                    NetworkStream stream = c.GetStream();
                    stream.Write(buffer);
                }
            }
            catch
            {
            }
        }
    }
}