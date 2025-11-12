using FleetApp.Core.Domain;
using FluentAssertions;

namespace FleetApp.Tests.Core;

public class VehicleFactoryTests
{
    [Theory]
    [InlineData(VehicleType.Car, 4, typeof(Car))]
    [InlineData(VehicleType.Truck, 1, typeof(Truck))]
    [InlineData(VehicleType.Bus, 42, typeof(Bus))]
    public void Create_returns_expected_concrete_type(VehicleType type, int passengers, Type expected)
    {
        var v = VehicleFactory.Create(type, new Chassis("AAA", 1u), "Red");

        v.Should().BeOfType(expected);
        v.NumberOfPassengers.Should().Be(passengers);
        v.Color.Should().Be("Red");
        v.Chassis.Id.Should().Be("AAA-1");
    }
}
