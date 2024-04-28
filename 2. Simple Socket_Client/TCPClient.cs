using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _2._Simple_Socket_Client;

public class TCPClient
{
    private Socket _server;

    public TCPClient(IPEndPoint serverEndPoint)
    {
        _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _server.Connect(serverEndPoint);
    }

    public void Send(string message)
    {
        byte[] data = Encoding.Unicode.GetBytes(message);
        _server.Send(data);
    }
    
    public string Recieve() {
        byte[] buffer = new byte[1024];
        int bytes = 0;
        string message = String.Empty;
        do
        {
            bytes = _server.Receive(buffer);
            message += Encoding.Unicode.GetString(buffer);
        } while (_server.Available > 0);

        return message;
    }

}