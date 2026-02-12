using FluentValidation;

namespace IssueFlow.Application.Organizations.Dtos.Validators;

public class CreateOrganizationDtoValidator : AbstractValidator<CreateOrganizationDto>
{
    public CreateOrganizationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Organization name is required.")
            .MaximumLength(100);
        RuleFor(x => x.OwnerProfileId)
            .NotEmpty()
            .WithMessage("Owner profile ID is required.");
    }
}
