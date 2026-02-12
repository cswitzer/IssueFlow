using FluentValidation;

namespace IssueFlow.Application.Projects.Dtos.Validators;

public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
{
    public UpdateProjectDtoValidator()
    {
        RuleFor(x => x.OwnerProfileId)
            .NotEmpty()
            .When(x => x.OwnerProfileId.HasValue)
            .WithMessage("Owner profile ID cannot be empty if provided.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => x.Name != null)
            .WithMessage("Project name must not be empty and must be at most 100 characters long.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .When(x => x.Description != null)
            .WithMessage("Description must be at most 500 characters long.");

        RuleFor(x => x.IsArchived)
            .NotNull()
            .When(x => x.IsArchived.HasValue)
            .WithMessage("IsArchived must be a boolean value if provided.");
    }
}
