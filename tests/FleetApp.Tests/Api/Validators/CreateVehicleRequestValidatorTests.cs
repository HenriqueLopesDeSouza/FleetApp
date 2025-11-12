using FleetApp.Api.Dtos;
using FleetApp.Api.Dtos.Validators;
using FluentAssertions;

namespace FleetApp.Tests.Api.Validators;

public class CreateVehicleRequestValidatorTests
{
    private readonly CreateVehicleRequestValidator _validator = new();

    [Fact]
    public async Task Valid_request_passes()
    {
        var req = new CreateVehicleRequest("ABC", 12u, "Car", "Blue");
        var vr = await _validator.ValidateAsync(req);

        vr.IsValid.Should().BeTrue(vr.ToString());
    }

    [Fact]
    public async Task Missing_or_invalid_fields_fail()
    {
        var req = new CreateVehicleRequest("", 0u, "Plane", "");
        var vr = await _validator.ValidateAsync(req);

        vr.IsValid.Should().BeFalse();

        vr.Errors.Should().Contain(e => e.PropertyName == nameof(CreateVehicleRequest.ChassisSeries));
        vr.Errors.Should().Contain(e => e.PropertyName == nameof(CreateVehicleRequest.Color));
        vr.Errors.Should().Contain(e => e.PropertyName == nameof(CreateVehicleRequest.Type));
        vr.Errors.Should().Contain(e => e.PropertyName == nameof(CreateVehicleRequest.ChassisNumber));
    }

    [Theory]
    [InlineData("Bus")]
    [InlineData("Truck")]
    [InlineData("Car")]
    [InlineData("car")]
    public async Task Type_must_be_valid(string type)
    {
        var req = new CreateVehicleRequest("AAA", 1u, type, "Red");
        var vr = await _validator.ValidateAsync(req);
        vr.IsValid.Should().BeTrue(vr.ToString());
    }
}
