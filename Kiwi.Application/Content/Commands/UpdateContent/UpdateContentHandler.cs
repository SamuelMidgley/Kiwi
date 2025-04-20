using Kiwi.Application.Interfaces;

namespace Kiwi.Application.Content.Commands.UpdateContent;

public class UpdateContentHandler(IContentRepository repository) : IRequestHandler<UpdateContentCommand, bool>
{
    public async Task<bool> Handle(UpdateContentCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected =  await repository.Update(request.Id, request.Title, request.Description);
        
        if (rowsAffected == 0)
            throw new NotFoundException("Content", request.Id.ToString());
        
        return true;
    }
}