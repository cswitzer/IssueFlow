using FluentValidation;

namespace IssueFlow.Application.Issues.Dtos.Validators;

public class UpdateIssueDtoValidator : AbstractValidator<UpdateIssueDto>
{
    public UpdateIssueDtoValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(5)
            .MaximumLength(100)
            .When(x => x.Title != null) // only validate if Title is provided
            .WithMessage("Title must be between 5 and 100 characters.");
        RuleFor(x => x.Description)
            .MinimumLength(10)
            .MaximumLength(1000)
            .When(x => x.Description != null)
            .WithMessage("Description must be between 10 and 1000 characters.");
        RuleFor(x => x.IssueTypeId)
            .NotEmpty()
            .When(x => x.IssueTypeId.HasValue);
        RuleFor(x => x.IssueStatusId)
            .NotEmpty()
            .When(x => x.IssueStatusId.HasValue);
        RuleFor(x => x.IssuePriorityId)
            .NotEmpty()
            .When(x => x.IssuePriorityId.HasValue);
    }
}
