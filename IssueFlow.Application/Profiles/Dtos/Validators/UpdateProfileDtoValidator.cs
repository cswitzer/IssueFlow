using FluentValidation;

namespace IssueFlow.Application.Profiles.Dtos.Validators;

public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
{
    public UpdateProfileDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => x.FirstName != null)
            .WithMessage("First name must not be empty and must be at most 100 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .When(x => x.LastName != null)
            .WithMessage("Last name must not be empty and must be at most 100 characters long.");

        RuleFor(x => x.ProfilePictureUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.ProfilePictureUrl))
            .WithMessage("Profile picture URL must be at most 500 characters long.");

        RuleFor(x => x.OrganizationIds)
            .Must(x => x == null || x.All(id => id != Guid.Empty))
            .When(x => x.OrganizationIds != null && x.OrganizationIds.Any())
            .WithMessage("Organization IDs cannot contain empty GUIDs.");
    }
}
