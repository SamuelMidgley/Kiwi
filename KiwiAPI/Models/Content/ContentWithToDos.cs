namespace KiwiAPI.Models.Content;

public class ContentWithToDos : Content
{
    public required IEnumerable<ToDo.ToDo> ToDos { get; set; }
}