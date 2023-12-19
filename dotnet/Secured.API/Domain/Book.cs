namespace Secured.API.Domain;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Author { get; set; }
}