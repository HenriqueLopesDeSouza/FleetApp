using FleetApp.Api.Dtos;
using FleetApp.Api.Dtos.Validators;
using FluentAssertions;

namespace FleetApp.Tests.Api.Validators;

public class ChangeColorRequestValidatorTests
{
    private readonly ChangeColorRequestValidator _validator = new();

    [Fact]
    public async Task Non_empty_color_is_valid()
    {
        var vr = await _validator.ValidateAsync(new ChangeColorRequest("White"));
        vr.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Empty_color_is_invalid()
    {
        var vr = await _validator.ValidateAsync(new ChangeColorRequest(""));
        vr.IsValid.Should().BeFalse();
    }
}
