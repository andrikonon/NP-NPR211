namespace _7._NovaPoshta;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        NovaPoshtaService nps = new();
        nps.GetAreas();
        nps.GetSettlements();
    }
}