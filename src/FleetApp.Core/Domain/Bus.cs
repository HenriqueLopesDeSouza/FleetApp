namespace FleetApp.Core.Domain;
/// <summary>
/// Represents a bus in the fleet.
/// </summary>
public sealed class Bus : Vehicle
{
    public Bus(Chassis chassis, string color) : base(chassis, color) { }
    public override VehicleType Type => VehicleType.Bus;
    public override int NumberOfPassengers => 42;
}