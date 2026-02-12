using FluentValidation;

namespace IssueFlow.Application.Profiles.Dtos.Validators;

public class CreateProfileDtoValidator : AbstractValidator<CreateProfileDto>
{
    public CreateProfileDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("First name is required and must be at most 100 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Last name is required and must be at most 100 characters long.");

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
