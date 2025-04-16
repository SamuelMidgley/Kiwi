namespace Kiwi.WebApi.Models.Content;

public class ContentWithToDos : Content
{
    public required IEnumerable<ToDo.ToDo> ToDos { get; set; }
}