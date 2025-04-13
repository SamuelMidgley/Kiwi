namespace KiwiAPI.Models.ToDo;

public class CreateToDoRequest
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public int ContentId { get; set; }
}