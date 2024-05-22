using System.IO;

namespace _5._TCP_Chat_Client;

public class ChatMessage
{
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;

    public static ChatMessage Deserialize(byte[] data)
    {
        using var m = new MemoryStream(data);
        using BinaryReader br = new(m);
        var userId = br.ReadString();
        var name = br.ReadString();
        var text = br.ReadString();
        var photo = br.ReadString();
        
        return new ChatMessage
        {
            UserId = userId,
            Name = name,
            Text = text,
            Photo = photo,
        };
    }

    public byte[] Serialize()
    {
        using var m = new MemoryStream();
        using BinaryWriter bw = new(m);
        bw.Write(UserId);
        bw.Write(Name);
        bw.Write(Text);
        bw.Write(Photo);
        return m.ToArray();
    }
}