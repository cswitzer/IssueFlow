using FluentValidation;

namespace IssueFlow.Application.Issues.Dtos.Validators;

public class CreateIssueDtoValidator : AbstractValidator<CreateIssueDto>
{
    public CreateIssueDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100)
            .WithMessage("Title must be between 5 and 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(1000);
        RuleFor(x => x.ProjectId)
            .NotEmpty();
        RuleFor(x => x.IssueTypeId)
            .NotEmpty();
        RuleFor(x => x.IssueStatusId)
            .NotEmpty();
        RuleFor(x => x.IssuePriorityId)
            .NotEmpty();
    }
}
