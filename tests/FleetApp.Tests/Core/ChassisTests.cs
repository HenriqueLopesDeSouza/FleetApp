using FleetApp.Core.Domain;
using FluentAssertions;

namespace FleetApp.Tests.Core;

public class ChassisTests
{
    [Fact]
    public void Id_should_be_SERIES_uppercased_dash_Number()
    {
        var c = new Chassis("  ab-12  ", 42u);
        c.Id.Should().Be("AB-12-42");
    }
}
