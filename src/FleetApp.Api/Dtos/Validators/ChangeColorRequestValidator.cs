using FluentValidation;


namespace FleetApp.Api.Dtos.Validators;


public sealed class ChangeColorRequestValidator : AbstractValidator<ChangeColorRequest>
{
    public ChangeColorRequestValidator()
    { RuleFor(x => x.Color).NotEmpty().MaximumLength(10); }
}