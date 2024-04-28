using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _1._Simple_Socket_Server;

public class TCPServer
{
    private Socket _server;

    public TCPServer(IPEndPoint serverEndPoint)
    {
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _server.Bind(serverEndPoint);
        _server.Listen();
    }

    public async Task Respond()
    {
        using Socket client = await _server.AcceptAsync();
        int bytes = 0;
        byte[] buffer = new byte[1024];
        string clientMessage = String.Empty;
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