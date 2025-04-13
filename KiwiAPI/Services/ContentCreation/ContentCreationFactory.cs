using KiwiAPI.Models.Content;

namespace KiwiAPI.Services.ContentCreation;

public interface IContentCreationFactory
{
    IContentCreationService GetContentCreationService(ContentType contentType);
}

public class ContentCreationFactory(IServiceProvider serviceProvider) : IContentCreationFactory
{
    public IContentCreationService GetContentCreationService(ContentType contentType)
    {
        return contentType switch
        {
            ContentType.ToDo => serviceProvider.GetRequiredService<ToDoContentCreationService>(),
            _ => throw new NotImplementedException()
        };
    }
}