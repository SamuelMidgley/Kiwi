using Kiwi.Application.Interfaces;

namespace Kiwi.Application.Content.Commands.DeleteContent;

public class DeleteContentHandler(IContentRepository repository) : IRequestHandler<DeleteContentCommand, bool>
{
    public async Task<bool> Handle(DeleteContentCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await repository.Delete(request.Id);

        if (rowsAffected == 0)
            throw new NotFoundException("Content", request.Id.ToString());
        
        return true;
    }
}