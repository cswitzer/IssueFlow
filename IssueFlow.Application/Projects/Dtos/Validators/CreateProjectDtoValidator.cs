using FluentValidation;

namespace IssueFlow.Application.Projects.Dtos.Validators;

public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectDtoValidator()
    {
        RuleFor(x => x.OwnerProfileId)
            .NotEmpty()
            .WithMessage("Owner profile ID is required.");

        RuleFor(x => x.OrganizationId)
            .NotEmpty()
            .WithMessage("Organization ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Project name is required and must be at most 100 characters long.");

        RuleFor(x => x.Key)
            .NotEmpty()
            .MaximumLength(10)
            .Matches("^[A-Z0-9]+$")
            .WithMessage("Project key is required, must be at most 10 characters long, and contain only uppercase letters and numbers.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must be at most 500 characters long.");
    }
}
