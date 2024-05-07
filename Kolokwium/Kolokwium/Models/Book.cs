namespace Kolokwium.Models;

public class Book
{
    public int PK { get; set; }
    public string title { get; set; }
    public List<Genres> genres { get; set; }
}