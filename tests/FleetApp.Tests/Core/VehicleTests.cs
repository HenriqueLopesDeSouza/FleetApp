using FleetApp.Core.Domain;
using FluentAssertions;

namespace FleetApp.Tests.Core;

public class VehicleTests
{
    [Fact]
    public void ChangeColor_trims_and_updates()
    {
        var v = VehicleFactory.Create(VehicleType.Car, new Chassis("C", 7u), "  Black  ");
        v.Color.Should().Be("Black");

        v.ChangeColor("  White ");
        v.Color.Should().Be("White");
    }

    [Fact]
    public void ChangeColor_throws_when_empty()
    {
        var v = VehicleFactory.Create(VehicleType.Car, new Chassis("C", 7u), "Blue");
        var act = () => v.ChangeColor("");
        act.Should().Throw<ArgumentException>();
    }
}
