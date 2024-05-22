

public class MobizonDTO<T> where T : class
{
    public int Code { get; set; }
    public T Data { get; set; } = null!;
    public string Message { get; set; } = string.Empty;
}