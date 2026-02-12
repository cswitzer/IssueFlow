using FluentValidation;

namespace IssueFlow.Application.Organizations.Dtos.Validators;


public class UpdateOrganizationDtoValidator : AbstractValidator<UpdateOrganizationDto>
{
    public UpdateOrganizationDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Organization name must be at most 100 characters long.");
        RuleFor(x => x.OwnerProfileId)
            .NotEmpty()
            .When(x => x.OwnerProfileId.HasValue)
            .WithMessage("Owner profile ID cannot be empty if provided.");
        RuleFor(x => x.IsActive)
            .NotNull()
            .When(x => x.IsActive.HasValue)
            .WithMessage("IsActive must be a boolean value if provided.");
    }
}
