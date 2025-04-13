namespace KiwiAPI.Models.Content;

public class Content
{
    public int Id { get; init; }
    
    public required string Title { get; init; }
    
    public string? Description { get; init; }
    
    public ContentType ContentType { get; init; }
    
    public DateTime DateCreated { get; init; }
    
    public DateTime DateUpdated { get; init; }
}