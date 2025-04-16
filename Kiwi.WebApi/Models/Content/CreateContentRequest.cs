namespace Kiwi.WebApi.Models.Content;

public class CreateContentRequest
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public ContentType ContentType { get; set; }
}