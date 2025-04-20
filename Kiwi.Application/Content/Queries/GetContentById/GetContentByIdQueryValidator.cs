namespace Kiwi.Application.Content.Queries.GetContentById;

public class GetContentByIdQueryValidator : AbstractValidator<GetContentByIdQuery>
{
    public GetContentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Page id cannot be null.");
    }
}
