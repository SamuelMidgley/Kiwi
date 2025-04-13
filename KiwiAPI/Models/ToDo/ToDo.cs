namespace KiwiAPI.Models.ToDo;

public class ToDo
{
    public int Id { get; set; }
    
    public int ContentId { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public DateTime DateCompleted { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public DateTime DateUpdated { get; set; }
}