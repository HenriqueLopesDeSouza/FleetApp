using FluentValidation;

namespace FleetApp.Api.Dtos.Validators;

public sealed class CreateVehicleRequestValidator : AbstractValidator<CreateVehicleRequest>
{
    public CreateVehicleRequestValidator()
    {
        RuleFor(x => x.ChassisSeries).NotEmpty().MaximumLength(5);
        RuleFor(x => x.Color).NotEmpty().MaximumLength(10);
        RuleFor(x => x.ChassisNumber)
            .NotNull().WithMessage("ChassisNumber is required.")
            .GreaterThan(0u).WithMessage("ChassisNumber must be greater than zero.");
        RuleFor(x => x.Type)
        .Must(t => new[] { "Bus", "Truck", "Car" }.Contains(t, StringComparer.OrdinalIgnoreCase))
        .WithMessage("Type must be Bus, Truck or Car.");
    }
}